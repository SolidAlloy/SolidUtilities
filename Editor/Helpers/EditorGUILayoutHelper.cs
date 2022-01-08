namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    public static class EditorGUILayoutHelper
    {
        // EditorStyles built-in styles may not be initialized yet when EditorDrawHelper static constructor is called,
        // and it will cause NullReferenceException, so GUIStyles are initialized inside properties instead.
        private static GUIStyle _infoMessageStyle;

        private static GUIStyle InfoMessageStyle =>
            _infoMessageStyle ?? (_infoMessageStyle = new GUIStyle("HelpBox")
            {
                margin = new RectOffset(4, 4, 2, 2),
                fontSize = 10,
                richText = true
            });

        /// <summary>Draws content in an automatically laid out scroll view.</summary>
        /// <returns>Position of the thumb that is changed to the new thumb position upon <see cref="ScrollView"/> initialization.</returns>
        public static ScrollView ScrollViewBlock(ref Vector2 scrollPos) => new ScrollView(ref scrollPos);

        /// <summary>
        /// Draws content in an automatically laid out scroll view.
        /// </summary>
        /// <param name="scrollPos">
        /// Position of the thumb that is changed to the new thumb position upon <see cref="ScrollView"/> initialization.
        /// </param>
        /// <param name="visible">
        /// Whether scrollbar should be visible (it may not be visible if the list is short enough to fit the window.)
        /// </param>
        [PublicAPI]
        public static ScrollView ScrollViewBlock(ref Vector2 scrollPos, bool visible) => new ScrollView(ref scrollPos, visible);

        public readonly struct ScrollView : IDisposable
        {
            private static readonly GUILayoutOption[] _dontExpandHeight = { GUILayout.ExpandHeight(false) };

            public ScrollView(ref Vector2 scrollPos)
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            }

            public ScrollView(ref Vector2 scrollPos, bool visible)
            {
                scrollPos = visible
                    ? EditorGUILayout.BeginScrollView(scrollPos, _dontExpandHeight)
                    : EditorGUILayout.BeginScrollView(scrollPos, GUIStyle.none, GUIStyle.none, _dontExpandHeight);
            }

            public void Dispose()
            {
                EditorGUILayout.EndScrollView();
            }
        }

        /// <summary>Draws content in the vertical direction.</summary>
        /// <param name="option">Option to draw the vertical group with.</param>
        /// <param name="backgroundColor">Background of the vertical group rectangle.</param>
        /// <param name="height">Height of the vertical group rectangle.</param>
        public static Vertical VerticalBlock(GUILayoutOption option, Color backgroundColor, out float height) =>
            new Vertical(option, backgroundColor, out height);

        /// <summary>Draws content in the vertical direction.</summary>
        /// <param name="rect">Rectangle of the vertical group.</param>
        public static Vertical VerticalBlock(out Rect rect) => new Vertical(out rect);

        [PublicAPI]
        public readonly struct Vertical : IDisposable
        {
            private static readonly GUILayoutOption[] _options = new GUILayoutOption[1];

            public Vertical(GUILayoutOption option, Color backgroundColor, out float height)
            {
                _options[0] = option;
                Rect rect = EditorGUILayout.BeginVertical(_options);
                EditorGUI.DrawRect(rect, backgroundColor);
                height = rect.height;
            }

            public Vertical(out Rect rect)
            {
                rect = EditorGUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
            }

            public void Dispose()
            {
                EditorGUILayout.EndVertical();
            }
        }

        /// <summary>Shows the info message.</summary>
        /// <param name="message">The message to output.</param>
        /// <example><code>EditorDrawHelper.DrawInfoMessage("No types to select.");</code></example>
        [PublicAPI] public static void DrawInfoMessage(string message) => DrawMessage(message, EditorIcons.Info);

        /// <summary>Shows the info message.</summary>
        /// <param name="message">The message to output.</param>
        /// <example><code>EditorDrawHelper.DrawInfoMessage("No types to select.");</code></example>
        [PublicAPI] public static void DrawErrorMessage(string message) => DrawMessage(message, EditorIcons.Error);

        private static void DrawMessage(string message, Texture2D icon)
        {
            var messageContent = new GUIContent(message, icon);
            Rect labelPos = EditorGUI.IndentedRect(GUILayoutUtility.GetRect(messageContent, InfoMessageStyle));
            GUI.Label(labelPos, messageContent, InfoMessageStyle);
        }
    }
}