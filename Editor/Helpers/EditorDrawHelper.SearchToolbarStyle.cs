namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using SolidUtilities;
    using UnityEditor;
    using UnityEngine;

    public static partial class EditorDrawHelper
    {
        /// <summary>Applies the search toolbar style to the stuff that will be drawn inside.</summary>
        /// <example><code>
        /// using (new EditorDrawHelper.SearchToolbarStyle(DropdownStyle.SearchToolbarHeight))
        /// {
        ///     DrawSearchToolbar();
        /// }
        /// </code></example>
        [PublicAPI]
        public readonly struct SearchToolbarStyle : IDisposable
        {
            private static readonly GUILayoutOption[] _options = { GUILayoutHelper.ExpandWidth(false), null };
            private static GUIStyle _style;

            private static GUIStyle Style =>
                _style ?? (_style = new GUIStyle(EditorStyles.toolbar)
                {
                    padding = new RectOffset(0, 0, 0, 0),
                    stretchHeight = true,
                    stretchWidth = true,
                    fixedHeight = 0f
                });

            /// <summary>Applies the search toolbar style to the stuff that will be drawn inside.</summary>
            /// <param name="toolbarHeight">Height of the toolbar.</param>
            /// <example><code>
            /// using (new EditorDrawHelper.SearchToolbarStyle(DropdownStyle.SearchToolbarHeight))
            /// {
            ///     DrawSearchToolbar();
            /// }
            /// </code></example>
            public SearchToolbarStyle(float toolbarHeight)
            {
                _options[1] = GUILayout.Height(toolbarHeight);

                EditorGUILayout.BeginHorizontal(
                    Style,
                    _options);
            }

            public void Dispose()
            {
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}