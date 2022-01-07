namespace SolidUtilities
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Different useful extensions for <see cref="float"/>.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>Determines if the numbers are equal with a certain tolerance.</summary>
        /// <param name="firstNum">First number.</param>
        /// <param name="secondNum">Second number.</param>
        /// <param name="tolerance">Tolerance by which to determine if the numbers are equal. Default is 0.01f.</param>
        /// <returns>Whether the numbers are equal with tolerance.</returns>
        /// <example><code>
        /// if (contentHeight.ApproximatelyEquals(position.height))
        ///    return true;
        /// </code></example>
        [PublicAPI] public static bool ApproximatelyEquals(this float firstNum, float secondNum, float tolerance = 0.01f)
        {
            return Math.Abs(firstNum - secondNum) < tolerance;
        }

        /// <summary>Determines if the number are not equal with a certain tolerance.</summary>
        /// <param name="firstNum">First number.</param>
        /// <param name="secondNum">Second number.</param>
        /// <param name="tolerance">Tolerance by which to determine if the numbers are equal. Default is 0.01f.</param>
        /// <returns>Whether the numbers are not equal with tolerance.</returns>
        /// <example><code>
        /// if (_optimalWidth.DoesNotEqualApproximately(position.width))
        ///     this.Resize(_optimalWidth);
        /// </code></example>
        [PublicAPI] public static bool DoesNotEqualApproximately(this float firstNum, float secondNum, float tolerance = 0.01f) =>
            !firstNum.ApproximatelyEquals(secondNum, tolerance);
    }
}