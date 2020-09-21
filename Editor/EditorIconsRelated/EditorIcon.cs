namespace SolidUtilities.Editor.EditorIconsRelated
{
  using Helpers;
  using UnityEngine;

  public class EditorIcon
  {
    public readonly Texture2D Default;
    public readonly Texture2D Active;
    public readonly Texture2D Highlighted;

    public EditorIcon(Texture2D icon)
    {
      Default = icon;
      Highlighted = GetIconWithMaterial(EditorIcons.Database.Highlighted);
      Active = GetIconWithMaterial(EditorIcons.Database.Active);
    }

    private Texture2D GetIconWithMaterial(Material material)
    {
      Texture2D newTexture = null;

      TextureHelper.WithSRGBWrite(() =>
      {
        TextureHelper.WithTemporaryActiveTexture(Default.width, Default.height, 0, temporary =>
        {
          GL.Clear(false, true, new Color(1f, 1f, 1f, 0f));
          Graphics.Blit(Default, temporary, material);

          newTexture = new Texture2D(Default.width, Default.width, TextureFormat.ARGB32, false, true)
          {
            filterMode = FilterMode.Bilinear
          };

          newTexture.ReadPixels(new Rect(0.0f, 0.0f, Default.width, Default.width), 0, 0);
          newTexture.alphaIsTransparency = true;
          newTexture.Apply();
        });
      });

      return newTexture;
    }
  }
}