namespace SolidUtilities
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;

    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Checks whether the member has a custom attribute of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="member">The member to check.</param>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <returns><c>true</c> if the type has a custom attribute of type <typeparamref name="T"/>.</returns>
        [PublicAPI, Pure]
        public static bool HasAttribute<T>(this MemberInfo member)
            where T : Attribute
        {
            // GetCustomAttributes() skips a number of null checks and a pretty long method call chain.
            return member.GetCustomAttributes(typeof(T), true).Length != 0;
        }
    }
}