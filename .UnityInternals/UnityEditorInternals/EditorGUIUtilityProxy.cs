namespace SolidUtilities.UnityEditorInternals
{
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class EditorGUIUtilityProxy
    {
        private static FieldInfo _showModeField;

        private static FieldInfo ShowModeField
        {
            get
            {
                if (_showModeField == null)
                {
                    _showModeField = typeof(ContainerWindow).GetField(
                        "m_ShowMode",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                }

                return _showModeField;
            }
        }

        [PublicAPI]
        public static void SetIconForObject(Object obj, Texture2D icon)
        {
            EditorGUIUtility.SetIconForObject(obj, icon);
        }

        /// <summary>
        /// Returns position of Unity Editor's main window. In Unity 2020.1 or newer,
        /// use <see cref="EditorGUIUtility.GetMainWindowPosition"/>.
        /// </summary>
        /// <returns>Position of Unity Editor's main window.</returns>
        [PublicAPI]
        public static Rect GetMainWindowPosition()
        {
            const int mainWindowIndex = 4;

            var windows = Resources.FindObjectsOfTypeAll<ContainerWindow>();

            foreach (ContainerWindow win in windows)
            {
                if ((int) ShowModeField.GetValue(win) != mainWindowIndex || win.m_DontSaveToLayout)
                    continue;

                return win.position;
            }

            return new Rect(0.0f, 0.0f, 1000f, 600f);
        }
    }
}