namespace SolidUtilities.Editor
{
    using System;
    using Editor;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    /// <summary>Different useful extensions for <see cref="EditorWindow"/>.</summary>
    public static class EditorWindowExtensions
    {
        /// <summary>Resizes the window to the needed size.</summary>
        /// <param name="window">The window to change the size of.</param>
        /// <param name="width">The width to set. If the value is -1f, the width will not be changed.</param>
        /// <param name="height">The height to set. If the value is -1f, the height will not be changed.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if width or height are negative numbers and not -1f.
        /// </exception>
        /// <example><code>
        /// public class DummyWindow : EditorWindow
        /// {
        ///     private void OnCreate(Rect buttonRect)
        ///     {
        ///         var windowSize = new Vector2(100f, 100f);
        ///         ShowAsDropDown(buttonRect, windowSize);
        ///     }
        ///
        ///     private void OnGUI()
        ///     {
        ///         float optimalWidth = CalculateOptimalWidth();
        ///         float optimalHeight = Math.Min(_contentHeight, DropdownStyle.MaxWindowHeight);
        ///         this.Resize(optimalWidth, optimalHeight);
        ///     }
        /// }
        /// </code></example>
        [PublicAPI] public static void Resize(this EditorWindow window, float width = -1f, float height = -1f)
        {
            EnsureTheValueIsNotNegative(nameof(width), width);
            EnsureTheValueIsNotNegative(nameof(height), height);

            bool changeWidth = width != -1f;
            bool changeHeight = height != -1f;

            Rect positionToAdjust = window.position;

            if (changeWidth)
                positionToAdjust.width = width;

            if (changeHeight)
                positionToAdjust.height = height;

            window.minSize = new Vector2(changeWidth ? width : window.minSize.x, changeHeight ? height : window.minSize.y);
            window.maxSize = new Vector2(changeWidth ? width : window.maxSize.x, changeHeight ? height : window.maxSize.y);

            if (changeWidth)
            {
                float screenWidth = EditorGUIUtilityHelper.GetScreenWidth();
                if (positionToAdjust.xMax >= screenWidth)
                    positionToAdjust.x -= positionToAdjust.xMax - screenWidth;
            }

            if (changeHeight)
            {
                // MainWindow is more reliable than Screen.currentResolution.height, especially for the multi-display setup.
                float mainWinYMax = EditorGUIUtilityHelper.GetMainWindowPosition().yMax;

                if (positionToAdjust.yMax >= mainWinYMax)
                    positionToAdjust.y -= positionToAdjust.yMax - mainWinYMax;
            }

            window.position = positionToAdjust;
        }

        /// <summary>Moves the window out of screen to hide but not close it.</summary>
        /// <param name="window">The window to hide.</param>
        [PublicAPI] public static void MoveOutOfScreen(this EditorWindow window)
        {
            window.position = new Rect(
                Screen.currentResolution.width + 10f,
                Screen.currentResolution.height + 10f,
                0f, 0f);
        }

        /// <summary>
        /// Centers the window in the main Unity window. This is not the same as centering a window on screen,
        /// because the Unity window may not be maximized.
        /// </summary>
        /// <param name="window">The window to center.</param>
        [PublicAPI] public static void CenterOnMainWin(this EditorWindow window)
        {
            Rect main = EditorGUIUtilityHelper.GetMainWindowPosition();

            Rect pos = window.position;
            float centerWidth = (main.width - pos.width) * 0.5f;
            float centerHeight = (main.height - pos.height) * 0.5f;
            pos.x = main.x + centerWidth;
            pos.y = main.y + centerHeight;
            window.position = pos;
        }

        private static void EnsureTheValueIsNotNegative(string valueName, float value)
        {
            if (value < 0f && value != -1f)
                throw new ArgumentOutOfRangeException(valueName, value, "The value can only be positive or -1f.");
        }
    }
}