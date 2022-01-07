namespace SolidUtilities
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    public static class GUILayoutHelper
    {
        private static readonly GUILayoutOption _expandWidthTrue = GUILayout.ExpandWidth(true);
        private static readonly GUILayoutOption _expandWidthFalse = GUILayout.ExpandWidth(true);

        /// <summary>
        /// Draws content in the horizontal direction.
        /// </summary>
        /// <param name="style">The style to draw with.</param>
        public static Horizontal HorizontalBlock(GUIStyle style = null) => new Horizontal(style);

        public readonly struct Horizontal : IDisposable
        {
            public Horizontal([CanBeNull] GUIStyle style)
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

        /// <summary>
        /// Draws content in the vertical direction.
        /// </summary>
        /// <param name="style">The style to draw with.</param>
        public static Vertical VerticalBlock(GUIStyle style = null) => new Vertical(style);

        [PublicAPI]
        public readonly struct Vertical : IDisposable
        {
            public Vertical([CanBeNull] GUIStyle style)
            {
                if (style == null)
                {
                    GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
                }
                else
                {
                    GUILayout.BeginVertical(style, null);
                }
            }

            public void Dispose() => GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws content in GUILayout area.
        /// </summary>
        /// <param name="screenRect">Screen rect to draw the content in.</param>
        public static Area AreaBlock(Rect screenRect) => new Area(screenRect);

        public readonly struct Area : IDisposable
        {
            public Area(Rect screenRect)
            {
                GUILayout.BeginArea(screenRect);
            }

            public void Dispose() => GUILayout.EndArea();
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