namespace SolidUtilities.Extensions
{
    using UnityEngine;

    /// <summary>Different useful extensions for <see cref="Texture2D"/></summary>
    public static class Texture2DExtensions
    {
        /// <summary>Rotates the input texture by 90 degrees and returns the new rotated texture.</summary>
        /// <param name="texture">Texture to rotate.</param>
        /// <param name="clockwise">Whether to rotate the texture clockwise.</param>
        /// <returns>The rotated texture.</returns>
        public static Texture2D Rotate(this Texture2D texture, bool clockwise = true)
        {
            var original = texture.GetPixels32();
            var rotated = new Color32[original.Length];
            int textureWidth = texture.width;
            int textureHeight = texture.height;
            int origLength = original.Length;

            for (int heightIndex = 0; heightIndex < textureHeight; ++heightIndex)
            {
                for (int widthIndex = 0; widthIndex < textureWidth; ++widthIndex)
                {
                    int rotIndex = (widthIndex + 1) * textureHeight - heightIndex - 1;

                    int origIndex = clockwise
                        ? origLength - 1 - (heightIndex * textureWidth + widthIndex)
                        : heightIndex * textureWidth + widthIndex;

                    rotated[rotIndex] = original[origIndex];
                }
            }

            var rotatedTexture = new Texture2D(textureHeight, textureWidth);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();
            return rotatedTexture;
        }

        public static void Draw(this Texture2D texture, Rect rect) => GUI.DrawTexture(rect, texture);
    }
}