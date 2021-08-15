namespace SolidUtilities.Extensions
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static void AddIfMissing<T>(this ICollection<T> list, T value)
        {
            if ( ! list.Contains(value))
                list.Add(value);
        }
    }
}