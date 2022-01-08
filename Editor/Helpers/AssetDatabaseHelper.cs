namespace SolidUtilities.Editor
{
    using System;
    using Editor;
    using JetBrains.Annotations;
    using UnityEditor;

    public static class AssetDatabaseHelper
    {
        /// <summary>
        /// Retrieves type of the class located in an asset with the matching <paramref name="guid"/>.
        /// </summary>
        /// <param name="guid">The GUID of an asset to search for.</param>
        /// <returns>Type of the class located in an asset with the matching <paramref name="guid"/>.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static Type GetTypeFromGUID(string guid)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(assetPath))
                return null;

            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);

            return script == null ? null : script.GetClassType();
        }
    }
}