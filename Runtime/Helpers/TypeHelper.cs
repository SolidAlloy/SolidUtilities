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
        [PublicAPI] public static Type MakeGenericTypeDefinition(Type typeToCheck)
        {
            return typeToCheck.IsGenericTypeDefinition ? typeToCheck : typeToCheck.GetGenericTypeDefinition();
        }
    }
}