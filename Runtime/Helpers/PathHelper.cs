namespace SolidUtilities
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;

    public static class PathHelper
    {
        /// <summary>
        /// Rebases file with path fromPath to folder with baseDir.
        /// </summary>
        /// <param name="fromPath">Full file path (absolute).</param>
        /// <param name="baseDir">Full base directory path (absolute).</param>
        /// <returns>Relative path to file in respect of baseDir.</returns>
        [PublicAPI]
        public static string MakeRelative(string fromPath, string baseDir)
        {
            const string pathSep = "\\";
            string fullFromPath = Path.GetFullPath(fromPath);
            // If folder contains upper folder references, they gets lost here. "c:\test\..\test2" => "c:\test2"
            string fullBaseDir = Path.GetFullPath(baseDir);

            string[] p1 = Regex.Split(fullFromPath, "[\\\\/]").Where(x => x.Length != 0).ToArray();
            string[] p2 = Regex.Split(fullBaseDir, "[\\\\/]").Where(x => x.Length != 0).ToArray();
            int i = 0;

            for (; i < p1.Length && i < p2.Length; i++)
            {
                if (string.Compare(p1[i], p2[i], StringComparison.OrdinalIgnoreCase) != 0)
                    break;
            }

            // Cannot make relative path, for example if resides on different drive
            if (i == 0)
                return fullFromPath;

            string r = string.Join(pathSep, Enumerable.Repeat("..", p2.Length - i).Concat(p1.Skip(i).Take(p1.Length - i)));
            return r;
        }

        /// <summary>
        /// Returns true if <paramref name="path"/> starts with the path <paramref name="baseDirPath"/>.
        /// The comparison is case-insensitive, handles / and \ slashes as folder separators and
        /// only matches if the base dir folder name is matched exactly ("c:\foobar\file.txt" is not a sub path of "c:\foo").
        /// </summary>
        [PublicAPI]
        public static bool IsSubPathOf(string path, string baseDirPath)
        {
            string normalizedPath = Path.GetFullPath(path.Replace('/', '\\').WithEnding("\\"));
            string normalizedBaseDirPath = Path.GetFullPath(baseDirPath.Replace('/', '\\').WithEnding("\\"));
            return normalizedPath.StartsWith(normalizedBaseDirPath, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns <paramref name="str"/> with the minimal concatenation of <paramref name="ending"/> (starting from end) that
        /// results in satisfying .EndsWith(ending).
        /// </summary>
        /// <example>"hel".WithEnding("llo") returns "hello", which is the result of "hel" + "lo".</example>
        private static string WithEnding([CanBeNull] this string str, string ending)
        {
            if (str == null)
                return ending;

            string result = str;

            // Right() is 1-indexed, so include these cases
            // * Append no characters
            // * Append up to N characters, where N is ending length
            for (int i = 0; i <= ending.Length; i++)
            {
                string tmp = result + ending.GetEnding(i);
                if (tmp.EndsWith(ending))
                    return tmp;
            }

            return result;
        }

        /// <summary>Gets the rightmost <paramref name="length" /> characters from a string.</summary>
        /// <param name="value">The string to retrieve the substring from.</param>
        /// <param name="length">The number of characters to retrieve.</param>
        /// <returns>The substring.</returns>
        private static string GetEnding([NotNull] this string value, int length)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "Length is less than zero");
            }

            return (length < value.Length) ? value.Substring(value.Length - length) : value;
        }
    }
}