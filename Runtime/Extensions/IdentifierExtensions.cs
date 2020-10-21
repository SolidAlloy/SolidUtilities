namespace SolidUtilities.Extensions
{
    using System.Collections;
    using System.Text.RegularExpressions;

    public static class IdentifierExtensions
    {
        // definition of a valid C# identifier: http://msdn.microsoft.com/en-us/library/aa664670(v=vs.71).aspx
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

        public static bool IsValidIdentifier(this string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                return false;

            string normalizedIdentifier = identifier.Normalize();

            // 1. check that the identifier match the valid identifier regex and it's not a C# keyword
            if (ValidIdentifierRegex.IsMatch(normalizedIdentifier) && ! ((IList) Keywords).Contains(normalizedIdentifier))
                return true;

            // 2. check if the identifier starts with @
            return normalizedIdentifier.StartsWith("@") && ValidIdentifierRegex.IsMatch(normalizedIdentifier.Substring(1));
        }
    }
}