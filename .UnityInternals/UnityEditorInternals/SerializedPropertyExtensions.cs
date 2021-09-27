namespace SolidUtilities.UnityEditorInternals
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEditor;

    public static class SerializedPropertyExtensions
    {
        /// <summary> Checks if property has custom drawer. </summary>
        /// <param name="property">The property to check.</param>
        /// <returns> <c>true</c> if the <paramref name="property"/> has a custom drawer. </returns>
        [PublicAPI]
        public static bool HasCustomPropertyDrawer(this SerializedProperty property)
        {
            return ScriptAttributeUtility.GetHandler(property).propertyDrawer != null;
        }

        [PublicAPI]
        public static (FieldInfo FieldInfo, Type Type) GetFieldInfoAndType(this SerializedProperty property)
        {
            var fieldInfo = ScriptAttributeUtility.GetFieldInfoFromProperty(property, out Type type);
            return (fieldInfo, type);
        }
    }
}