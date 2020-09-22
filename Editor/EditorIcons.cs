namespace SolidUtilities.Editor
{
    using EditorIconsRelated;
    using Extensions;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Assertions;

    /// <summary>
    /// Collection of icons to use for creating custom inspectors and drawers. Icons can have different tints
    /// depending on their state: active, highlighted, etc.
    /// </summary>
    public static class EditorIcons
    {
        /// <summary>
        /// Scriptable object that holds references to textures, materials, and other resources used in <see cref="EditorIcons"/>.
        /// </summary>
        internal static readonly EditorIconsDatabase Database = GetDatabase();

        /// <summary>The default Unity info icon.</summary>
        public static readonly Texture2D Info = (Texture2D) EditorGUIUtility.Load("console.infoicon");

        /// <summary>Triangle with one of the vertices looking to the right. Useful in foldout menus.</summary>
        public static readonly EditorIcon TriangleRight = new EditorIcon(Database.TriangleRight);

        /// <summary>Triangle with one of the vertices looking to the bottom. Useful in foldout menus.</summary>
        public static readonly EditorIcon TriangleDown = new EditorIcon(Database.TriangleRight.Rotate());

        private static EditorIconsDatabase GetDatabase()
        {
            const string databaseGuid = "77b69d89a67a8ad4f80ee7ffcabd3c20";
            string databasePath = AssetDatabase.GUIDToAssetPath(databaseGuid);
            var database = AssetDatabase.LoadAssetAtPath<EditorIconsDatabase>(databasePath);
            Assert.IsNotNull(database);
            return database;
        }
    }
}