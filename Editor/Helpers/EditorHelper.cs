namespace SolidUtilities.Editor.Helpers
{
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

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
    }
}