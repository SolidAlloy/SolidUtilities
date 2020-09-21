namespace Utilities.Extensions
{
    using UnityEngine;

    public static class Texture2DExtensions
    {
        public static Texture2D Rotate(this Texture2D texture, bool clockwise = true)
        {
            var original = texture.GetPixels32();
            var rotated = new Color32[original.Length];
            int w = texture.width;
            int h = texture.height;
            int origLength = original.Length;

            for (int j = 0; j < h; ++j)
            {
                for (int i = 0; i < w; ++i)
                {
                    int rotIndex = (i + 1) * h - j - 1;
                    int origIndex = clockwise ? origLength - 1 - (j * w + i) : j * w + i;
                    rotated[rotIndex] = original[origIndex];
                }
            }

            var rotatedTexture = new Texture2D(h, w);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();
            return rotatedTexture;
        }

        public static void Draw(this Texture2D texture, Rect rect) => GUI.DrawTexture(rect, texture);
    }
}