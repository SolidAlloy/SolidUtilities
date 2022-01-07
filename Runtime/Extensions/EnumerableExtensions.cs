﻿namespace SolidUtilities
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts the collection to HashSet.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <typeparam name="T">Type of the collection items.</typeparam>
        /// <returns>HashSet containing of the collection items.</returns>
        /// <exception cref="ArgumentNullException">If source is null.</exception>
        [PublicAPI, NotNull] public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source) => new HashSet<T>(source);
    }
}