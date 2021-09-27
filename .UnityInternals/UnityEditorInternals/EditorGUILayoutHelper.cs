#if UNITY_2020

namespace SolidUtilities.UnityEditorInternals
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public static class EditorGUILayoutProxy
    {
        [PublicAPI]
        public static bool DelayedPropertyField(SerializedProperty property, GUIContent label = null, params GUILayoutOption[] options)
        {
            var handler = ScriptAttributeUtility.GetHandler(property);
            bool includeChildren = EditorGUILayout.IsChildrenIncluded(property);
            Rect rect = handler.GetPropertyRect(property, label, includeChildren, options);
            EditorGUILayout.s_LastRect = rect;
            return handler.OnGUIDelayed(rect, property, label, includeChildren);
        }

        [PublicAPI]
        public static bool DelayedPropertyField(Rect rect, SerializedProperty property, GUIContent label = null, bool includeChildren = false)
        {
            return ScriptAttributeUtility.GetHandler(property).OnGUIDelayed(rect, property, label, includeChildren);
        }

        private static Rect GetPropertyRect(this PropertyHandler handler, SerializedProperty property, GUIContent label,
            bool includeChildren, GUILayoutOption[] options)
        {
            if (property.propertyType == SerializedPropertyType.Boolean
                && handler.propertyDrawer == null
                && (handler.m_DecoratorDrawers == null || handler.m_DecoratorDrawers.Count == 0))
            {
                return EditorGUILayout.GetToggleRect(true, options);
            }

            return EditorGUILayout.GetControlRect(
                EditorGUI.LabelHasContent(label),
                handler.GetHeight(property, label, includeChildren),
                options);
        }

        private static void OnGUISafeExtended(this PropertyDrawer propertyDrawer, Rect position,
            SerializedProperty property, GUIContent label)
        {
            ScriptAttributeUtility.s_DrawerStack.Push(propertyDrawer);

            if (propertyDrawer is IDelayable delayableDrawer)
            {
                delayableDrawer.OnGUIDelayed(position, property, label);
            }
            else
            {
                propertyDrawer.OnGUI(position, property, label);
            }

            ScriptAttributeUtility.s_DrawerStack.Pop();
        }

        // EditorGUI.DefaultPropertyField is replaced with EditorGUIHelper.DefaultPropertyFieldDelayed
        // propertyDrawer.OnGUISafe is replaced with propertyDrawer.OnGUISafeExtended
        private static bool OnGUIDelayed(this PropertyHandler handler, Rect position, SerializedProperty property,
            GUIContent label, bool includeChildren)
        {
            Rect visibleArea = new Rect(0f, 0f, float.MaxValue, float.MaxValue);

            handler.TestInvalidateCache();

            float oldLabelWidth, oldFieldWidth;

            float propHeight = position.height;
            position.height = 0;
            if (handler.m_DecoratorDrawers != null && !handler.isCurrentlyNested)
            {
                foreach (DecoratorDrawer decorator in handler.m_DecoratorDrawers)
                {
                    position.height = decorator.GetHeight();

                    oldLabelWidth = EditorGUIUtility.labelWidth;
                    oldFieldWidth = EditorGUIUtility.fieldWidth;
                    decorator.OnGUI(position);
                    EditorGUIUtility.labelWidth = oldLabelWidth;
                    EditorGUIUtility.fieldWidth = oldFieldWidth;

                    position.y += position.height;
                    propHeight -= position.height;
                }
            }

            position.height = propHeight;
            if (handler.propertyDrawer != null)
            {
                // Remember widths
                oldLabelWidth = EditorGUIUtility.labelWidth;
                oldFieldWidth = EditorGUIUtility.fieldWidth;
                // Draw with custom drawer
                handler.propertyDrawer.OnGUISafeExtended(position, property.Copy(), label ?? EditorGUIUtility.TempContent(property.localizedDisplayName));
                // Restore widths
                EditorGUIUtility.labelWidth = oldLabelWidth;
                EditorGUIUtility.fieldWidth = oldFieldWidth;

                return false;
            }

            if (PropertyHandler.IsNonStringArray(property))
            {
                string key = ReorderableListWrapper.GetPropertyIdentifier(property);

                if ( ! PropertyHandler.s_reorderableLists.TryGetValue(key, out ReorderableListWrapper reorderableList))
                {
                    throw new IndexOutOfRangeException(
                        $"collection with name \"{property.name}\" doesn't have ReorderableList assigned to it.");
                }

                reorderableList.Property = property;
                reorderableList.Draw(label, position, visibleArea, includeChildren);
                return false;
            }

            if ( ! includeChildren)
            {
                return EditorGUIProxy.DefaultPropertyFieldDelayed(position, property, label);
            }

            // Remember state
            Vector2 oldIconSize = EditorGUIUtility.GetIconSize();
            bool wasEnabled = GUI.enabled;
            int origIndent = EditorGUI.indentLevel;

            int relIndent = origIndent - property.depth;

            SerializedProperty prop = property.Copy();

            position.height = EditorGUI.GetSinglePropertyHeight(prop, label);

            // First property with custom label
            EditorGUI.indentLevel = prop.depth + relIndent;
            bool childrenAreExpanded = EditorGUIProxy.DefaultPropertyFieldDelayed(position, prop, label) && EditorGUI.HasVisibleChildFields(prop);
            position.y += position.height + EditorGUI.kControlVerticalSpacing;

            // Loop through all child properties
            if (childrenAreExpanded)
            {
                SerializedProperty endProperty = prop.GetEndProperty();
                while (prop.NextVisible(childrenAreExpanded) && !SerializedProperty.EqualContents(prop, endProperty))
                {
                    var childHandler = ScriptAttributeUtility.GetHandler(prop);
                    EditorGUI.indentLevel = prop.depth + relIndent;
                    position.height = childHandler.GetHeight(prop, null, false);

                    if (position.Overlaps(visibleArea))
                    {
                        EditorGUI.BeginChangeCheck();
                        childrenAreExpanded = childHandler.OnGUIDelayed(position, prop, null, false) && EditorGUI.HasVisibleChildFields(prop);
                        // Changing child properties (like array size) may invalidate the iterator,
                        // so stop now, or we may get errors.
                        if (EditorGUI.EndChangeCheck())
                            break;
                    }

                    position.y += position.height + EditorGUI.kControlVerticalSpacing;
                }
            }

            // Restore state
            GUI.enabled = wasEnabled;
            EditorGUIUtility.SetIconSize(oldIconSize);
            EditorGUI.indentLevel = origIndent;

            return false;
        }
    }
}
#endif