namespace SolidUtilities.Editor.Extensions
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;
    using NUnit.Framework;
    using UnityEditor;

    /// <summary>Different useful extensions for <see cref="SerializedProperty"/>.</summary>
    [PublicAPI]
    public static class SerializedPropertyExtensions
    {
        /// <summary>
        /// Checks whether the serialized property is built-in. <see cref="SerializedObject"/> has a lot of built-in
        /// properties and we are often interested only in the custom ones.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <returns>Whether the property is built-in.</returns>
        public static bool IsBuiltIn(this SerializedProperty property)
        {
            if (property.name == "size" || property.name == "Array")
                return true;

            string firstTwoChars = property.name.Substring(0, 2);
            return firstTwoChars == "m_";
        }

        /// <summary>Gets type of the object serialized by the <paramref name="property"/>.</summary>
        /// <param name="property">The property whose type to find.</param>
        /// <returns>Type of the object serialized by <paramref name="property"/>.</returns>
        [NotNull]
        public static Type GetObjectType(this SerializedProperty property)
        {
            const BindingFlags instanceFieldFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            Type parentType = property.serializedObject.targetObject.GetType();

            const string arraySuffix = ".Array.data";
            string fieldName = property.propertyPath;

            if (fieldName.Contains(arraySuffix))
            {
                int suffixIndex = fieldName.IndexOf(arraySuffix, StringComparison.Ordinal);
                fieldName = fieldName.Substring(0, suffixIndex);
                FieldInfo propertyField = parentType.GetField(fieldName, instanceFieldFlags);
                Assert.IsNotNull(propertyField);
                Type collectionType = propertyField.FieldType;
                Type realType;

                if (collectionType.IsGenericType && collectionType.GetGenericArguments().Length == 1)
                {
                    realType = collectionType.GetGenericArguments()[0];
                }
                else if (collectionType.IsArray)
                {
                    realType = collectionType.GetElementType();
                }
                else
                {
                    throw new ArgumentException("The method does not know how to handle this collection type. " +
                                                "Please contact the author of the plugin to discuss how it can be implemented.");
                }

                Assert.IsNotNull(realType);
                return realType;
            }
            else
            {
                FieldInfo propertyField = parentType.GetField(fieldName, instanceFieldFlags);
                Assert.IsNotNull(propertyField);
                return propertyField.FieldType;
            }
        }
    }
}