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
            return _content;
        }
    }
}