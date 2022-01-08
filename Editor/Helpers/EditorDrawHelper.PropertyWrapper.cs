namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    public static partial class EditorDrawHelper
    {
        /// <summary>
        /// Draws content in a property wrapper, useful for making regular GUI controls work with SerializedProperty.
        /// </summary>
        [PublicAPI]
        public readonly struct PropertyWrapper : IDisposable
        {
            private readonly GUIContent _label;

            /// <summary>
            /// Draws content in a property wrapper, useful for making regular GUI controls work with SerializedProperty.
            /// </summary>
            /// <param name="position">Rectangle on the screen to use for the control, including label if applicable.</param>
            /// <param name="label">Optional label in front of the slider. Use null to use the name from the
            /// SerializedProperty. Use GUIContent.none to not display a label.</param>
            /// <param name="property">The SerializedProperty to use for the control.</param>
            public PropertyWrapper(Rect position, GUIContent label, SerializedProperty property)
            {
                _label = EditorGUI.BeginProperty(position, label, property);
            }

            public static implicit operator GUIContent(PropertyWrapper wrapper) => wrapper._label;

            public void Dispose()
            {
                EditorGUI.EndProperty();
            }
        }
    }
}