namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    public static class EditorGUIUtilityHelper
    {
        public static LabelWidth LabelWidthBlock(float width) => new LabelWidth(width);

        public readonly struct LabelWidth : IDisposable
        {
            private readonly float _oldWidth;

            public LabelWidth(float width)
            {
                _oldWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = width;
            }

            public void Dispose()
            {
                EditorGUIUtility.labelWidth = _oldWidth;
            }
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
    }
}