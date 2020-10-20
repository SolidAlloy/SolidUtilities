namespace SolidUtilities.Editor.Extensions
{
    using System;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine.Assertions;

    public static class MonoScriptExtensions
    {
        /// <summary>
        /// Returns the <see cref="Type"/> of the class implemented by this script. Works with generic classes too
        /// (the file must be named by the "GenericClass`1.cs" template).
        /// </summary>
        /// <param name="script">The script to get the type from.</param>
        /// <returns>The <see cref="Type"/> of the class implemented by this script or <see langword="null"/>,
        /// if the type was not found.</returns>
        [PublicAPI, CanBeNull] public static Type GetClassType(this MonoScript script)
        {
            Type simpleType = script.GetClass();
            if (simpleType != null)
                return simpleType;

            string className = script.name;

            if (IsNotGeneric(className))
                return null;

            string assemblyName = script.GetAssemblyName();
            Assembly assembly = Assembly.Load(assemblyName);

            string namespaceName = script.GetNamespaceName();
            string fullTypeName = namespaceName == string.Empty ? className : $"{namespaceName}.{className}";

            Type type = assembly.GetType(fullTypeName);
            return type;
        }

        /// <summary>Returns the assembly name of the class implemented by this script.</summary>
        /// <param name="script">The script to search for assembly in.</param>
        /// <returns>
        /// The assembly name without the .dll extension, or an empty string if the assembly was not found.
        /// </returns>
        [PublicAPI, NotNull] public static string GetAssemblyName(this MonoScript script)
        {
            MethodInfo getAssemblyNameMethod = typeof(MonoScript)
                .GetMethod("GetAssemblyName", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(getAssemblyNameMethod);
            string assemblyName = (string) getAssemblyNameMethod.Invoke(script, new object[0]);
            string assemblyNameWithoutExtension = assemblyName.Split('.')[0];
            return assemblyNameWithoutExtension;
        }

        private static string GetNamespaceName(this MonoScript asset)
        {
            string content = asset.text;
            var regex = new Regex(@"(?<=namespace[\s]+)[\w_.-]+", RegexOptions.Compiled);
            Match match = regex.Match(content);
            return match.Success ? match.Value : string.Empty;
        }

        private static bool IsNotGeneric(string typeName) => ! typeName.Contains("`");
    }
}