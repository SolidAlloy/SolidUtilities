namespace SolidUtilities.Editor.Helpers
{
    using System;
    using UnityEngine;

    /// <summary>Different useful methods that simplify <see cref="GUILayout"/> API.</summary>
    public static class DrawHelper
    {
        private static readonly GUIStyle CloseButtonStyle = GUI.skin.FindStyle("ToolbarSeachCancelButton");
        private static readonly GUILayoutOption ExpandWidthTrue = GUILayout.ExpandWidth(true);
        private static readonly GUILayoutOption ExpandWidthFalse = GUILayout.ExpandWidth(true);

        /// <summary>Draws content in the horizontal direction.</summary>
        /// <param name="drawContent">Action that draws the content.</param>
        public static void DrawHorizontally(Action drawContent)
        {
            GUILayout.BeginHorizontal();
            drawContent();
            GUILayout.EndHorizontal();
        }

        /// <summary>Draws content in the vertical direction.</summary>
        /// <param name="drawContent">Action that draws the content.</param>
        public static void DrawVertically(Action drawContent)
        {
            GUILayout.BeginVertical();
            drawContent();
            GUILayout.EndVertical();
        }

        /// <summary>Draws content in the vertical direction.</summary>
        /// <param name="style">Style to draw the content with.</param>
        /// <param name="drawContent">Action that draws the content.</param>
        public static void DrawVertically(GUIStyle style, Action drawContent)
        {
            GUILayout.BeginVertical(style);
            drawContent();
            GUILayout.EndVertical();
        }

        /// <summary>Draws the close button.</summary>
        /// <param name="buttonRect">Rect the button should be located in.</param>
        /// <returns>Whether the button was pressed.</returns>
        public static bool CloseButton(Rect buttonRect)
        {
            // This is a known problem that the button does not align to center horizontally for some reason.
            // I tried alignment = TextAnchor.MiddleCenter, setting padding and margin to different values,
            // but to no avail. Any help with this is appreciated.
            return GUI.Button(buttonRect, GUIContent.none, CloseButtonStyle);
        }

        /// <summary>
        /// <see cref="GUILayout.ExpandWidth"/> that is instantiated only once, reducing the garbage collection overhead.
        /// </summary>
        /// <param name="expand">Whether to expand width.</param>
        /// <returns><see cref="GUILayout.ExpandWidth"/> with the given expand bool.</returns>
        public static GUILayoutOption ExpandWidth(bool expand) => expand ? ExpandWidthTrue : ExpandWidthFalse;
    }
}