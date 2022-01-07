namespace SolidUtilities.Editor.PropertyDrawers
{
    using System;
    using SolidUtilities;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ResizableTextAreaAttribute))]
    public class ResizableTextAreaAttributeDrawer : PropertyDrawer
    {
        private static readonly GUIContent _tempContent = new GUIContent();
        private static GUIStyle _style;

        private float _textAreaHeight;

        private static GUIStyle Style => _style ?? (_style = new GUIStyle(EditorStyles.textField) { wordWrap = true });

        private static GUIContent TempContent(string text)
        {
            _tempContent.text = text;
            return _tempContent;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float labelHeight = EditorGUIUtility.singleLineHeight;
            return labelHeight + _textAreaHeight;
        }

        public override void OnGUI(Rect fieldRect, SerializedProperty property, GUIContent label)
        {
            if (property.type != "string")
                throw new ArgumentException($"ResizableTextArea attribute works only with string fields. {property.type} type was used instead.");

            Rect labelRect = new Rect(fieldRect) { height = EditorGUIUtility.singleLineHeight };
            EditorGUI.LabelField(labelRect, label);

            // fieldRect has the correct width only if the event type is repaint.
            if (Event.current.type == EventType.Repaint)
                _textAreaHeight = Style.CalcHeight(TempContent(property.stringValue), fieldRect.width);

            var textAreaRect = new Rect(fieldRect.x, fieldRect.y + EditorGUIUtility.singleLineHeight, fieldRect.width, _textAreaHeight);
            property.stringValue = EditorGUI.TextArea(textAreaRect, property.stringValue, Style);

            if (property.serializedObject.hasModifiedProperties)
                property.serializedObject.ApplyModifiedProperties();
        }
    }
}