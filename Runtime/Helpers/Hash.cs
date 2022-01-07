namespace SolidUtilities
{
    using System.Security.Cryptography;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>Collection of useful hashing methods.</summary>
    [PublicAPI]
    public static class Hash
    {
        /// <summary>
        /// Hashes <paramref name="input"/> using <see cref="SHA1Managed"/> and converts bytes array to string.
        /// </summary>
        /// <param name="input">The string to hash.</param>
        /// <returns>SHA1 hash converted to string.</returns>
        /// <exception cref="System.ArgumentNullException">If <paramref name="input"/> is null.</exception>
        [NotNull]
        public static string SHA1(string input)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(input);

            byte[] hashBytes;

            using (var sha1 = new SHA1Managed())
                hashBytes = sha1.ComputeHash(plaintextBytes);

            var sb = new StringBuilder();

            foreach (byte hashByte in hashBytes)
                sb.AppendFormat("{0:x2}", hashByte);

            return sb.ToString();
        }
    }
}