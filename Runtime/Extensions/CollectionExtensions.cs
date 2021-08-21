namespace SolidUtilities.Extensions
{
    using System.Collections.Generic;

    public static class CollectionExtensions
    {
        public static bool AddIfMissing<T>(this ICollection<T> list, T value)
        {
            if (list.Contains(value))
                return false;

            list.Add(value);
            return true;
        }
    }
}