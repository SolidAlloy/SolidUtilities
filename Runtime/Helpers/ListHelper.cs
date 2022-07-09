namespace SolidUtilities
{
    using System.Collections.Generic;

    public static class ListHelper
    {
        public static List<T> Empty<T>()
        {
            return EmptyList<T>.Value;
        }

        private static class EmptyList<T>
        {
            public static readonly List<T> Value = new List<T>(0);
        }
    }
}