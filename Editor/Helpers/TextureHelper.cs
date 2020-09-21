namespace SolidUtilities.Editor.Helpers
{
    using System;
    using UnityEngine;

    public static class TextureHelper
    {
        public static void WithSRGBWrite(Action createTexture)
        {
            bool previousValue = GL.sRGBWrite;
            GL.sRGBWrite = true;
            createTexture();
            GL.sRGBWrite = previousValue;
        }

        public static void WithTemporaryActiveTexture(int width, int height, int depthBuffer, Action<RenderTexture> useTexture)
        {
            WithTemporaryTexture(width, height, depthBuffer, temporary =>
            {
                RenderTexture previousActiveTexture = RenderTexture.active;
                RenderTexture.active = temporary;

                useTexture(temporary);

                RenderTexture.active = previousActiveTexture;
            });
        }

        public static void WithTemporaryTexture(int width, int height, int depthBuffer, Action<RenderTexture> useTexture)
        {
            RenderTexture temporary = RenderTexture.GetTemporary(width, height, depthBuffer);
            useTexture(temporary);
            RenderTexture.ReleaseTemporary(temporary);
        }
    }
}