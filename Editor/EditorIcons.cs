namespace SolidUtilities.Editor
{
    using EditorIconsRelated;
    using Extensions;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Assertions;

    public static class EditorIcons
    {
        public static readonly EditorIconsDatabase Database = GetDatabase();

        public static readonly Texture2D Info = (Texture2D) EditorGUIUtility.Load("console.infoicon");

        public static readonly EditorIcon TriangleDown = new EditorIcon(Database.TriangleRight.Rotate());

        public static readonly EditorIcon TriangleRight = new EditorIcon(Database.TriangleRight);


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