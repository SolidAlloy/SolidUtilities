namespace SolidUtilities.Editor
{
    using System.Linq;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;
    using PackageInfo = UnityEditor.PackageManager.PackageInfo;

    public static class PackageSearcher
    {
        /// <summary>
        /// Finds a package by its <see cref="PackageInfo.name"/> or <see cref="PackageInfo.displayName"/>.
        /// </summary>
        /// <param name="packageName">
        /// <see cref="PackageInfo.name"/> or <see cref="PackageInfo.displayName"/> of the package to search for.
        /// </param>
        /// <returns>Package Info object of the package or <c>null</c> if the package was not found.</returns>
        [PublicAPI, CanBeNull]
        public static PackageInfo FindPackageByName(string packageName)
        {
            if (packageName.Substring(0, 4) == "com.")
                return PackageInfo.FindForAssetPath($"Packages/{packageName}");

            return AssetDatabase.FindAssets("package")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(packageJsonPath => AssetDatabase.LoadAssetAtPath<TextAsset>(packageJsonPath) != null)
                .Select(PackageInfo.FindForAssetPath)
                .FirstOrDefault(x => x.displayName == packageName);
        }
    }
}