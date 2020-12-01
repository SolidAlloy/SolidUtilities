namespace SolidUtilities.Editor.Helpers
{
    using System;
    using System.Linq;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    /// <summary>Various methods that simplify or extend <see cref="AssetDatabase"/> usage.</summary>
    public static class AssetDatabaseHelper
    {
        private static readonly string DataPath = Application.dataPath;

        /// <summary>
        /// Creates folders in the path if they do not exist. The parent folder of the path is considered the Assets
        /// folder. The path does not have to start with the Assets folder.
        /// </summary>
        /// <param name="folderPath">
        /// The path to a folder. The parent folder of the path is considered the Assets folder.
        /// </param>
        /// <example><code>
        /// string fullAssetPath = $"{Application.dataPath}/Scripts/GenericScriptableObjects/Generic_{className}.cs";
        /// AssetDatabaseHelper.MakeSureFolderExists("Scripts/GenericScriptableObjects");
        /// File.WriteAllText(fullAssetPath, template);
        /// AssetDatabase.Refresh();
        /// </code></example>
        [PublicAPI] public static void MakeSureFolderExists(string folderPath)
        {
            folderPath = folderPath.Trim('/');
            var folders = folderPath.Split('/');

            for (int i = 0; i < folders.Length; i++)
            {
                string folderToCreate = folders[i];
                string parentFolders = $"Assets/{string.Join("/", folders.Take(i))}".TrimEnd('/');

                if (!AssetDatabase.IsValidFolder($"{parentFolders}/{folderToCreate}"))
                    AssetDatabase.CreateFolder(parentFolders, folderToCreate);
            }
        }

        /// <summary>Transforms a path relative to the Unity Assets folder into an absolute path.</summary>
        /// <param name="relativeAssetPath">Path relative to the Unity Assets folder.</param>
        /// <returns>Absolute path.</returns>
        /// <exception cref="ArgumentException">If <paramref name="relativeAssetPath"/> does not start with 'Assets/'.</exception>
        [PublicAPI, Pure]
        public static string RelativeToAbsolutePath(string relativeAssetPath)
        {
            if (relativeAssetPath.Substring(0, 6) != "Assets")
            {
                throw new ArgumentException($"The method supports only paths that start with 'Assets'. Received {relativeAssetPath} instead.");
            }

            string relativePath = relativeAssetPath.Substring(6);
            string absolutePath = $"{DataPath}{relativePath}";
            return absolutePath;
        }

        /// <summary>
        /// Transforms GUID into an absolute path. Works only with assets located in the "Assets" folder.
        /// </summary>
        /// <param name="guid">GUID to search for.</param>
        /// <returns>Absolute path to the asset.</returns>
        /// <exception cref="ArgumentException">If asset with such GUID was not found.</exception>
        [PublicAPI]
        public static string GUIDToAbsolutePath(string guid)
        {
            string relativeAssetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (relativeAssetPath == string.Empty)
            {
                throw new ArgumentException($"No asset found with the following GUID: {guid}");
            }

            return RelativeToAbsolutePath(relativeAssetPath);
        }
    }
}