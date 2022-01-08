namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEditor;

    public static partial class EditorDrawHelper
    {
        /// <summary> Sets <see cref="EditorGUI.showMixedValue"/> to the needed value temporarily. </summary>
        /// <example><code>
        /// using (new EditorDrawHelper.MixedValue(true))
        /// {
        ///     DrawTypeSelectionControl();
        /// }
        /// </code></example>
        [PublicAPI]
        public readonly struct MixedValue : IDisposable
        {
            private readonly bool _previousValue;

            /// <summary> Sets <see cref="EditorGUI.showMixedValue"/> to the needed value temporarily. </summary>
            /// <param name="showMixedValue">Whether to show mixed value.</param>
            /// <example><code>
            /// using (new EditorDrawHelper.MixedValue(true))
            /// {
            ///     DrawTypeSelectionControl();
            /// }
            /// </code></example>
            public MixedValue(bool showMixedValue)
            {
                _previousValue = EditorGUI.showMixedValue;
                EditorGUI.showMixedValue = showMixedValue;
            }

            public void Dispose()
            {
                EditorGUI.showMixedValue = _previousValue;
            }
        }
    }
}