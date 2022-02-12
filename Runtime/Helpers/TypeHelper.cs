namespace SolidUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>Different helper functions for <see cref="System.Type"/></summary>
    public static class TypeHelper
    {
        private static readonly Dictionary<string, string> _builtInTypes = new Dictionary<string, string>
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
        /// Strips the generic suffix from <paramref name="typeName"/> and leaves only the type name itself.
        /// </summary>
        /// <param name="typeName">The name of the type to strip the suffix from.</param>
        /// <returns>Type name without a generic suffix.</returns>
        [PublicAPI, Pure]
        public static string StripGenericSuffix(this string typeName)
        {
            int separatorIndex = typeName.IndexOf('`');
            return separatorIndex == -1 ? typeName : typeName.Substring(0, separatorIndex);
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

            if (_builtInTypes.TryGetValue(fullTypeName, out string builtInName))
            {
                return withFolder ? builtInTypesPrefix + builtInName : builtInName;
            }
            else
            {
                return fullTypeName;
            }
        }
        
        public static string GetNiceNameOfGenericType(Type genericType, bool fullName = false)
        {
            return genericType.IsGenericTypeDefinition
                ? GetNiceNameOfGenericTypeDefinition(genericType, fullName)
                : GetNiceNameOfGenericTypeWithArgs(genericType, fullName);
        }

        public static string[] GetNiceArgsOfGenericType(Type genericType)
        {
            return genericType.IsGenericTypeDefinition
                ? GetNiceArgsOfGenericTypeDefinition(genericType)
                : GetNiceArgsOfGenericTypeWithArgs(genericType);
        }

        /// <summary>
        /// Gets a type name for nice representation of the type. It looks like this: ClassName&lt;T1,T2>.
        /// </summary>
        private static string GetNiceNameOfGenericTypeDefinition(Type genericTypeWithoutArgs, bool fullName)
        {
            string typeNameWithoutBrackets = fullName
                ? genericTypeWithoutArgs.FullName.StripGenericSuffix()
                : genericTypeWithoutArgs.Name.StripGenericSuffix();

            var argumentNames = GetNiceArgsOfGenericTypeDefinition(genericTypeWithoutArgs);
            return $"{typeNameWithoutBrackets}<{string.Join(",", argumentNames)}>";
        }

        /// <summary>
        /// Gets a type name for nice representation of the type. It looks like this: ClassName&lt;int,TestArg>.
        /// </summary>
        private static string GetNiceNameOfGenericTypeWithArgs(Type genericTypeWithArgs, bool fullName)
        {
            string typeNameWithoutSuffix = fullName
                ? genericTypeWithArgs.FullName.StripGenericSuffix()
                : genericTypeWithArgs.Name.StripGenericSuffix();

            var argumentNames = GetNiceArgsOfGenericTypeWithArgs(genericTypeWithArgs);

            return $"{typeNameWithoutSuffix}<{string.Join(",", argumentNames)}>";
        }

        private static string[] GetNiceArgsOfGenericTypeDefinition(Type genericTypeWithoutArgs)
        {
            Type[] genericArgs = genericTypeWithoutArgs.GetGenericArguments();
            return genericArgs.Select(argument => argument.Name).ToArray();
        }

        private static string[] GetNiceArgsOfGenericTypeWithArgs(Type genericTypeWithArgs)
        {
            return genericTypeWithArgs.GetGenericArguments()
                .Select(argument => argument.FullName)
                .Select(argFullName => argFullName.ReplaceWithBuiltInName())
                .Select(argFullName => argFullName.GetSubstringAfterLast('.'))
                .ToArray();
        }
    }
}