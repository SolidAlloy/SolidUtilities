namespace SolidUtilities
{
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified <paramref name="key"/> and casts it to type <typeparamref name="TActual"/>.
        /// </summary>
        /// <param name="data">The dictionary to search for the value in.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified key, if the key is found;
        /// otherwise, the default value for the type of the <paramref name="value" /> parameter.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <typeparam name="TKey">Type of the key.</typeparam>
        /// <typeparam name="TValue">Type of the value in the dictionary.</typeparam>
        /// <typeparam name="TActual">Type to cast the value to when returning it.</typeparam>
        /// <returns>
        /// <see langword="true" /> if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element
        /// with the specified key; otherwise, <see langword="false" />.
        /// </returns>
        public static bool TryGetTypedValue<TKey, TValue, TActual>(this Dictionary<TKey, TValue> data, TKey key, out TActual value)
            where TActual : TValue
        {
            bool result = data.TryGetValue(key, out TValue tmp);
            value = (TActual)tmp;
            return result;
        }
    }
}