namespace SolidUtilities.Editor.Helpers
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ContentCache
    {
        private readonly Dictionary<string, GUIContent> _contentCache = new Dictionary<string, GUIContent>();

        /// <summary>
        /// Get cached GUIContent or create a new one and cache it.
        /// </summary>
        /// <param name="text">Text in GUIContent.</param>
        /// <returns>GUIContent instance containing the text.</returns>
        public GUIContent GetItem(string text)
        {
            if (_contentCache.TryGetValue(text, out GUIContent content))
                return content;

            content = new GUIContent(text);
            _contentCache.Add(text, content);
            return content;
        }
    }
}