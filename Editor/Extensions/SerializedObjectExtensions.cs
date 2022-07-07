namespace SolidUtilities.Editor
{
    using UnityEditor;
    using UnityEngine;

    public static class SerializedObjectExtensions
    {
        public static void SetHideFlagsPersistently(this SerializedObject serializedObject, HideFlags flags)
        {
            // The only way to set the hide flags persistently.
            var hideFlagsProp = serializedObject.FindProperty("m_ObjectHideFlags");
            int flagsValue = (int) flags;

            if (hideFlagsProp.intValue != flagsValue)
            {
                hideFlagsProp.intValue = flagsValue;
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}