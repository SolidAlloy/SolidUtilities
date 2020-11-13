namespace SolidUtilities.Editor.PropertyDrawers
{
    using System;
    using Attributes;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ResizableTextAreaAttribute))]
    public class ResizableTextAreaAttributeDrawer : PropertyDrawer
    {
        private static GUIStyle _style;

        private static GUIStyle Style
        {
            get
            {
                if (_style == null)
                    _style = new GUIStyle(EditorStyles.textField) { wordWrap = true };

                return _style;
            }
        }

        public override void OnGUI(Rect fieldRect, SerializedProperty property, GUIContent label)
        {
            if (property.type != "string")
                throw new ArgumentException($"ResizableTextArea attribute works only with string fields. {property.type} type was used instead.");

            // EditorGUILayout adds one empty line for some reason
            Rect labelRect = new Rect(fieldRect) { height = EditorGUIUtility.singleLineHeight };
            EditorGUI.LabelField(labelRect, label);

            property.stringValue = EditorGUILayout.TextArea(property.stringValue, Style);

            if (property.serializedObject.hasModifiedProperties)
                property.serializedObject.ApplyModifiedProperties();
        }
    }
}