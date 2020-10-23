namespace SolidUtilities.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using JetBrains.Annotations;

    /// <summary>Different useful extensions for <see cref="string"/>.</summary>
    public static class StringExtensions
    {
        #region IsValidPath

        private static readonly HashSet<char> InvalidFilenameChars = new HashSet<char>(Path.GetInvalidFileNameChars());

        /// <summary>Checks if the path is a valid Unity path.</summary>
        /// <param name="path">The path to check.</param>
        /// <returns><c>true</c> if the path is a valid Unity path.</returns>
        [PublicAPI] public static bool IsValidPath(this string path)
        {
            return path
                .Split('/')
                .All(filename => ! filename.Any(
                    character => InvalidFilenameChars.Contains(character)));
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
        private static readonly string[] Keywords =
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

        private static readonly Regex ValidIdentifierRegex = new Regex("^" + IdentifierOrKeyword + "$", RegexOptions.Compiled);

        /// <summary>Checks whether a string is a valid identifier (class name, namespace name, etc.)</summary>
        /// <param name="identifier">The string to check.</param>
        /// <returns><see langword="true"/> if the string is a valid identifier.</returns>
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
            if (ValidIdentifierRegex.IsMatch(normalizedIdentifier) && ! Keywords.Contains(normalizedIdentifier))
                return true;

            // 2. check if the identifier starts with @
            return normalizedIdentifier.StartsWith("@") && ValidIdentifierRegex.IsMatch(normalizedIdentifier.Substring(1));
        }

        #endregion
    }
}