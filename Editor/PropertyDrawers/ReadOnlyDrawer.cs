namespace GenericScriptableObjects.Usage_Example.Editor
{
    using SolidUtilities.Attributes;
    using SolidUtilities.Helpers;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    internal class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawHelper.WithDisabledGUI(() =>
            {
                EditorGUI.PropertyField(position, property, label, true);
            });
        }
    }
}