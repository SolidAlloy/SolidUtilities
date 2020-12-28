namespace SolidUtilities.Editor.Helpers.EditorIconsRelated
{
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Scriptable object that holds references to resources needed for <see cref="EditorIcons"/>. With this database,
    /// we only need to know a GUID of the scriptable object instead of GUIDS or paths to all the resources used
    /// in <see cref="EditorIcons"/>.
    /// </summary>
    internal class EditorIconsDatabase : ScriptableObject
    {
        [UsedImplicitly]
        [SerializeField, Multiline(6)] private string _description;

        public Texture2D TriangleRight;
        [SerializeField] private Material _activeDarkSkin;
        [SerializeField] private Material _activeLightSkin;
        [SerializeField] private Material _highlightedDarkSkin;
        [SerializeField] private Material _highlightedLightSkin;

        public Material Active => DarkSkin ? _activeDarkSkin : _activeLightSkin;

        public Material Highlighted => DarkSkin ? _highlightedDarkSkin : _highlightedLightSkin;

        private static bool DarkSkin => EditorGUIUtility.isProSkin;
    }
}