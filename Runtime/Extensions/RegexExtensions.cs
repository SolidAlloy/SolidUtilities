namespace SolidUtilities
{
    using System;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;

    /// <summary>
    /// Different useful extension methods for <see cref="Regex"/>.
    /// </summary>
    public static class RegexExtensions
    {
        /// <summary>
        /// Searches for a match in <paramref name="text"/> and returns it. If there is no match, throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="regex">The regex to use for search.</param>
        /// <param name="text">The text to search in.</param>
        /// <returns>Match that was found in the <paramref name="text"/>.</returns>
        /// <exception cref="ArgumentException">If no match was found in <paramref name="text"/>.</exception>
        [PublicAPI]
        public static string Find(this Regex regex, string text)
        {
            string result = regex.Match(text).Value;

            if (result.Length == 0)
                throw new ArgumentException($"Failed to find \"{regex}\" in the following text: \"{text}\".");

            return result;
        }
    }
}