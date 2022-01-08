namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

#pragma warning disable 0649

    /// <summary>
    /// Scriptable object that holds references to resources needed for <see cref="EditorIcons"/>. With this database,
    /// we only need to know a GUID of the scriptable object instead of GUIDS or paths to all the resources used
    /// in <see cref="EditorIcons"/>.
    /// </summary>
    internal class EditorIconsDatabase : ScriptableObject
    {
        private const string TriangleRightData = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABaklEQVQ4jdWRsWrCUBSGT87NrSGQ0WAgT9AH8AV8BXHqpJNDodClQ0GhSwuFLn2DQl/AxaXi0EWKUKhbcHBx65QrNAnnnnKjETW6t/90uNz/u+f/L/x/WUWCXq8HlmVBlmUghEDbtllrzcycnxv1+/1SYCwGc4mIwPf9MymlVkoxAAhEBAM5JXtLQhRKKWo0GvfVajUZj8ePs9nsW2sNlUpFMDMdY2w3YGZrEyHwff+m1Wp9drvdqzAM3TiOKU3TkvkwQp4XETPzWpZlYRAET51O56PZbF7UajWv5N4FrBlWMQghxA8RpVLK83q9/tJuty9L7t0O1inyskz7RESOlBJWq9X7ZDK5m06nbyX3ASDfgJkdUygifs3n84fBYPC6XC7ZcZySeQ/AzHrTQbRYLK5Ho9FzFEWpEAI8z0NtvuOI9gCu68JwOLxVSkGSJOC6LppzItJFPydL3LwOcRybLGhgxVanzH9AAPALQK6xKuQq1X8AAAAASUVORK5CYII=";

        [UsedImplicitly]
        [SerializeField, Multiline(6)] private string _description;

        private Texture2D _triangleRight;

        public Texture2D TriangleRight
        {
            get
            {
                if (_triangleRight == null)
                {
                    var pngBytes = Convert.FromBase64String(TriangleRightData);
                    _triangleRight = new Texture2D(2, 2);
                    _triangleRight.LoadImage(pngBytes);
                }

                return _triangleRight;
            }
        }

        [SerializeField] private Material _activeDarkSkin;
        [SerializeField] private Material _activeLightSkin;
        [SerializeField] private Material _highlightedDarkSkin;
        [SerializeField] private Material _highlightedLightSkin;

        public Material Active => EditorGUIUtility.isProSkin ? _activeDarkSkin : _activeLightSkin;

        public Material Highlighted => EditorGUIUtility.isProSkin ? _highlightedDarkSkin : _highlightedLightSkin;
    }
}