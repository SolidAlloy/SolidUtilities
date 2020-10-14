namespace SolidUtilities.Extensions
{
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>Different useful extensions for <see cref="Vector2"/>.</summary>
    public static class Vector2Extensions
    {
        [PublicAPI] public static Rect Center(this Vector2 smallerRectSize, Rect outerRect)
        {
            float horizontalPadding = (outerRect.width - smallerRectSize.x) / 2f;
            float verticalPadding = (outerRect.height - smallerRectSize.y) / 2f;
            var smallerRectPosition = new Vector2(outerRect.x + horizontalPadding, outerRect.y + verticalPadding);
            return new Rect(smallerRectPosition, smallerRectSize);
        }
    }
}