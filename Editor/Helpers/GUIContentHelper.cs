namespace SolidUtilities.Editor
{
    using UnityEngine;

    public static class GUIContentHelper
    {
        private static readonly GUIContent _content = new GUIContent();

        public static GUIContent Temp(string text, Texture icon = null)
        {
            _content.text = text;
            _content.image = icon;
            _content.tooltip = string.Empty;
            return _content;
        }
        
        public static GUIContent Temp(string text, string tooltip)
        {
            _content.text = text;
            _content.tooltip = tooltip;
            _content.image = null;
            return _content;
        }
    }
}