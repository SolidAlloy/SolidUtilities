namespace SolidUtilities.Extensions
{
    using UnityEngine;

    public static class RectExtensions
    {
        public static void RoundUpCoordinates(this ref Rect rect)
        {
            rect.x = Mathf.Round(rect.x);
            rect.y = Mathf.Round(rect.y);
            rect.width = Mathf.Round(rect.width);
            rect.height = Mathf.Round(rect.height);
        }

        /// <summary>
        /// Cuts a big rect into two smaller ones by placing a vertical cut at <paramref name="cutDistance"/> from the
        /// left or right border of the rect.
        /// </summary>
        /// <param name="originalRect">The rect that should be split.</param>
        /// <param name="cutDistance">The distance from the left or right border of the rect where to place vertical cut.</param>
        /// <param name="fromRightBorder">Whether to count the distance from left or right border.</param>
        /// <returns>Left and right rects that appeared after the cut.</returns>
        public static (Rect leftRect, Rect rightRect) CutVertically(this Rect originalRect, float cutDistance, bool fromRightBorder = false)
        {
            Rect leftRect, rightRect;

            Vector2 leftRectPos = originalRect.position;
            Vector2 RightRectPos() => new Vector2(originalRect.x + leftRect.width, originalRect.y);
            var cutDistanceSize = new Vector2(cutDistance, originalRect.height);
            var leftoverSize = new Vector2(originalRect.width - cutDistance, originalRect.height);

            if (fromRightBorder)
            {
                leftRect = new Rect(leftRectPos, leftoverSize);
                rightRect = new Rect(RightRectPos(), cutDistanceSize);
            }
            else
            {
                leftRect = new Rect(leftRectPos, cutDistanceSize);
                rightRect = new Rect(RightRectPos(), leftoverSize);
            }

            return (leftRect, rightRect);
        }

        public static Rect AddHorizontalPadding(this Rect rect, float leftPadding, float rightPadding)
        {
            rect.xMin += leftPadding;
            rect.width -= rightPadding;
            return rect;
        }

        public static Rect AlignMiddleVertically(this Rect rect, float height)
        {
            rect.y = rect.y + rect.height * 0.5f - height * 0.5f;
            rect.height = height;
            return rect;
        }
    }
}