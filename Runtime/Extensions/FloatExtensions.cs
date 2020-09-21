namespace SolidUtilities.Extensions
{
    using System;

    public static class FloatExtensions
    {
        private const float Tolerance = 0.01f;

        public static bool ApproximatelyEquals(this float firstNum, float secondNum)
        {
            return Math.Abs(firstNum - secondNum) < Tolerance;
        }
    }
}