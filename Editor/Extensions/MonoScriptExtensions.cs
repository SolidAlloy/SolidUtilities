namespace SolidUtilities.Editor.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;
    using SolidUtilities.Extensions;
    using UnityEditor;
    using UnityEditorInternals;

    public static class MonoScriptExtensions
    {
        private static readonly Regex _namespaceNameRegex = new Regex(@"(?<=namespace[\s]+)[\w_.-]+", RegexOptions.Compiled);

        /// <summary>
        /// Returns the <see cref="Type"/> of the class implemented by this script. Works for types not derived from
        /// <see cref="UnityEngine.Object"/> and generic classes (the file must be named by the "GenericClass`1.cs" template).
        /// </summary>
        /// <param name="script">The script to get the type from.</param>
        /// <returns>The <see cref="Type"/> of the class implemented by this script or <see langword="null"/>,
        /// if the type was not found.</returns>
        [PublicAPI, CanBeNull] public static Type GetClassType(this MonoScript script)
        {
            Type simpleType = script.GetClass();
            if (simpleType != null)
                return simpleType;

            string className = GetFirstClassFromText(script.text);

            if (string.IsNullOrEmpty(className))
                return null;

            string assemblyName = script.GetAssemblyName();
            Assembly assembly;

            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch (Exception e)
            {
                // Whatever caused this exception, the type cannot be loaded, so disregard it as null.
                if (e is FileNotFoundException || e is FileLoadException)
                    return null;

                throw;
            }

            string namespaceName = script.GetNamespaceName();
            string fullTypeName = namespaceName == string.Empty ? className : $"{namespaceName}.{className}";

            Type type = assembly.GetType(fullTypeName);
            return type;
        }

        private static string GetFirstClassFromText(string text)
        {
            const string classNameRegex = @"(?<=(class )|(struct )).*?(?=(\s|\n)*(:|{))";
            string className = Regex.Match(text, classNameRegex).Value;

            if (string.IsNullOrEmpty(className))
                return className;

            if ( ! className.Contains("<"))
                return className;

            int argsCount = className.CountChars(',') + 1;
            int bracketIndex = className.IndexOf('<');
            return $"{className.Substring(0, bracketIndex)}`{argsCount.ToString()}";
        }

        /// <summary>Returns the assembly name of the class implemented by this script.</summary>
        /// <param name="script">The script to search for assembly in.</param>
        /// <returns>
        /// The assembly name without the .dll extension, or an empty string if the assembly was not found.
        /// </returns>
        [PublicAPI, NotNull] public static string GetAssemblyName(this MonoScript script)
        {
            string assemblyName = script.Internal_GetAssemblyName();
            int lastDotIndex = assemblyName.LastIndexOf('.');
            return lastDotIndex == -1 ? string.Empty : assemblyName.Substring(0, lastDotIndex);
        }

        private static string GetNamespaceName(this MonoScript asset)
        {
            string content = asset.text;
            Match match = _namespaceNameRegex.Match(content);
            return match.Success ? match.Value : string.Empty;
        }
    }
}