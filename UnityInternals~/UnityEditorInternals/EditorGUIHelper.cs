namespace SolidUtilities.UnityEditorInternals
{
    using System;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class EditorGUIProxy
    {
        public static bool HasKeyboardFocus(int controlID) => EditorGUI.HasKeyboardFocus(controlID);

#if UNITY_2020
        // I've seen a lot of ugly methods in Unity source code, but this is just.. OMG
        // The method is identical to the original, only non-delayed fields are replaced with their delayed versions where possible.
        // For SerializedPropertyType.Integer, there is also a ternary expression instead of a single LongField because a version of DelayedLongField doesn't exist.
        public static bool DefaultPropertyFieldDelayed(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginPropertyInternal(position, label, property);

            SerializedPropertyType type = property.propertyType;

            bool childrenAreExpanded = false;

            // Should we inline? All one-line vars as well as Vector2, Vector3, Rect and Bounds properties are inlined.
            if ( ! EditorGUI.HasVisibleChildFields(property))
            {
                switch (type)
                {
                    case SerializedPropertyType.Integer:
                    {
                        EditorGUI.BeginChangeCheck();

                        long newValue = property.longValue > Int32.MaxValue
                            ? EditorGUI.LongField(position, label, property.longValue)
                            : EditorGUI.DelayedIntField(position, label, property.intValue);

                        if (EditorGUI.EndChangeCheck())
                        {
                            property.longValue = newValue;
                        }
                        break;
                    }
                    case SerializedPropertyType.Float:
                    {
                        EditorGUI.BeginChangeCheck();

                        // Necessary to check for float type to get correct string formatting for float and double.
                        bool isFloat = property.type == "float";
                        double newValue = isFloat ? EditorGUI.DelayedFloatField(position, label, property.floatValue) :
                            EditorGUI.DelayedDoubleField(position, label, property.doubleValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            property.doubleValue = newValue;
                        }
                        break;
                    }
                    case SerializedPropertyType.String:
                    {
                        EditorGUI.BeginChangeCheck();
                        string newValue = EditorGUI.DelayedTextField(position, label, property.stringValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            property.stringValue = newValue;
                        }
                        break;
                    }
                    case SerializedPropertyType.Boolean:
                    {
                        EditorGUI.BeginChangeCheck();
                        bool newValue = EditorGUI.Toggle(position, label, property.boolValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            property.boolValue = newValue;
                        }
                        break;
                    }
                    case SerializedPropertyType.Color:
                    {
                        EditorGUI.BeginChangeCheck();
                        Color newColor = EditorGUI.ColorField(position, label, property.colorValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            property.colorValue = newColor;
                        }
                        break;
                    }
                    case SerializedPropertyType.ArraySize:
                    {
                        EditorGUI.BeginChangeCheck();
                        int newValue = EditorGUI.ArraySizeField(position, label, property.intValue, EditorStyles.numberField);
                        if (EditorGUI.EndChangeCheck())
                        {
                            property.intValue = newValue;
                        }
                        break;
                    }
                    case SerializedPropertyType.FixedBufferSize:
                    {
                        EditorGUI.DelayedIntField(position, label, property.intValue);
                        break;
                    }
                    case SerializedPropertyType.Enum:
                    {
                        EditorGUI.EnumPopup(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.ObjectReference:
                    {
                        EditorGUI.ObjectFieldInternal(position, property, null, label, EditorStyles.objectField);
                        break;
                    }
                    case SerializedPropertyType.LayerMask:
                    {
                        EditorGUI.LayerMaskField(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Character:
                    {
                        char[] value = { (char)property.intValue };

                        bool wasChanged = GUI.changed;
                        GUI.changed = false;
                        string newValue = EditorGUI.DelayedTextField(position, label, new string(value));
                        if (GUI.changed)
                        {
                            if (newValue.Length == 1)
                            {
                                property.intValue = newValue[0];
                            }
                            // Value didn't get changed after all
                            else
                            {
                                GUI.changed = false;
                            }
                        }
                        GUI.changed |= wasChanged;
                        break;
                    }
                    case SerializedPropertyType.AnimationCurve:
                    {
                        int id = GUIUtility.GetControlID(EditorGUI.s_CurveHash, FocusType.Keyboard, position);
                        EditorGUI.DoCurveField(EditorGUI.PrefixLabel(position, id, label), id, null, EditorGUI.kCurveColor, new Rect(), property);
                        break;
                    }
                    case SerializedPropertyType.Gradient:
                    {
                        int id = GUIUtility.GetControlID(EditorGUI.s_CurveHash, FocusType.Keyboard, position);
                        EditorGUI.DoGradientField(EditorGUI.PrefixLabel(position, id, label), id, null, property, false, ColorSpace.Gamma);
                        break;
                    }
                    case SerializedPropertyType.Vector3:
                    {
                        EditorGUI.Vector3Field(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Vector4:
                    {
                        EditorGUI.Vector4Field(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Vector2:
                    {
                        EditorGUI.Vector2Field(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Vector2Int:
                    {
                        EditorGUI.Vector2IntField(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Vector3Int:
                    {
                        EditorGUI.Vector3IntField(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Rect:
                    {
                        EditorGUI.RectField(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.RectInt:
                    {
                        EditorGUI.RectIntField(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.Bounds:
                    {
                        EditorGUI.BoundsField(position, property, label);
                        break;
                    }
                    case SerializedPropertyType.BoundsInt:
                    {
                        EditorGUI.BoundsIntField(position, property, label);
                        break;
                    }
                    default:
                    {
                        int genericID = GUIUtility.GetControlID(EditorGUI.s_GenericField, FocusType.Keyboard, position);
                        EditorGUI.PrefixLabel(position, genericID, label);
                        break;
                    }
                }
            }
            // Handle Foldout
            else
            {
                Event tempEvent = new Event(Event.current);

                // Handle the actual foldout first, since that's the one that supports keyboard control.
                // This makes it work more consistent with PrefixLabel.
                childrenAreExpanded = property.isExpanded;

                bool newChildrenAreExpanded = childrenAreExpanded;
                using (new EditorGUI.DisabledScope(!property.editable))
                {
                    GUIStyle foldoutStyle = (DragAndDrop.activeControlID == -10) ? EditorStyles.foldoutPreDrop : EditorStyles.foldout;
                    newChildrenAreExpanded = EditorGUI.Foldout(position, childrenAreExpanded, EditorGUI.s_PropertyFieldTempContent, true, foldoutStyle);
                }


                if (childrenAreExpanded && property.isArray && property.arraySize > property.serializedObject.maxArraySizeForMultiEditing && property.serializedObject.isEditingMultipleObjects)
                {
                    Rect boxRect = position;
                    boxRect.xMin += EditorGUIUtility.labelWidth - EditorGUI.indent;

                    EditorGUI.s_ArrayMultiInfoContent.text = EditorGUI.s_ArrayMultiInfoContent.tooltip = string.Format(EditorGUI.s_ArrayMultiInfoFormatString, property.serializedObject.maxArraySizeForMultiEditing);
                    EditorGUI.LabelField(boxRect, GUIContent.none, EditorGUI.s_ArrayMultiInfoContent, EditorStyles.helpBox);
                }

                if (newChildrenAreExpanded != childrenAreExpanded)
                {
                    // Recursive set expanded
                    if (Event.current.alt)
                    {
                        EditorGUI.SetExpandedRecurse(property, newChildrenAreExpanded);
                    }
                    // Expand one element only
                    else
                    {
                        property.isExpanded = newChildrenAreExpanded;
                    }
                }
                childrenAreExpanded = newChildrenAreExpanded;


                // Check for drag & drop events here, to add objects to an array by dragging to the foldout.
                // The event may have already been used by the Foldout control above, but we want to also use it here,
                // so we use the event copy we made prior to calling the Foldout method.

                // We need to use last s_LastControlID here to ensure we do not break duplicate functionality (fix for case 598389)
                // If we called GetControlID here s_LastControlID would be incremented and would not longer be in sync with GUIUtililty.keyboardFocus that
                // is used for duplicating (See DoPropertyFieldKeyboardHandling)
                int id = EditorGUIUtility.s_LastControlID;
                switch (tempEvent.type)
                {
                    case EventType.DragExited:
                        if (GUI.enabled)
                        {
                            HandleUtility.Repaint();
                        }

                        break;
                    case EventType.DragUpdated:
                    case EventType.DragPerform:

                        if (position.Contains(tempEvent.mousePosition) && GUI.enabled)
                        {
                            Object[] references = DragAndDrop.objectReferences;

                            // Check each single object, so we can add multiple objects in a single drag.
                            Object[] oArray = new Object[1];
                            bool didAcceptDrag = false;
                            foreach (Object o in references)
                            {
                                oArray[0] = o;
                                Object validatedObject = EditorGUI.ValidateObjectFieldAssignment(oArray, null, property, EditorGUI.ObjectFieldValidatorOptions.None);
                                if (validatedObject != null)
                                {
                                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                                    if (tempEvent.type == EventType.DragPerform)
                                    {
                                        property.AppendFoldoutPPtrValue(validatedObject);
                                        didAcceptDrag = true;
                                        DragAndDrop.activeControlID = 0;
                                    }
                                    else
                                    {
                                        DragAndDrop.activeControlID = id;
                                    }
                                }
                            }
                            if (didAcceptDrag)
                            {
                                GUI.changed = true;
                                DragAndDrop.AcceptDrag();
                            }
                        }
                        break;
                }
            }

            EditorGUI.EndProperty();

            return childrenAreExpanded;
        }
#endif
    }
}