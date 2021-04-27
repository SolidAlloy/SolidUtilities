namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;
    using UnityEngine;

    public interface IDelayable
    {
        void OnGUIDelayed(Rect rect, SerializedProperty property, GUIContent label);
    }
}