namespace SolidUtilities.Editor
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Object = UnityEngine.Object;

    public static class EditorHelper
    {
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

        private static Func<Vector2> _getCurrentMousePosition;

        /// <summary>
        /// Returns the current mouse position in screen coordinates. Unlike Event.current.mousePosition, it is window-agnostic and always returns the correct screen coordinates.
        /// </summary>
        /// <returns>The current mouse position in screen coordinates.</returns>
        public static Vector2 GetCurrentMousePosition()
        {
            if (_getCurrentMousePosition == null)
            {
                var currentMousePositionMethod = typeof(Editor).GetMethod("GetCurrentMousePosition", BindingFlags.NonPublic | BindingFlags.Static);
                Assert.IsNotNull(currentMousePositionMethod);
                _getCurrentMousePosition = (Func<Vector2>) Delegate.CreateDelegate(typeof(Func<Vector2>), currentMousePositionMethod);
            }

            return _getCurrentMousePosition();
        }

        private static Action _forceRebuildInspectors;
        public static void ForceRebuildInspectors()
        {
            if (_forceRebuildInspectors == null)
            {
                var rebuildMethod = typeof(EditorUtility).GetMethod("ForceRebuildInspectors",
                    BindingFlags.NonPublic | BindingFlags.Static);

                _forceRebuildInspectors = (Action) Delegate.CreateDelegate(typeof(Action), rebuildMethod);
            }

            _forceRebuildInspectors();
        }
    }
}