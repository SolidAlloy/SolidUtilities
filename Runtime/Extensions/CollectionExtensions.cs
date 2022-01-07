namespace SolidUtilities
{
    using System.Collections.Generic;

    public static class ReadonlyCollectionExtensions
    {
        public static int IndexOf<T>(this IReadOnlyCollection<T> collection, T elementToFind)
        {
            int i = 0;

            foreach (T element in collection)
            {
                if (Equals(element, elementToFind))
                    return i;

                i++;
            }

            return -1;
        }
    }

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