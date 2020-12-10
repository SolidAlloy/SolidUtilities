namespace SolidUtilities.Helpers
{
    using System;
    using JetBrains.Annotations;

    /// <summary>Different helper functions for <see cref="System.Type"/></summary>
    public static class TypeHelper
    {
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
    }
}