namespace SolidUtilities.Editor
{
    using SolidUtilities;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Assertions;

    /// <summary>
    /// Collection of icons to use for creating custom inspectors and drawers. Icons can have different tints
    /// depending on their state: active, highlighted, etc.
    /// </summary>
    /// <example><code>
    /// var messageContent = new GUIContent(message, EditorIcons.Info);
    /// EditorIcon triangleIcon = Expanded ? EditorIcons.TriangleDown : EditorIcons.TriangleRight;
    /// </code></example>
    public static class EditorIcons
    {
        /// <summary>
        /// Scriptable object that holds references to textures, materials, and other resources used in <see cref="EditorIcons"/>.
        /// </summary>
        internal static readonly EditorIconsDatabase Database = GetDatabase();

        /// <summary>The default Unity info icon.</summary>
        public static readonly Texture2D Info = (Texture2D) EditorGUIUtility.Load("console.infoicon");

        public static readonly Texture2D Error = (Texture2D) EditorGUIUtility.Load("console.erroricon");

        /// <summary>Triangle with one of the vertices looking to the right. Useful in foldout menus.</summary>
        public static EditorIcon TriangleRight { get; private set; }

        /// <summary>Triangle with one of the vertices looking to the bottom. Useful in foldout menus.</summary>
        public static EditorIcon TriangleDown { get; private set; }

        public static EditorIcon AddButtonS { get; private set; }

        public static EditorIcon AddButtonI { get; private set; }

        static EditorIcons()
        {
            CreateEditorIcons();
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            AssemblyReloadEvents.beforeAssemblyReload += DisposeOfEditorIcons;
        }

        private static EditorIconsDatabase GetDatabase()
        {
            const string databaseGuid = "86b4b7622f8a9fc4382b4c179f1e601a";
            string databasePath = AssetDatabase.GUIDToAssetPath(databaseGuid);
            var database = AssetDatabase.LoadAssetAtPath<EditorIconsDatabase>(databasePath);
            Assert.IsNotNull(database);
            return database;
        }

        private static void CreateEditorIcons()
        {
            DisposeOfEditorIcons(); // dispose of previous editor icons on play mode exit just in case.
            TriangleRight = new EditorIcon(EditorIconsDatabase.TriangleRight);
            TriangleDown = new EditorIcon(EditorIconsDatabase.TriangleRight.Rotate());
            AddButtonS = new EditorIcon(EditorIconsDatabase.ToolbarPlusS);
            AddButtonI = new EditorIcon(EditorIconsDatabase.ToolbarPlusI);
        }

        private static void DisposeOfEditorIcons()
        {
            TriangleRight.Dispose();
            TriangleDown.Dispose();
            AddButtonS.Dispose();
            AddButtonI.Dispose();
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (stateChange == PlayModeStateChange.EnteredEditMode)
                CreateEditorIcons();
        }
    }
}