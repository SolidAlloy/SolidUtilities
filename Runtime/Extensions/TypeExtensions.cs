namespace SolidUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>Different useful extensions for <see cref="System.Type"/>.</summary>
    public static class TypeExtensions
    {
        private static readonly HashSet<Type> _unitySerializablePrimitiveTypes = new HashSet<Type>
        {
            typeof(bool), typeof(byte), typeof(sbyte), typeof(char), typeof(double), typeof(float), typeof(int),
            typeof(uint), typeof(long), typeof(ulong), typeof(short), typeof(ushort), typeof(string)
        };

        private static readonly HashSet<Type> _unitySerializableBuiltinTypes = new HashSet<Type>
        {
            typeof(Vector2), typeof(Vector3), typeof(Vector4), typeof(Rect), typeof(Quaternion), typeof(Matrix4x4),
            typeof(Color), typeof(Color32), typeof(LayerMask), typeof(AnimationCurve), typeof(Gradient),
            typeof(RectOffset), typeof(GUIStyle)
        };

        /// <summary>Finds a field recursively in the fields of a class.</summary>
        /// <param name="parentType">The class type to start the search from.</param>
        /// <param name="path">The path to a field, separated by dot.</param>
        /// <param name="flags">Custom flags to search the field.</param>
        /// <returns>Field info if the field is found, and null if not.</returns>
        /// <example><code>
        /// FieldInfo nestedField = targetType.GetFieldAtPath("parentField.nestedField");
        /// Debug.Log((string)nestedField.GetValue(obj));
        /// </code></example>
        [PublicAPI]
        public static FieldInfo GetFieldAtPath(this Type parentType, string path,
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            FieldInfo field = null;

            foreach (string part in path.Split('.'))
            {
                field = parentType.GetFieldRecursive(part, flags);
                if (field == null)
                    return null;

                parentType = field.FieldType;
            }

            return field;
        }

        /// <summary> Searches for the specified field in the type and all its base types. </summary>
        /// <param name="type">The type to search in.</param>
        /// <param name="name">The name of the field.</param>
        /// <param name="flags">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" />
        /// that specify how the search is conducted.</param>
        /// <returns>The field found in the class hierarchy, or null if the field was not found.</returns>
        /// <exception cref="NullReferenceException"><paramref name="name"/> is null.</exception>
        [PublicAPI]
        public static FieldInfo GetFieldRecursive(this Type type, string name, BindingFlags flags)
        {
            while (true)
            {
                FieldInfo field = type.GetField(name, flags);

                if (field != null)
                    return field;

                Type baseType = type.BaseType;

                if (baseType == null)
                    return null;

                type = baseType;
            }
        }

        /// <summary>
        /// Collects all the serializable fields of a class: private ones with SerializeField attribute and public ones.
        /// </summary>
        /// <param name="type">Class type to collect the fields from.</param>
        /// <returns>Collection of the serializable fields of a class.</returns>
        /// <example><code>
        /// var fields = objectType.GetSerializedFields();
        /// foreach (var field in fields)
        /// {
        ///     string fieldLabel = ObjectNames.NicifyVariableName(field.Name);
        ///     object fieldValue = field.GetValue(serializedObject);
        ///     object newValue = DrawField(fieldLabel, fieldValue);
        ///     field.SetValue(serializedObject, newValue);
        /// }
        /// </code></example>
        [PublicAPI] public static IEnumerable<FieldInfo> GetSerializedFields(this Type type)
        {
            const BindingFlags instanceFilter = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var instanceFields = type.GetFields(instanceFilter);
            return instanceFields.Where(field => field.IsPublic || field.HasAttribute<SerializeField>());
        }

        /// <summary>Checks whether the type is nullable.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is nullable.</returns>
        [PublicAPI] public static bool IsNullable(this Type type)
        {
            return ! type.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Checks whether the type is derivative of a generic class without specifying its type parameter.
        /// </summary>
        /// <param name="typeToCheck">The type to check.</param>
        /// <param name="generic">The generic class without type parameter.</param>
        /// <returns>True if the type is subclass of the generic class.</returns>
        /// <example><code>
        /// class Base&lt;T> { }
        /// class IntDerivative : Base&lt;int> { }
        /// class StringDerivative : Base&lt;string> { }
        ///
        /// bool intIsSubclass = typeof(IntDerivative).IsSubclassOfRawGeneric(typeof(Base&lt;>)); // true
        /// bool stringIsSubclass = typeof(StringDerivative).IsSubclassOfRawGeneric(typeof(Base&lt;>)); // true
        /// </code></example>
        [PublicAPI] public static bool IsSubclassOfRawGeneric(this Type typeToCheck, Type generic)
        {
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                Type cur = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;

                if (generic == cur)
                    return true;

                typeToCheck = typeToCheck.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the type inherits from the base type.
        /// </summary>
        /// <param name="typeToCheck">The type to check.</param>
        /// <param name="baseType">
        /// The base type to check inheritance from. It can be a generic type without the type parameter.
        /// </param>
        /// <returns>Whether <paramref name="typeToCheck"/>> inherits <paramref name="baseType"/>.</returns>
        /// <example><code>
        /// class Base&lt;T> { }
        /// class IntDerivative : Base&lt;int> { }
        ///
        /// bool isAssignableWithTypeParam = typeof(typeof(Base&lt;int>).IsAssignableFrom(IntDerivative)); // true
        /// bool isAssignableWithoutTypeParam = typeof(typeof(Base&lt;>)).IsAssignableFrom(IntDerivative); // false
        /// bool inherits = typeof(IntDerivative).InheritsFrom(typeof(Base&lt;>)); // true
        /// </code></example>
        [PublicAPI] public static bool InheritsFrom(this Type typeToCheck, Type baseType)
        {
            bool subClassOfRawGeneric = baseType.IsGenericType && typeToCheck.IsSubclassOfRawGeneric(baseType);

            return baseType.IsAssignableFrom(typeToCheck) || subClassOfRawGeneric;
        }

        /// <summary>Checks if the type is serializable by Unity.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/> if the type can be serialized by Unity.</returns>
        [PublicAPI] public static bool IsUnitySerializable(this Type type)
        {
            bool IsSystemType(Type typeToCheck) => typeToCheck.Namespace != null && typeToCheck.Namespace.StartsWith("System");

            bool IsCustomSerializableType(Type typeToCheck) => typeToCheck.IsSerializable && !IsSystemType(typeToCheck);

            // the latter check is for static classes. https://stackoverflow.com/questions/1175888/determine-if-a-type-is-static
            if (type.IsInterface || (type.IsAbstract && type.IsSealed))
                return false;

            if (IsCustomSerializableType(type))
                return true;

            if (type.InheritsFrom(typeof(UnityEngine.Object)) && ! type.IsGenericTypeDefinition)
                return true;

            if (type.IsEnum)
                return true;

            return _unitySerializablePrimitiveTypes.Contains(type) || _unitySerializableBuiltinTypes.Contains(type);
        }

        /// <summary>Checks whether the type has no fields, methods, and other members.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/> if the type is empty.</returns>
        [PublicAPI, Pure]
        public static bool IsEmpty(this Type type)
        {
            // One found member is the default constructor.
            return type.GetMembers((BindingFlags) (-1)).Length == 1;
        }

        /// <summary>
        /// Gets the short name of the assembly where the type is defined. For example, typeof(string) will return "mscorlib".
        /// </summary>
        /// <param name="type">The type which assembly name to search for.</param>
        /// <returns>Assembly name without the .dll extension.</returns>
        [PublicAPI, Pure]
        public static string GetShortAssemblyName(this Type type)
        {
            string assemblyFullName = type.Assembly.FullName;
            int commaIndex = assemblyFullName.IndexOf(',');
            return assemblyFullName.Substring(0, commaIndex);
        }
    }
}