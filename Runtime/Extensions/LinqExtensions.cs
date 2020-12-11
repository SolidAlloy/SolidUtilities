namespace SolidUtilities.Extensions
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>Different useful LINQ extensions.</summary>
    public static class LinqExtensions
    {
        /// <summary>Performs an action on each item.</summary>
        /// <param name="source">The source to enumerate.</param>
        /// <param name="action">The action to perform.</param>
        [PublicAPI]
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T obj in source)
                action(obj);

            return source;
        }
    }
}