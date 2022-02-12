namespace SolidUtilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;

    /// <summary>Different useful extensions for <see cref="string"/>.</summary>
    public static class StringExtensions
    {
        #region IsValidPath

        private static readonly HashSet<char> _invalidFilenameChars = new HashSet<char>(Path.GetInvalidFileNameChars());

        /// <summary>Checks if the path is a valid Unity path.</summary>
        /// <param name="path">The path to check.</param>
        /// <returns><c>true</c> if the path is a valid Unity path.</returns>
        [PublicAPI] public static bool IsValidPath(this string path)
        {
            return path
                .Split('/')
                .All(filename => ! filename.Any(
                    character => _invalidFilenameChars.Contains(character)));
        }

        #endregion

        #region IsValidIdentifier

        // definition of a valid C# identifier: https://www.programiz.com/csharp-programming/keywords-identifiers
        private const string FormattingCharacter = @"\p{Cf}";
        private const string ConnectingCharacter = @"\p{Pc}";
        private const string DecimalDigitCharacter = @"\p{Nd}";
        private const string CombiningCharacter = @"\p{Mn}|\p{Mc}";
        private const string LetterCharacter = @"\p{Lu}|\p{Ll}|\p{Lt}|\p{Lm}|\p{Lo}|\p{Nl}";

        private const string IdentifierPartCharacter = LetterCharacter + "|" +
                                                         DecimalDigitCharacter + "|" +
                                                         ConnectingCharacter + "|" +
                                                         CombiningCharacter + "|" +
                                                         FormattingCharacter;

        private const string IdentifierPartCharacters = "(" + IdentifierPartCharacter + ")+";
        private const string IdentifierStartCharacter = "(" + LetterCharacter + "|_)";

        private const string IdentifierOrKeyword = IdentifierStartCharacter + "(" +
                                                     IdentifierPartCharacters + ")*";

        // C# keywords: http://msdn.microsoft.com/en-us/library/x53a06bb(v=vs.71).aspx
        private static readonly HashSet<string> _keywords = new HashSet<string>
        {
            "abstract",  "event",      "new",        "struct",
            "as",        "explicit",   "null",       "switch",
            "base",      "extern",     "object",     "this",
            "bool",      "false",      "operator",   "throw",
            "break",     "finally",    "out",        "true",
            "byte",      "fixed",      "override",   "try",
            "case",      "float",      "params",     "typeof",
            "catch",     "for",        "private",    "uint",
            "char",      "foreach",    "protected",  "ulong",
            "checked",   "goto",       "public",     "unchecked",
            "class",     "if",         "readonly",   "unsafe",
            "const",     "implicit",   "ref",        "ushort",
            "continue",  "in",         "return",     "using",
            "decimal",   "int",        "sbyte",      "virtual",
            "default",   "interface",  "sealed",     "volatile",
            "delegate",  "internal",   "short",      "void",
            "do",        "is",         "sizeof",     "while",
            "double",    "lock",       "stackalloc",
            "else",      "long",       "static",
            "enum",      "namespace",  "string"
        };

        private static readonly Regex _validIdentifierRegex = new Regex("^" + IdentifierOrKeyword + "$", RegexOptions.Compiled);

        /// <summary>Checks whether a string is a valid identifier (class name, namespace name, etc.)</summary>
        /// <param name="identifier">The string to check.</param>
        /// <returns><see langword="true"/> if the string is a valid identifier.</returns>
        [PublicAPI, Pure]
        public static bool IsValidIdentifier(this string identifier)
        {
            return identifier.Contains('.')
                ? identifier.Split('.').All(IsValidIdentifierInternal)
                : IsValidIdentifierInternal(identifier);
        }

        // This is the pure IsValidIdentifier method that does not accept dot-separated identifiers.
        private static bool IsValidIdentifierInternal(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                return false;

            string normalizedIdentifier = identifier.Normalize();

            // 1. check that the identifier matches the valid identifier regex and it's not a C# keyword
            if (_validIdentifierRegex.IsMatch(normalizedIdentifier) && ! _keywords.Contains(normalizedIdentifier))
                return true;

            // 2. check if the identifier starts with @
            return normalizedIdentifier.StartsWith("@") && _validIdentifierRegex.IsMatch(normalizedIdentifier.Substring(1));
        }

        #endregion

        /// <summary>
        /// Returns a substring that follows the last occurence of <paramref name="character"/>.
        /// </summary>
        /// <param name="text">The string to search in.</param>
        /// <param name="character">The char to search for.</param>
        /// <returns>A substring that follows the last occurence of <paramref name="character"/>.</returns>
        [PublicAPI, Pure]
        public static string GetSubstringAfterLast(this string text, char character)
        {
            int lastCharIndex = text.LastIndexOf(character);
            return lastCharIndex == -1 ? text : text.Substring(lastCharIndex + 1, text.Length - lastCharIndex - 1);
        }

        [PublicAPI, Pure]
        public static string GetSubstringBeforeLast(this string text, char character)
        {
            int lastCharIndex = text.LastIndexOf(character);
            return lastCharIndex == -1 ? text : text.Substring(0, lastCharIndex);
        }

        [PublicAPI, Pure]
        public static string GetSubstringBefore(this string text, char character)
        {
            int charIndex = text.IndexOf(character);
            return charIndex == -1 ? text : text.Substring(0, charIndex);
        }
        
        [PublicAPI, Pure]
        public static string GetSubstringAfter(this string text, char character)
        {
            int charIndex = text.IndexOf(character);
            return charIndex == -1 ? text : text.Substring(charIndex + 1, text.Length - charIndex - 1);
        }

        /// <summary>
        /// Counts the number of times <paramref name="substring"/> occured in <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The string to search in.</param>
        /// <param name="substring">The substring to search for.</param>
        /// <returns>The number of times <paramref name="substring"/> occured in <paramref name="text"/>.</returns>
        [PublicAPI, Pure]
        public static int CountSubstrings(this string text, string substring) =>
            (text.Length - text.Replace(substring, string.Empty).Length) / substring.Length;

        public static int CountChars(this string text, char character)
        {
            int count = 0;
            int textLength = text.Length;

            for (int i = 0; i < textLength; i++)
            {
                if (text[i] == character)
                    count++;
            }

            return count;
        }

        [PublicAPI]
        public static int IndexOfNth(this string str, char chr, int nth = 0)
        {
            if (nth < 0)
                throw new ArgumentException("Can not find a negative index of substring in string. Must start with 0");

            int offset = str.IndexOf(chr);
            for (int i = 0; i < nth; i++)
            {
                if (offset == -1) return -1;
                offset = str.IndexOf(chr, offset + 1);
            }

            return offset;
        }
    }
}