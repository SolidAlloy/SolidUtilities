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
        public static EditorIcon TriangleRight => GetEditorIcon(ref _triangleRight, EditorIconsDatabase.TriangleRight);
        private static EditorIcon _triangleRight;

        /// <summary>Triangle with one of the vertices looking to the bottom. Useful in foldout menus.</summary>
        public static EditorIcon TriangleDown
        {
            get
            {
                // not using GetEditorIcon here because it would rotate the texture each time the parameter is passed into the method.
                if (_triangleDown.Default == null)
                {
                    _triangleDown.Dispose();
                    _triangleDown = new EditorIcon(EditorIconsDatabase.TriangleRight.Rotate());
                }

                return _triangleDown;
            }
        }
        private static EditorIcon _triangleDown;

        public static EditorIcon AddButtonS => GetEditorIcon(ref _addButtonS, EditorIconsDatabase.ToolbarPlusS);
        private static EditorIcon _addButtonS;

        public static EditorIcon AddButtonI => GetEditorIcon(ref _addButtonI, EditorIconsDatabase.ToolbarPlusI);
        private static EditorIcon _addButtonI;

        static EditorIcons()
        {
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

        private static void DisposeOfEditorIcons()
        {
            _triangleRight.Dispose();
            _triangleDown.Dispose();
            _addButtonS.Dispose();
            _addButtonI.Dispose();
        }

        private static EditorIcon GetEditorIcon(ref EditorIcon editorIcon, Texture2D originalIcon)
        {
            if (editorIcon.Default == null)
            {
                editorIcon.Dispose();
                editorIcon = new EditorIcon(originalIcon);
            }

            return editorIcon;
        }
    }
}