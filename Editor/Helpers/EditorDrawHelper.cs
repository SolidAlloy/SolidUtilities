namespace SolidUtilities.Editor.Helpers
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEditorInternals;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>Different useful methods that simplify <see cref="EditorGUILayout"/> API.</summary>
    public static partial class EditorDrawHelper
    {
        /// <summary>
        /// Cache that creates <see cref="GUIContent"/> instances and keeps them, reducing the garbage
        /// collection overhead.
        /// </summary>
        public static readonly ContentCache ContentCache = new ContentCache();

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

        private static GUIStyle _placeholderStyle;

        private static GUIStyle PlaceholderStyle =>
            _placeholderStyle ?? (_placeholderStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleLeft,
                clipping = TextClipping.Clip,
                margin = new RectOffset(4, 4, 4, 4)
            });

        /// <summary>Draws content in an automatically laid out scroll view.</summary>
        /// <returns>The new thumb position.</returns>
        /// <example><code>
        /// _thumbPos = EditorDrawHelper.DrawInScrollView(_thumbPos, () =>
        /// {
        ///     float contentHeight = EditorDrawHelper.DrawVertically(_selectionTree.Draw, _preventExpandingHeight,
        ///         DropdownStyle.BackgroundColor);
        /// });
        /// </code></example>
        [PublicAPI]
        public readonly struct ScrollView : IDisposable
        {
            private static readonly GUILayoutOption[] _dontExpandHeight = { GUILayout.ExpandHeight(false) };

            /// <summary>Draws content in an automatically laid out scroll view.</summary>
            /// <param name="scrollPos">
            /// Position of the thumb that is changed to the new thumb position upon <see cref="ScrollView"/> initialization.
            /// </param>
            public ScrollView(ref Vector2 scrollPos)
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            }

            /// <summary>
            /// Draws content in an automatically laid out scroll view.
            /// </summary>
            /// <param name="scrollPos">
            /// Position of the thumb that is changed to the new thumb position upon <see cref="ScrollView"/> initialization.
            /// </param>
            /// <param name="visible">
            /// Whether scrollbar should be visible (it may not be visible if the list is short enough to fit the window.)
            /// </param>
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

        [PublicAPI]
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

        /// <summary>Draws content in the vertical direction.</summary>
        /// <example><code>
        /// using (new EditorDrawHelper.VerticalBlock(_preventExpandingHeight, DropdownStyle.BackgroundColor, out float contentHeight))
        /// {
        ///     _selectionTree.Draw();
        ///
        ///     if (Event.current.type == EventType.Repaint)
        ///         _contentHeight = contentHeight;
        /// }
        /// </code></example>
        [PublicAPI]
        public readonly struct VerticalBlock : IDisposable
        {
            private static readonly GUILayoutOption[] _options = new GUILayoutOption[1];

            /// <summary>Draws content in the vertical direction.</summary>
            /// <param name="option">Option to draw the vertical group with.</param>
            /// <param name="backgroundColor">Background of the vertical group rectangle.</param>
            /// <param name="height">Height of the vertical group rectangle.</param>
            /// <example><code>
            /// using (new EditorDrawHelper.VerticalBlock(_preventExpandingHeight, DropdownStyle.BackgroundColor, out float contentHeight))
            /// {
            ///     _selectionTree.Draw();
            ///
            ///     if (Event.current.type == EventType.Repaint)
            ///         _contentHeight = contentHeight;
            /// }
            /// </code></example>
            public VerticalBlock(GUILayoutOption option, Color backgroundColor, out float height)
            {
                _options[0] = option;
                Rect rect = EditorGUILayout.BeginVertical(_options);
                EditorGUI.DrawRect(rect, backgroundColor);
                height = rect.height;
            }

            /// <summary>Draws content in the vertical direction.</summary>
            /// <param name="rect">Rectangle of the vertical group.</param>
            /// <example><code>
            /// Rect newWholeListRect = EditorDrawHelper.DrawVertically(() =>
            /// {
            ///     for (int index = 0; index &lt; nodes.Count; ++index)
            ///         nodes[index].DrawSelfAndChildren(0, visibleRect);
            /// });
            /// </code></example>
            public VerticalBlock(out Rect rect)
            {
                rect = EditorGUILayout.BeginVertical((GUILayoutOption[]) null);
            }

            public void Dispose()
            {
                EditorGUILayout.EndVertical();
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

        /// <summary>Shows the info message.</summary>
        /// <param name="message">The message to output.</param>
        /// <example><code>EditorDrawHelper.DrawInfoMessage("No types to select.");</code></example>
        [PublicAPI] public static void DrawInfoMessage(string message)
        {
            var messageContent = new GUIContent(message, EditorIcons.Info);
            Rect labelPos = EditorGUI.IndentedRect(GUILayoutUtility.GetRect(messageContent, InfoMessageStyle));
            GUI.Label(labelPos, messageContent, InfoMessageStyle);
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
                GUI.Label(placeHolderArea, ContentCache.GetItem(placeholder), PlaceholderStyle);
            }

            return text;
        }

        /// <summary>Creates an editor of type <typeparamref name="T"/> for <paramref name="targetObject"/>.</summary>
        /// <param name="targetObject">Target object to create an editor for.</param>
        /// <typeparam name="T">Type of the editor to create.</typeparam>
        /// <returns>Editor of type <typeparamref name="T"/>.</returns>
        [PublicAPI, Pure]
        public static T CreateEditor<T>(Object targetObject)
            where T : Editor
        {
            return (T) Editor.CreateEditor(targetObject, typeof(T));
        }

        /// <summary>
        /// Returns the same value as <see cref="Screen.currentResolution.width"/> if one screen is used. Returns the
        /// sum of two screens' widths when two monitors are used and Unity is located on the second screen. It will
        /// only return the incorrect value when Unity is located on the second screen and is not fullscreen.
        /// </summary>
        /// <returns>
        /// Screen width if one monitor is used, or sum of screen widths if multiple monitors are used.
        /// </returns>
        /// <remarks>
        /// <see cref="Display.displays"/> always returns 1 in Editor, so there is no way to check the resolution of
        /// both monitors. This method uses a workaround but it does not work correctly when Unity is on the second
        /// screen and is not fullscreen. Any help to overcome this issue will be appreciated.
        /// </remarks>
        [PublicAPI, Pure]
        public static float GetScreenWidth()
        {
            return Mathf.Max(GetMainWindowPosition().xMax, Screen.currentResolution.width);
        }

        /// <summary>
        /// Returns the rectangle of the main Unity window.
        /// </summary>
        /// <returns>Rectangle of the main Unity window.</returns>
        /// <remarks>
        /// For Unity 2020.1 and above, this is just a wrapper of <see cref="EditorGUIUtility.GetMainWindowPosition"/>,
        /// but below 2020.1 this is a separate solution.
        /// </remarks>
        [PublicAPI, Pure]
        public static Rect GetMainWindowPosition()
        {
#if UNITY_2020_1_OR_NEWER
            return EditorGUIUtility.GetMainWindowPosition();
#else
            return EditorGUIUtilityProxy.GetMainWindowPosition();
#endif
        }

        /// <summary>
        /// Draws the property identically to <see cref="EditorGUILayout.PropertyField(SerializedProperty, GUIContent, GUILayoutOption[])"/>
        /// but makes fields delayed where possible.
        /// </summary>
        /// <param name="property">The SerializedProperty to make a field for.</param>
        /// <param name="label">Optional label to use. If not specified the label of the property itself is used. Use GUIContent.none to not display a label at all.</param>
        /// <param name="options">An optional list of layout options that specify extra layout properties. Any values passed in here will override settings defined by the style.&lt;br&gt;
        /// See Also: GUILayout.Width, GUILayout.Height, GUILayout.MinWidth, GUILayout.MaxWidth, GUILayout.MinHeight,
        /// GUILayout.MaxHeight, GUILayout.ExpandWidth, GUILayout.ExpandHeight.</param>
        /// <returns>True if the property has children and is expanded and includeChildren was set to false; otherwise false.</returns>
        public static bool DelayedPropertyField(SerializedProperty property, GUIContent label = null, params GUILayoutOption[] options)
        {
#if UNITY_2020_1_OR_NEWER
            return EditorGUILayoutHelper.DelayedPropertyField(property, label, options);
#else
            throw new NotImplementedException("The method is implemented only for Unity 2020 and higher");
#endif
        }
    }
}