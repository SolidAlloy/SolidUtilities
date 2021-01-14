namespace SolidUtilities.Editor.Helpers
{
    using System;
    using JetBrains.Annotations;
    using SolidUtilities.Helpers;
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
                EditorGUILayout.BeginHorizontal(
                    Style,
                    GUILayout.Height(toolbarHeight),
                    DrawHelper.ExpandWidth(false));
            }

            public void Dispose()
            {
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}