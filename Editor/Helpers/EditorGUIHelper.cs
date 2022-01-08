namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    public static class EditorGUIHelper
    {
        private static GUIStyle _placeholderStyle;

        private static GUIStyle PlaceholderStyle =>
            _placeholderStyle ?? (_placeholderStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleLeft,
                clipping = TextClipping.Clip,
                margin = new RectOffset(4, 4, 4, 4)
            });

        [PublicAPI]
        public static bool HasKeyboardFocus(int controlID) => UnityEditorInternals.EditorGUIProxy.HasKeyboardFocus(controlID);

        public static IndentLevel IndentLevelBlock(int indentLevel) => new IndentLevel(indentLevel);

        public readonly struct IndentLevel : IDisposable
        {
            private readonly int _previousIndentLevel;

            public IndentLevel(int indentLevel)
            {
                _previousIndentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = indentLevel;
            }

            public void Dispose()
            {
                EditorGUI.indentLevel = _previousIndentLevel;
            }
        }

        /// <summary>Draws borders with a given color and width around a rectangle.</summary>
        /// <param name="rectWidth">Width of the rectangle.</param>
        /// <param name="rectHeight">Height of the rectangle.</param>
        /// <param name="color">Color of the borders.</param>
        /// <param name="borderWidth">Width of the borders.</param>
        /// <example><code>
        /// EditorDrawHelper.DrawBorders(position.width, position.height, DropdownStyle.BorderColor);
        /// </code></example>
        [PublicAPI] public static void DrawBorders(float rectWidth, float rectHeight, Color color, float borderWidth = 1f)
        {
            if (Event.current.type != EventType.Repaint)
                return;

            var leftBorder = new Rect(0f, 0f, borderWidth, rectHeight);
            var topBorder = new Rect(0f, 0f, rectWidth, borderWidth);
            var rightBorder = new Rect(0f, 0f, rectWidth, borderWidth);
            var bottomBorder = new Rect(0f, rectHeight - borderWidth, rectWidth, borderWidth);

            EditorGUI.DrawRect(leftBorder, color);
            EditorGUI.DrawRect(topBorder, color);
            EditorGUI.DrawRect(rightBorder, color);
            EditorGUI.DrawRect(bottomBorder, color);
        }

        /// <summary>Draws content and checks if it was changed.</summary>
        /// <param name="drawContent">Action that draws the content.</param>
        /// <returns>Whether the content was changed.</returns>
        /// <example><code>
        /// bool changed = EditorDrawHelper.CheckIfChanged(() =>
        /// {
        ///     _searchString = DrawSearchField(innerToolbarArea, _searchString);
        /// });
        /// </code></example>
        [Pure, PublicAPI] public static bool CheckIfChanged(Action drawContent)
        {
            EditorGUI.BeginChangeCheck();
            drawContent();
            return EditorGUI.EndChangeCheck();
        }

        /// <summary>Draws a text field that is always focused.</summary>
        /// <param name="rect">Rectangle to draw the field in.</param>
        /// <param name="text">The text to show in the field.</param>
        /// <param name="placeholder">Placeholder to show if the field is empty.</param>
        /// <param name="style">Style to draw the field with.</param>
        /// <param name="controlName">Unique control name of the field.</param>
        /// <returns>The text that was written to the field.</returns>
        /// <example><code>
        /// searchText = EditorDrawHelper.FocusedTextField(searchFieldArea, searchText, "Search",
        ///     DropdownStyle.SearchToolbarStyle, _searchFieldControlName);
        /// </code></example>
        [PublicAPI] public static string FocusedTextField(Rect rect, string text, string placeholder, GUIStyle style, string controlName)
        {
            const float placeholderIndent = 14f;

            GUI.SetNextControlName(controlName);
            text = EditorGUI.TextField(rect, text, style);
            EditorGUI.FocusTextInControl(controlName);

            if (Event.current.type == EventType.Repaint && string.IsNullOrEmpty(text))
            {
                var placeHolderArea = new Rect(rect.x + placeholderIndent, rect.y, rect.width - placeholderIndent, rect.height);
                GUI.Label(placeHolderArea, GUIContentHelper.Temp(placeholder), PlaceholderStyle);
            }

            return text;
        }
    }
}