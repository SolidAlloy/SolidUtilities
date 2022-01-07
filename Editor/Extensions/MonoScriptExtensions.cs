namespace SolidUtilities.Editor.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;
    using SolidUtilities;
    using UnityEditor;
    using UnityEditorInternals;

    public static class MonoScriptExtensions
    {
        private static readonly Regex _namespaceNameRegex = new Regex(@"(?<=namespace[\s]+)[\w_.-]+", RegexOptions.Compiled);
        private static readonly Regex _classRegex = new Regex(@"(?<=(class )|(struct )).*?(?=(\s|\n)*(:|{))", RegexOptions.Compiled);

        /// <summary>
        /// Returns the <see cref="Type"/> of the class implemented by this script. Works for types not derived from
        /// <see cref="UnityEngine.Object"/> and generic classes (the file must be named by the "GenericClass`1.cs" template).
        /// </summary>
        /// <param name="script">The script to get the type from.</param>
        /// <param name="className">A specific class name to search for.</param>
        /// <returns>The <see cref="Type"/> of the class implemented by this script or <see langword="null"/>,
        /// if the type was not found.</returns>
        [PublicAPI, CanBeNull] public static Type GetClassType(this MonoScript script, string className = null)
        {
            Type simpleType = script.GetClass();
            if (simpleType != null)
                return simpleType;

            string foundClassName = string.IsNullOrEmpty(className) ? GetFirstClassFromText(script.text) : GetFirstClassWithName(script.text, className);

            if (string.IsNullOrEmpty(foundClassName))
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
            string fullTypeName = namespaceName == string.Empty ? foundClassName : $"{namespaceName}.{foundClassName}";

            Type type = assembly.GetType(fullTypeName);
            return type;
        }

        private static string GetFirstClassFromText(string text)
        {
            string className = _classRegex.Match(text).Value;
            return GetProperClassName(className);
        }

        private static string GetFirstClassWithName(string text, string className)
        {
            string pattern = $@"(?<=(class )|(struct )){className}(\s)?(<.*?>)?(?=(\s|\n)*(:|{{))";
            string foundClassName = Regex.Match(text, pattern).Value;
            return GetProperClassName(foundClassName);
        }

        private static string GetProperClassName(string rawClassName)
        {
            if (string.IsNullOrEmpty(rawClassName))
                return rawClassName;

            if ( ! rawClassName.Contains("<"))
                return rawClassName;

            int argsCount = rawClassName.CountChars(',') + 1;
            int bracketIndex = rawClassName.IndexOf('<');
            return $"{rawClassName.Substring(0, bracketIndex)}`{argsCount.ToString()}";
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