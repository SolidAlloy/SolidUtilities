namespace SolidUtilities.Editor.Helpers
{
    using System.Linq;
    using JetBrains.Annotations;
    using UnityEditor;

    /// <summary>Various methods that simplify <see cref="AssetDatabase"/> usage.</summary>
    public static class AssetDatabaseHelper
    {
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
    }
}