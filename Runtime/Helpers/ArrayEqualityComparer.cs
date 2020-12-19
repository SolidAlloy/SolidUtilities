namespace SolidUtilities.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// Generic EqualityComparer for an array of <typeparamref name="T"/> that allows for quick equality check of arrays
    /// and enables using them as keys in collections (provides GetHashCode method).
    /// Note that elements of the array must correctly pass equality check for this (override <see cref="object.Equals"/>).
    /// </summary>
    /// <typeparam name="T">The type of array elements.</typeparam>
    public sealed class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        private readonly EqualityComparer<T> _elementComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="customElementComparer">
        /// Custom element comparer. Uses <see cref="EqualityComparer&lt;T>.Default"/> by default.
        /// </param>
        public ArrayEqualityComparer(EqualityComparer<T> customElementComparer = null)
        {
            _elementComparer = customElementComparer ?? EqualityComparer<T>.Default;
        }

        public bool Equals(T[] first, T[] second)
        {
            if (first == second)
                return true;

            if (first == null || second == null)
                return false;

            if (first.Length != second.Length)
                return false;

            for (int i = 0; i < first.Length; i++)
            {
                if ( ! _elementComparer.Equals(first[i], second[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(T[] array)
        {
            unchecked
            {
                int hash = 17;

                foreach (T element in array)
                {
                    hash = hash * 31 + _elementComparer.GetHashCode(element);
                }

                return hash;
            }
        }
    }
}