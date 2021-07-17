namespace SolidUtilities.Editor.Helpers
{
    using UnityEngine;

    public static class GUIContentHelper
    {
        private static readonly GUIContent _content = new GUIContent();

        public static GUIContent Temp(string text)
        {
            _content.text = text;
            return _content;
        } 
    }
}