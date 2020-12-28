namespace SolidUtilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>Different helper functions for <see cref="System.Type"/></summary>
    public static class TypeHelper
    {
        private static readonly Dictionary<string, string> BuiltInTypes = new Dictionary<string, string>
        {
            { "System.Boolean", "bool" },
            { "System.Byte", "byte" },
            { "System.SByte", "sbyte" },
            { "System.Char", "char" },
            { "System.Decimal", "decimal" },
            { "System.Double", "double" },
            { "System.Single", "float" },
            { "System.Int32", "int" },
            { "System.UInt32", "uint" },
            { "System.Int64", "long" },
            { "System.UInt64", "ulong" },
            { "System.Int16", "short" },
            { "System.UInt16", "ushort" },
            { "System.Object", "object" },
            { "System.String", "string" }
        };

        /// <summary>Makes the type a generic type definition if it is not.</summary>
        /// <param name="typeToCheck">The type to get generic type definition from.</param>
        /// <returns>A type that was made GenericTypeDefinition.</returns>
        /// <example><code>
        /// void UseGenericType(Type genericType)
        /// {
        ///     genericType = TypeHelper.MakeSureIsGenericTypeDefinition(genericType);
        ///     // Use genericType
        /// }
        ///
        /// UseGenericType(typeof(GenericClass&lt;>)); // is the right way to use the method
        /// UseGenericType(typeof(GenericClass&lt;int>)); // but this works too now
        /// </code></example>
        [PublicAPI, Pure] public static Type MakeGenericTypeDefinition(Type typeToCheck)
        {
            return typeToCheck.IsGenericTypeDefinition ? typeToCheck : typeToCheck.GetGenericTypeDefinition();
        }

        /// <summary>
        /// Returns a type name without restricted characters. It can then be used as a class/field name.
        /// </summary>
        /// <param name="rawTypeName">Short or full name of the type.</param>
        /// <returns>A type name without restricted characters.</returns>
        [PublicAPI, Pure]
        public static string MakeClassFriendly(this string rawTypeName)
        {
            return rawTypeName
                .Replace('.', '_')
                .Replace('`', '_');
        }

        /// <summary>
        /// Strips the generic suffix from <paramref name="typeName"/> and leaves only the type name.
        /// It can be something like `1 or _2.
        /// </summary>
        /// <param name="typeName">The name of the type to strip the suffix from.</param>
        /// <returns>Type name without a generic suffix.</returns>
        [PublicAPI, Pure]
        public static string StripGenericSuffix(this string typeName)
        {
            char[] separators = { '_', '`' };
            int separatorIndex = typeName.LastIndexOfAny(separators);

            if (separatorIndex == -1 || typeName.Length == separatorIndex + 1)
                return typeName;

            return char.IsDigit(typeName[separatorIndex + 1])
                ? typeName.Substring(0, separatorIndex)
                : typeName;
        }

        /// <summary>
        /// Returns a built-in name for <paramref name="fullTypeName"/> if the built-in analogue exists.
        /// </summary>
        /// <param name="fullTypeName">Full name of the type.</param>
        /// <param name="withFolder">Whether to prepend the built-in type with "Built-in.".</param>
        /// <returns>A built-in name for <paramref name="fullTypeName"/> if the built-in analogue exists.</returns>
        /// <example><code>
        /// string intName = typeof(System.Int32).FullName;
        /// Debug.Log(intName.TryReplaceWithBuiltInName()); // prints "Built-in.int"
        ///
        /// string intName = typeof(System.Int32).FullName;
        /// Debug.Log(intName.TryReplaceWithBuiltInName(true)); // prints "int"
        /// </code></example>
        [PublicAPI, Pure]
        public static string ReplaceWithBuiltInName(this string fullTypeName, bool withFolder = false)
        {
            const string builtInTypesPrefix = "Built-in.";

            if (BuiltInTypes.TryGetValue(fullTypeName, out string builtInName))
            {
                return withFolder ? builtInTypesPrefix + builtInName : builtInName;
            }
            else
            {
                return fullTypeName;
            }
        }
    }
}