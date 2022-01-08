namespace SolidUtilities.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;
    using SolidUtilities;
    using UnityEditor;
    using UnityEditorInternals;
    using UnityEngine.Assertions;

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

        public static SerializedProperty GetParent(this SerializedProperty property)
        {
            if (!property.propertyPath.Contains('.'))
                return null;

            var parentPropertyPath = property.propertyPath.GetSubstringBeforeLast('.');
            if (parentPropertyPath.EndsWith(".Array"))
            {
                parentPropertyPath = parentPropertyPath.Substring(0, parentPropertyPath.Length - 6);
            }

            if (string.IsNullOrEmpty(parentPropertyPath))
                return null;

            return property.serializedObject.FindProperty(parentPropertyPath);
        }

        /// <summary>Gets type of the object serialized by the <paramref name="property"/>.</summary>
        /// <param name="property">The property whose type to find.</param>
        /// <returns>Type of the object serialized by <paramref name="property"/>.</returns>
        [NotNull, PublicAPI]
        public static Type GetObjectType(this SerializedProperty property) => property.GetFieldInfoAndType().Type;

        [PublicAPI]
        public static FieldInfo GetFieldInfo(this SerializedProperty property) => property.GetFieldInfoAndType().FieldInfo;

        public static T GetObject<T>(this SerializedProperty property) => (T) property.GetObject();

        public static object GetObject(this SerializedProperty property)
        {
            var propertyPaths = property.propertyPath.Split('.');

            var currentProperty = property.serializedObject.FindProperty(propertyPaths[0]);
            var fieldInfo = currentProperty.GetFieldInfo();
            object target = fieldInfo.GetValue(property.serializedObject.targetObject);

            foreach (string path in propertyPaths.Skip(1))
            {
                if (path == "Array")
                {
                    continue;
                }

                if (path.StartsWith("data["))
                {
                    int index = int.Parse(path[5].ToString());
                    currentProperty = currentProperty.GetArrayElementAtIndex(index);
                    target = ((IList) target)[index];
                }
                else
                {
                    currentProperty = currentProperty.FindPropertyRelative(path);
                    fieldInfo = currentProperty.GetFieldInfo();
                    target = fieldInfo.GetValue(target);
                }
            }

            return target;
        }
    }
}