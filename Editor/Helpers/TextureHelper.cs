namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>Helps to create new textures.</summary>
    public static class TextureHelper
    {
        /// <summary>
        /// Temporarily sets <see cref="GL.sRGBWrite"/> to the passed value, then returns it back.
        /// </summary>
        [PublicAPI]
        public readonly struct SRGBWriteScope : IDisposable
        {
            private readonly bool _previousValue;

            /// <summary>Temporarily sets <see cref="GL.sRGBWrite"/> to <paramref name=""/>, then executes the action.</summary>
            /// <param name="enableWrite"> Temporary value of <see cref="GL.sRGBWrite"/>. </param>
            /// <example><code>
            /// using (new SRGBWriteScope(true))
            /// {
            ///     GL.Clear(false, true, new Color(1f, 1f, 1f, 0f));
            ///     Graphics.Blit(Default, temporary, material);
            /// });
            /// </code></example>
            public SRGBWriteScope(bool enableWrite)
            {
                _previousValue = GL.sRGBWrite;
                GL.sRGBWrite = enableWrite;
            }

            public void Dispose()
            {
                GL.sRGBWrite = _previousValue;
            }
        }
    }
}