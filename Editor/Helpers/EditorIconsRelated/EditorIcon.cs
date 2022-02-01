namespace SolidUtilities.Editor
{
    using System;
    using Editor;
    using JetBrains.Annotations;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Icon that can have different tints depending on its state: active, highlighted, etc. Useful for creating custom
    /// inspectors and drawers.
    /// </summary>
    [PublicAPI]
    public readonly struct EditorIcon : IDisposable
    {
        /// <summary>Icon with the default color.</summary>
        public readonly Texture2D Default;

        /// <summary>Icon with the active state tint.</summary>
        public readonly Texture2D Active;

        /// <summary>Icon with the highlighted state tint.</summary>
        public readonly Texture2D Highlighted;

        public EditorIcon(Texture2D icon)
        {
            // This way we can control the lifecycle of the textures ourselves, not fearing that the original texture will be destroyed.
            Default = new Texture2D(icon.width, icon.height, icon.format, icon.mipmapCount != 1);
            Graphics.CopyTexture(icon, Default);

            Highlighted = null;
            Active = null;

            Highlighted = GetIconWithMaterial(EditorIcons.Database.Highlighted);
            Active = GetIconWithMaterial(EditorIcons.Database.Active);
        }

        private Texture2D GetIconWithMaterial(Material material)
        {
            Texture2D newTexture = null;

            using (new TextureHelper.SRGBWriteScope(true))
            {
                using (var temporary = new TemporaryActiveTexture(Default.width, Default.height, 0))
                {
                    GL.Clear(false, true, new Color(1f, 1f, 1f, 0f));
                    Graphics.Blit(Default, temporary, material);

                    newTexture = new Texture2D(Default.width, Default.height, TextureFormat.ARGB32, false, true)
                    {
                        filterMode = FilterMode.Bilinear
                    };

                    newTexture.ReadPixels(new Rect(0.0f, 0.0f, Default.width, Default.height), 0, 0);
                    newTexture.alphaIsTransparency = true;
                    newTexture.Apply();
                }
            }

            return newTexture;
        }

        public void Dispose()
        {
            Object.DestroyImmediate(Default);
            Object.DestroyImmediate(Highlighted);
            Object.DestroyImmediate(Active);
        }
    }
}