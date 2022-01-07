namespace SolidUtilities
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public static class HashSetExtensions
    {
        /// <summary>
        /// Generates a new set that contains only the elements located in <paramref name="thisSet"/> and not in <paramref name="otherSet"/>.
        /// </summary>
        /// <param name="thisSet">This set.</param>
        /// <param name="otherSet">Other set.</param>
        /// <param name="comparer">Comparer to use for the new set.</param>
        /// <typeparam name="T">Type of the HashSet collection elements.</typeparam>
        /// <returns>New set that contains only the elements located in <paramref name="thisSet"/> and not in <paramref name="otherSet"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static HashSet<T> ExceptWith<T>(this HashSet<T> thisSet, HashSet<T> otherSet, EqualityComparer<T> comparer = null)
        {
            var newSet = new HashSet<T>(thisSet, comparer ?? EqualityComparer<T>.Default);
            newSet.ExceptWith(otherSet);
            return newSet;
        }

        /// <summary>
        /// Generates a new set that contains elements located in both <paramref name="thisSet"/> and <paramref name="otherSet"/>.
        /// </summary>
        /// <param name="thisSet">This set.</param>
        /// <param name="otherSet">Other set.</param>
        /// <param name="comparer">Comparer to use for the new set.</param>
        /// <typeparam name="T">Type of the HashSet collection elements.</typeparam>
        /// <returns>New set that contains elements located in both <paramref name="thisSet"/> and <paramref name="otherSet"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static HashSet<T> IntersectWith<T>(this HashSet<T> thisSet, HashSet<T> otherSet, EqualityComparer<T> comparer = null)
        {
            var newSet = new HashSet<T>(thisSet, comparer ?? EqualityComparer<T>.Default);
            newSet.IntersectWith(otherSet);
            return newSet;
        }

        /// <summary>
        /// Generates a new set that contains only the elements located in <paramref name="thisSet"/> and not in <paramref name="otherSet"/>.
        /// </summary>
        /// <param name="thisSet">This set.</param>
        /// <param name="otherSet">Other set.</param>
        /// <typeparam name="T">Type of the HashSet collection elements.</typeparam>
        /// <returns>New set that contains only the elements located in <paramref name="thisSet"/> and not in <paramref name="otherSet"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static HashSet<T> ExceptWithAndCreateNew<T>(this HashSet<T> thisSet, HashSet<T> otherSet)
        {
            var newSet = new HashSet<T>(thisSet);
            newSet.ExceptWith(otherSet);
            return newSet;
        }

        /// <summary>
        /// Generates a new set that contains elements located in both <paramref name="thisSet"/> and <paramref name="otherSet"/>.
        /// </summary>
        /// <param name="thisSet">This set.</param>
        /// <param name="otherSet">Other set.</param>
        /// <typeparam name="T">Type of the HashSet collection elements.</typeparam>
        /// <returns>New set that contains elements located in both <paramref name="thisSet"/> and <paramref name="otherSet"/>.</returns>
        [PublicAPI, NotNull, Pure]
        public static HashSet<T> IntersectWithAndCreateNew<T>(this HashSet<T> thisSet, HashSet<T> otherSet)
        {
            var newSet = new HashSet<T>(thisSet);
            newSet.IntersectWith(otherSet);
            return newSet;
        }

        /// <summary>
        /// Checks if <paramref name="thisSet"/> contains the same elements as <paramref name="array"/>. Works faster
        /// than the default <see cref="HashSet{T}.SetEquals"/> method because it does not access items through interface.
        /// </summary>
        /// <param name="thisSet">The set to compare.</param>
        /// <param name="array">The array to compare.</param>
        /// <typeparam name="T">Type of the items to compare.</typeparam>
        /// <returns>Whether <paramref name="thisSet"/> and <paramref name="array"/> contain the same elements.</returns>
        [PublicAPI, Pure]
        public static bool SetEqualsArray<T>(this HashSet<T> thisSet, T[] array)
        {
            int arrayLength = array.Length;

            for (int i = 0; i < arrayLength; i++)
            {
                if ( ! thisSet.Contains(array[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if <paramref name="thisSet"/> contains the same elements as <paramref name="list"/>. Works faster
        /// than the default <see cref="HashSet{T}.SetEquals"/> method because it does not access items through interface.
        /// </summary>
        /// <param name="thisSet">The set to compare.</param>
        /// <param name="list">The list to compare.</param>
        /// <typeparam name="T">Type of the items to compare.</typeparam>
        /// <returns>Whether <paramref name="thisSet"/> and <paramref name="list"/> contain the same elements.</returns>
        [PublicAPI, Pure]
        public static bool SetEqualsList<T>(this HashSet<T> thisSet, List<T> list)
        {
            int listCount = list.Count;

            for (int i = 0; i < listCount; i++)
            {
                if ( ! thisSet.Contains(list[i]))
                    return false;
            }

            return true;
        }
    }
}