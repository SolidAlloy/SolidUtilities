namespace SolidUtilities.Editor
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>Creates a temporary texture that can be used and then removed automatically.</summary>
    /// <seealso cref="TemporaryActiveTexture"/>
    /// <example><code>
    /// using (var temporaryTexture = new TemporaryRenderTexture(icon.width, icon.height, 0))
    /// {
    ///     Graphics.Blit(icon, temporaryTexture, material);
    /// });
    /// </code></example>
    [PublicAPI]
    public class TemporaryRenderTexture : IDisposable
    {
        private readonly RenderTexture _value;

        /// <summary>Creates a temporary texture that can be used and then removed automatically.</summary>
        /// <param name="width">Width of the temporary texture in pixels.</param>
        /// <param name="height">Height of the temporary texture in pixels.</param>
        /// <param name="depthBuffer">Depth buffer of the temporary texture.</param>
        /// <seealso cref="TemporaryActiveTexture"/>
        /// <example><code>
        /// using (var temporaryTexture = new TemporaryRenderTexture(icon.width, icon.height, 0))
        /// {
        ///     Graphics.Blit(icon, temporaryTexture, material);
        /// });
        /// </code></example>
        public TemporaryRenderTexture(int width, int height, int depthBuffer)
        {
            _value = RenderTexture.GetTemporary(width, height, depthBuffer);
        }

        public static implicit operator RenderTexture(TemporaryRenderTexture temporaryRenderTexture) => temporaryRenderTexture._value;

        public void Dispose()
        {
            RenderTexture.ReleaseTemporary(_value);
        }
    }
}