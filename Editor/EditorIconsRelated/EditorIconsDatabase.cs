namespace SolidUtilities.Editor.EditorIconsRelated
{
    using UnityEditor;
    using UnityEngine;

    public class EditorIconsDatabase : ScriptableObject
    {
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