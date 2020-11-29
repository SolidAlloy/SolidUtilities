namespace SolidUtilities.Editor.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

#if UNITY_2020_1_OR_NEWER
    using System.Reflection;
    using Object = UnityEngine.Object;
#endif

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
                float screenWidth = EditorDrawHelper.GetScreenWidth();
                if (positionToAdjust.xMax >= screenWidth)
                {
                    positionToAdjust.x -= positionToAdjust.xMax - screenWidth;
                    Debug.Log($"previous pos.x: {window.position.x}, new pos.x: {positionToAdjust.x}");
                }
            }

            if (changeHeight)
            {
                Debug.Log($"previous height: {window.position.height}, new height: {height}");

                // MainWindow is more reliable than Screen.currentResolution.height, especially for the multi-display setup.
                float mainWinYMax = GetMainWindowPosition().yMax;

                if (positionToAdjust.yMax >= mainWinYMax)
                {
                    positionToAdjust.y -= positionToAdjust.yMax - mainWinYMax;
                    Debug.Log($"previous pos.y: {window.position.y}, new pos.y: {positionToAdjust.y}");
                }

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
            Rect main = GetMainWindowPosition();

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

        [UsedImplicitly]
        private static IEnumerable<Type> GetAllDerivedTypes(this AppDomain appDomain, Type parentType)
        {
            return from assembly in appDomain.GetAssemblies()
                from assemblyType in assembly.GetTypes()
                where assemblyType.IsSubclassOf(parentType)
                select assemblyType;
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
        private static Rect GetMainWindowPosition()
        {
#if UNITY_2020_1_OR_NEWER
            return EditorGUIUtility.GetMainWindowPosition();
#else
            const int mainWindowIndex = 4;
            const string showModeName = "m_ShowMode";
            const string positionName = "position";
            const string containerWindowName = "ContainerWindow";

            Type containerWinType = AppDomain.CurrentDomain
                .GetAllDerivedTypes(typeof(ScriptableObject))
                .FirstOrDefault(type => type.Name == containerWindowName);

            if (containerWinType == null)
                throw new MissingMemberException($"Can't find internal type {containerWindowName}. Maybe something has changed inside Unity.");

            FieldInfo showModeField = containerWinType.GetField(showModeName, BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo positionProperty = containerWinType.GetProperty(positionName, BindingFlags.Public | BindingFlags.Instance);

            if (showModeField == null || positionProperty == null)
                throw new MissingFieldException($"Can't find internal fields '{showModeName}' or '{positionName}'. Maybe something has changed inside Unity.");

            var windows = Resources.FindObjectsOfTypeAll(containerWinType);

            foreach (Object win in windows)
            {
                if ((int) showModeField.GetValue(win) != mainWindowIndex)
                    continue;

                return (Rect)positionProperty.GetValue(win, null);
            }

            throw new NotSupportedException("Can't find internal main window. Maybe something has changed inside Unity");
#endif
        }
    }
}