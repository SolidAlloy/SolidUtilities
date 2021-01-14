namespace SolidUtilities.Helpers
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>Different useful methods that simplify <see cref="GUILayout"/> API.</summary>
    public static class DrawHelper
    {
        private static readonly GUIStyle _closeButtonStyle = GUI.skin.FindStyle("ToolbarSeachCancelButton");
        private static readonly GUILayoutOption _expandWidthTrue = GUILayout.ExpandWidth(true);
        private static readonly GUILayoutOption _expandWidthFalse = GUILayout.ExpandWidth(true);

        /// <summary>Draws content in the horizontal direction.</summary>
        /// <param name="drawContent">Action that draws the content.</param>
        /// <seealso cref="HorizontalBlock"/>
        /// <example><code>
        /// DrawHelper.DrawHorizontally(() =>
        /// {
        ///     selectedValue = DrawSelectorDropdownAndGetSelectedValue();
        ///
        ///     if (Event.current.type == EventType.Repaint)
        ///         DrawHamburgerMenuButton();
        /// });
        /// </code></example>
        [PublicAPI] public static void DrawHorizontally(Action drawContent)
        {
            GUILayout.BeginHorizontal();
            drawContent();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws content in the horizontal direction.
        /// </summary>
        /// <seealso cref="DrawHelper.DrawHorizontally"/>
        [PublicAPI]
        public readonly struct HorizontalBlock : IDisposable
        {
            /// <summary>
            /// Starts drawing content in the horizontal direction. Pass null if you don't want any GC allocation.
            /// </summary>
            /// <param name="style">The style to draw with.</param>
            public HorizontalBlock([CanBeNull] GUIStyle style)
            {
                if (style == null)
                {
                    GUILayout.BeginHorizontal((GUILayoutOption[]) null);
                }
                else
                {
                    GUILayout.BeginHorizontal(style, null);
                }
            }

            public void Dispose() => GUILayout.EndHorizontal();
        }

        /// <summary>Draws content in the vertical direction.</summary>
        /// <param name="drawContent">Action that draws the content.</param>
        /// <seealso cref="VerticalBlock"/>
        /// <example><code>
        /// DrawHelper.DrawVertically(() =>
        /// {
        ///     EditorDrawHelper.DrawInfoMessage("No types to select.");
        /// });
        /// </code></example>
        [PublicAPI] public static void DrawVertically(Action drawContent)
        {
            GUILayout.BeginVertical((GUILayoutOption[]) null);
            drawContent();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws content in the vertical direction.
        /// </summary>
        /// <seealso cref="DrawHelper.DrawVertically"/>
        [PublicAPI]
        public readonly struct VerticalBlock : IDisposable
        {
            /// <summary>
            /// Starts drawing content in the vertical direction.
            /// </summary>
            /// <param name="style">The style to draw with. Pass null to draw with default style.</param>
            public VerticalBlock([CanBeNull] GUIStyle style)
            {
                if (style == null)
                {
                    GUILayout.BeginVertical((GUILayoutOption[]) null);
                }
                else
                {
                    GUILayout.BeginVertical(style, null);
                }
            }

            public void Dispose() => GUILayout.EndHorizontal();
        }

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
        /// <see cref="GUILayout.ExpandWidth"/> that is instantiated only once, reducing the garbage collection overhead.
        /// </summary>
        /// <param name="expand">Whether to expand width.</param>
        /// <returns><see cref="GUILayout.ExpandWidth"/> with the given expand bool.</returns>
        /// <example><code>
        /// EditorGUILayout.BeginHorizontal(
        ///     SearchToolbarStyle,
        ///     GUILayout.Height(toolbarHeight),
        ///     DrawHelper.ExpandWidth(false));
        /// </code></example>
        [PublicAPI] public static GUILayoutOption ExpandWidth(bool expand) => expand ? _expandWidthTrue : _expandWidthFalse;
    }
}