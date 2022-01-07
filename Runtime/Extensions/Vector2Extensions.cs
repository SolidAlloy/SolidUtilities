namespace SolidUtilities
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    /// <summary>Different useful extensions for <see cref="Vector2"/>.</summary>
    public static class Vector2Extensions
    {
        /// <summary>Creates a smaller rect with the given size inside of a bigger rect.</summary>
        /// <param name="smallerRectSize">The width and height of a smaller rect.</param>
        /// <param name="outerRect">The bigger rect.</param>
        /// <returns>A rect centered inside of <paramref name="outerRect"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If x or y coordinates of <paramref name="smallerRectSize"/> are bigger than width or height
        /// of <paramref name="outerRect"/>.
        /// </exception>
        [PublicAPI] public static Rect Center(this Vector2 smallerRectSize, Rect outerRect)
        {
            if (smallerRectSize.x > outerRect.width)
            {
                throw new ArgumentOutOfRangeException($"The x value of {nameof(smallerRectSize)} must be " +
                                                      $"less or equal to {nameof(outerRect)}.width. Actual values " +
                                                      $"were: {smallerRectSize.x} > {outerRect.width}.");
            }

            if (smallerRectSize.y > outerRect.height)
            {
                throw new ArgumentOutOfRangeException($"The y value of {nameof(smallerRectSize)} must be " +
                                                      $"less or equal to {nameof(outerRect)}.height. Actual values " +
                                                      $"were: {smallerRectSize.y} > {outerRect.height}.");
            }

            float horizontalPadding = (outerRect.width - smallerRectSize.x) / 2f;
            float verticalPadding = (outerRect.height - smallerRectSize.y) / 2f;
            var smallerRectPosition = new Vector2(outerRect.x + horizontalPadding, outerRect.y + verticalPadding);
            return new Rect(smallerRectPosition, smallerRectSize);
        }

        /// <summary>Returns a new vector with values rounded up to the nearest integers.</summary>
        /// <param name="vector">The vector which values to round up.</param>
        /// <returns>New vector with rounded values.</returns>
        [PublicAPI]
        public static Vector2 RoundUp(this Vector2 vector)
        {
            vector.x = Mathf.Round(vector.x);
            vector.y = Mathf.Round(vector.y);
            return vector;
        }
    }
}