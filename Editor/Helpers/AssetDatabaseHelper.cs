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
        
        /// <summary>
        /// Returns a unique GUID that is known to have no conflicts with the existing assets, so that you create a new asset manually.
        /// </summary>
        /// <returns>A unique asset GUID.</returns>
        public static string GetUniqueGUID()
        {
            GUID newGUID;

            do
            {
                newGUID = GUID.Generate();
            }
            while ( ! string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(newGUID.ToString())));

            return newGUID.ToString();
        }

        /// <summary>
        /// Completely prevents AssetDatabase from importing assets or refreshin.
        /// </summary>
        /// <returns>Instance of a disposable struct.</returns>
        [PublicAPI]
        public static DisabledAssetDatabase DisabledScope() => new DisabledAssetDatabase(default);
        
        /// <summary>
        /// Completely prevents AssetDatabase from importing assets or refreshing.
        /// </summary>
        public readonly struct DisabledAssetDatabase : IDisposable
        {
            public DisabledAssetDatabase(bool _)
            {
                EditorApplication.LockReloadAssemblies();
                AssetDatabase.DisallowAutoRefresh();
                AssetDatabase.StartAssetEditing();
            }

            public void Dispose()
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.AllowAutoRefresh();
                EditorApplication.UnlockReloadAssemblies();
            }
        }
    }
}