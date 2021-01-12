namespace SolidUtilities.UnityEditorInternals
{
    using UnityEditor;
    using UnityEngine;

    public static class EditorGUIUtilityProxy
    {
        public static void SetIconForObject(Object obj, Texture2D icon)
        {
            EditorGUIUtility.SetIconForObject(obj, icon);
        }
    }
}