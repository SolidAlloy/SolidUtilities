namespace SolidUtilities.Editor.PropertyDrawers
{
    using Editor;
    using SolidUtilities;
    using UnityEditor;
    using UnityEditorInternals;
    using UnityEngine;

    public abstract class DrawerWithModes : PropertyDrawer
    {
        private static GUIStyle _buttonStyle;
        private static GUIStyle ButtonStyle => _buttonStyle ??= new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
        {
            imagePosition = ImagePosition.ImageOnly
        };

        private static float ButtonWidth => ButtonStyle.fixedWidth;

        protected abstract SerializedProperty ExposedProperty { get; }

        protected abstract bool ShouldDrawFoldout { get; }

        protected abstract string[] PopupOptions { get; }

        protected abstract int PopupValue { get; set; }

        protected bool ShowChoiceButton { get; set; } = true;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (! property.isExpanded)
                return EditorGUIUtility.singleLineHeight;

            // If a property has a custom property drawer, it will be drown inside a foldout anyway, so we account for
            // it by adding a single line height.

            float additionalHeight = ExposedProperty.HasCustomPropertyDrawer() ? EditorGUIUtility.singleLineHeight : 0f;
            return EditorGUI.GetPropertyHeight(ExposedProperty, GUIContent.none) + additionalHeight;
        }

        public override void OnGUI(Rect fieldRect, SerializedProperty property, GUIContent label)
        {
            (Rect labelRect, Rect buttonRect, Rect valueRect) = GetLabelButtonValueRects(fieldRect);

            DrawLabel(property, fieldRect, labelRect, label);

            // The indent level must be made 0 for the button and value to be displayed normally, without any
            // additional indent. Otherwise, the button will not be clickable, and the value will look shifted
            // compared to other fields.
            int previousIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            if (ShowChoiceButton)
                PopupValue = DrawButton(buttonRect, PopupValue);

            DrawValue(property, valueRect, fieldRect, previousIndent);

            EditorGUI.indentLevel = previousIndent;
        }

        protected abstract void DrawValue(SerializedProperty property, Rect valueRect, Rect totalRect, int indentLevel);

        protected void DrawValueProperty(SerializedProperty mainProperty, Rect valueRect, Rect totalRect, int indentLevel)
        {
            if (ExposedProperty.propertyType == SerializedPropertyType.Generic)
            {
                DrawValueInFoldout(mainProperty, ExposedProperty, totalRect, indentLevel);
            }
            else
            {
                EditorGUI.PropertyField(valueRect, ExposedProperty, GUIContent.none);
            }
        }

        private void DrawLabel(SerializedProperty property, Rect totalRect, Rect labelRect, GUIContent label)
        {
            if (ShouldDrawFoldout)
            {
                property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, label, true);
            }
            else
            {
                EditorGUI.HandlePrefixLabel(totalRect, labelRect, label);
            }
        }

        private (Rect label, Rect button, Rect value) GetLabelButtonValueRects(Rect totalRect)
        {
            const float indentWidth = 15f;
            const float valueLeftIndent = 2f;

            totalRect.height = EditorGUIUtility.singleLineHeight;

            (Rect labelAndButtonRect, Rect valueRect) = totalRect.CutVertically(EditorGUIUtility.labelWidth);

            labelAndButtonRect.xMin += EditorGUI.indentLevel * indentWidth;

            (Rect labelRect, Rect buttonRect) =
                labelAndButtonRect.CutVertically(ShowChoiceButton ? ButtonWidth : 0f, fromRightSide: true);

            valueRect.xMin += valueLeftIndent;
            return (labelRect, buttonRect, valueRect);
        }

        private static void DrawValueInFoldout(SerializedProperty mainProperty, SerializedProperty valueProperty, Rect totalRect, int indentLevel)
        {
            valueProperty.isExpanded = mainProperty.isExpanded;

            if ( ! mainProperty.isExpanded)
                return;

            var shiftedRect = totalRect.ShiftOneLineDown(indentLevel + 1);

            if (valueProperty.HasCustomPropertyDrawer())
            {
                shiftedRect.height = EditorGUI.GetPropertyHeight(valueProperty);
                EditorGUI.PropertyField(shiftedRect, valueProperty, GUIContent.none);
                return;
            }

            // This draws all child fields of the _constantValue property with indent.
            SerializedProperty iterator = valueProperty.Copy();
            var nextProp = valueProperty.Copy();
            nextProp.NextVisible(false);

            while (iterator.NextVisible(true) && ! SerializedProperty.EqualContents(iterator, nextProp))
            {
                shiftedRect.height = EditorGUI.GetPropertyHeight(iterator, false);
                EditorGUI.PropertyField(shiftedRect, iterator, true);
                shiftedRect = shiftedRect.ShiftOneLineDown(lineHeight: shiftedRect.height);
            }
        }

        private int DrawButton(Rect buttonRect, int currentValue)
        {
            return EditorGUI.Popup(buttonRect, currentValue, PopupOptions, ButtonStyle);
        }
    }
}