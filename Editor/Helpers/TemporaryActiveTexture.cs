namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>
    /// Creates a temporary texture, sets it as active in <see cref="RenderTexture.active"/>, then removes the changes
    /// and sets the previous active texture back automatically.
    /// </summary>
    /// <seealso cref="TemporaryRenderTexture"/>
    /// <example><code>
    /// using (var temporaryActiveTexture = new TemporaryActiveTexture(icon.width, icon.height, 0))
    /// {
    ///     Graphics.Blit(icon, temporary, material);
    /// });
    /// </code></example>
    [PublicAPI]
    public class TemporaryActiveTexture : IDisposable
    {
        private readonly RenderTexture _previousActiveTexture;
        private readonly TemporaryRenderTexture _value;

        /// <summary>
        /// Creates a temporary texture, sets it as active in <see cref="RenderTexture.active"/>, then removes it
        /// and sets the previous active texture back automatically.
        /// </summary>
        /// <param name="width">Width of the temporary texture in pixels.</param>
        /// <param name="height">Height of the temporary texture in pixels.</param>
        /// <param name="depthBuffer">Depth buffer of the temporary texture.</param>
        /// <seealso cref="TemporaryRenderTexture"/>
        /// <example><code>
        /// using (var temporaryActiveTexture = new TemporaryActiveTexture(icon.width, icon.height, 0))
        /// {
        ///     Graphics.Blit(icon, temporary, material);
        /// });
        /// </code></example>
        public TemporaryActiveTexture(int width, int height, int depthBuffer)
        {
            _previousActiveTexture = RenderTexture.active;
            _value = new TemporaryRenderTexture(width, height, depthBuffer);
            RenderTexture.active = _value;
        }

        public static implicit operator RenderTexture(TemporaryActiveTexture temporaryTexture) => temporaryTexture._value;

        public void Dispose()
        {
            _value.Dispose();
            RenderTexture.active = _previousActiveTexture;
        }
    }
}