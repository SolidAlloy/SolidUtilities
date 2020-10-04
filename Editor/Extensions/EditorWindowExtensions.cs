namespace SolidUtilities.Editor.Extensions
{
    using System;
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
        public static void Resize(this EditorWindow window, float width = -1f, float height = -1f)
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
                float screenWidth = Screen.currentResolution.width;
                if (positionToAdjust.xMax >= screenWidth)
                    positionToAdjust.x -= positionToAdjust.xMax - screenWidth;
            }

            if (changeHeight)
            {
                const float windowTitleBarHeight = 40f;
                float screenHeight = Screen.currentResolution.height - windowTitleBarHeight;
                if (positionToAdjust.yMax >= screenHeight)
                    positionToAdjust.y -= positionToAdjust.yMax - screenHeight;
            }

            window.position = positionToAdjust;
        }

        private static void EnsureTheValueIsNotNegative(string valueName, float value)
        {
            if (value < 0f && value != -1f)
                throw new ArgumentOutOfRangeException(valueName, value, "The value can only be positive or -1f.");
        }
    }
}