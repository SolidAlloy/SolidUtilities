namespace SolidUtilities.Editor.Helpers
{
    using System;
    using UnityEngine;

    public static class DrawHelper
    {
        private static readonly GUIStyle CloseButtonStyle = GUI.skin.FindStyle("ToolbarSeachCancelButton");
        private static readonly GUILayoutOption ExpandWidthTrue = GUILayout.ExpandWidth(true);
        private static readonly GUILayoutOption ExpandWidthFalse = GUILayout.ExpandWidth(true);

        public static void DrawHorizontally(Action drawContent)
        {
            GUILayout.BeginHorizontal();
            drawContent();
            GUILayout.EndHorizontal();
        }

        public static void DrawVertically(Action drawContent)
        {
            GUILayout.BeginVertical();
            drawContent();
            GUILayout.EndVertical();
        }

        public static void DrawVertically(GUIStyle style, Action drawContent)
        {
            GUILayout.BeginVertical(style);
            drawContent();
            GUILayout.EndVertical();
        }

        public static bool CloseButton(Rect buttonArea)
        {
            // This is a known problem that the button does not align to center horizontally for some reason.
            // I tried alignment = TextAnchor.MiddleCenter, setting padding and margin to different values,
            // but to no avail.
            return GUI.Button(buttonArea, GUIContent.none, CloseButtonStyle);
        }

        public static GUILayoutOption ExpandWidth(bool expand) => expand ? ExpandWidthTrue : ExpandWidthFalse;
    }
}