namespace SolidUtilities.Extensions
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
    }
}