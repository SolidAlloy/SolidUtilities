namespace SolidUtilities.Editor.Helpers
{
    using JetBrains.Annotations;
    using UnityEditor.PackageManager;

    public static class PackageSearcher
    {
        [PublicAPI, CanBeNull]
        public static PackageInfo FindPackageByName(string packageName) =>
            PackageInfo.FindForAssetPath($"Packages/{packageName}");
    }
}