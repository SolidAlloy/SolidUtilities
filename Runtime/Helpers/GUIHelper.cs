namespace SolidUtilities
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    public static class GUIHelper
    {
        private static readonly GUIStyle _closeButtonStyle = GUI.skin.FindStyle("ToolbarSearchCancelButton");

        /// <summary>Draws the close button.</summary>
        /// <param name="buttonRect">Rect the button should be located in.</param>
        /// <returns>Whether the button was pressed.</returns>
        /// <example><code>
        /// if (DrawHelper.CloseButton(buttonRect))
        /// {
        ///     searchText = string.Empty;
        ///     GUI.FocusControl(null);
        /// }
        /// </code></example>
        [PublicAPI] public static bool CloseButton(Rect buttonRect)
        {
            // This is a known problem that the button does not align to center horizontally for some reason.
            // I tried alignment = TextAnchor.MiddleCenter, setting padding and margin to different values,
            // but to no avail. Any help with this is appreciated.
            return GUI.Button(buttonRect, GUIContent.none, _closeButtonStyle);
        }

        /// <summary>
        /// Draws content in scroll view.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the ScrollView.</param>
        /// <param name="scrollPosition">The pixel distance that the view is scrolled in the X and Y directions.</param>
        /// <param name="viewRect">The rectangle used inside the scrollView.</param>
        public static ScrollView ScrollViewBlock(Rect position, ref Vector2 scrollPosition, Rect viewRect) => new ScrollView(position, ref scrollPosition, viewRect);

        public readonly struct ScrollView : IDisposable
        {
            public ScrollView(Rect position, ref Vector2 scrollPosition, Rect viewRect)
            {
                scrollPosition = GUI.BeginScrollView(position, scrollPosition, viewRect);
            }

            public void Dispose() => GUI.EndScrollView();
        }
    }
}
