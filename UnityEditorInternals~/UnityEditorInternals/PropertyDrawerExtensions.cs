namespace SolidUtilities.UnityEditorInternals
{
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    public static class PropertyDrawerExtensions
    {
        public static PropertyAttribute GetAttribute(this PropertyDrawer propertyDrawer) => propertyDrawer.m_Attribute;

        public static void SetAttribute(this PropertyDrawer propertyDrawer, PropertyAttribute value) => propertyDrawer.m_Attribute = value;

        public static FieldInfo GetFieldInfo(this PropertyDrawer propertyDrawer) => propertyDrawer.m_FieldInfo;

        public static void SetFieldInfo(this PropertyDrawer propertyDrawer, FieldInfo fieldInfo) => propertyDrawer.m_FieldInfo = fieldInfo;
    }
}