namespace SolidUtilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public interface IKeysValuesHolder<out TKey, out TValue>
    {
        IReadOnlyList<TKey> Keys { get; }

        IReadOnlyList<TValue> Values { get; }
    }

    /// <summary>
    /// A dictionary that can be iterated as fast as a List at the cost of a larger memory footprint and
    /// slower <see cref="Remove(System.Collections.Generic.KeyValuePair{TKey,TValue})"/> execution.
    /// </summary>
    /// <typeparam name="TKey">Type of the key.</typeparam>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <remarks>
    /// Besides making iteration faster, the speed of <see cref="Count"/>, <see cref="Keys"/>, and <see cref="Values"/> is also increased
    /// </remarks>
    public class FastIterationDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IKeysValuesHolder<TKey, TValue>
    {
        private readonly List<TKey> _keys;
        private readonly List<TValue> _values;
        private readonly Dictionary<TKey, TValue> _dictionary;

        [PublicAPI]
        public FastIterationDictionary(int capacity = 3)
        {
            _keys = new List<TKey>(capacity);
            _values = new List<TValue>(capacity);
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => Add(key, value);
        }

        public IReadOnlyList<TKey> Keys => _keys;

        public IReadOnlyList<TValue> Values => _values;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => _values;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => _keys;

        /// <summary>
        /// Allows to update fields of a <see cref="key"/> that take part in calculating hash of the object without
        /// losing reference to the object in the dictionary.
        /// </summary>
        /// <param name="key">A key to update.</param>
        /// <param name="updateKey">The action to take on key.</param>
        /// <exception cref="KeyNotFoundException">If the key is not found in the database.</exception>
        [PublicAPI]
        public void UpdateKey(TKey key, Action updateKey)
        {
            if (! _dictionary.TryGetValue(key, out TValue value))
                throw new KeyNotFoundException($"'{key}' key was not found in the database.");

            _dictionary.Remove(key);

            updateKey();

            _dictionary.Add(key, value);
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        public void Clear()
        {
            _dictionary.Clear();
            _keys.Clear();
            _values.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.TryGetValue(item.Key, out TValue value) &&
                   EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if ( ! _dictionary.Remove(item.Key))
                return false;

            _keys.Remove(item.Key);
            _values.Remove(item.Value);

            return true;
        }

        public int Count => _keys.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            _keys.Add(key);
            _values.Add(value);
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public bool Remove(TKey key)
        {
            if ( ! _dictionary.TryGetValue(key, out TValue value))
                return false;

            _dictionary.Remove(key);
            _keys.Remove(key);
            _values.Remove(value);
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() =>
            GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).CopyTo(array, arrayIndex);

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) =>
            Add(item.Key, item.Value);

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private List<TKey>.Enumerator _keyEnumerator;
            private List<TValue>.Enumerator _valueEnumerator;

            public Enumerator(FastIterationDictionary<TKey, TValue> dictionary)
            {
                _keyEnumerator = dictionary._keys.GetEnumerator();
                _valueEnumerator = dictionary._values.GetEnumerator();
            }

            public bool MoveNext()
            {
                _keyEnumerator.MoveNext();
                return _valueEnumerator.MoveNext();
            }

            public void Reset()
            {
                ((IEnumerator)_keyEnumerator).Reset();
                ((IEnumerator)_valueEnumerator).Reset();
            }

            public KeyValuePair<TKey, TValue> Current => new KeyValuePair<TKey, TValue>(_keyEnumerator.Current, _valueEnumerator.Current);

            object IEnumerator.Current => Current;

            public void Dispose() { }
        }
    }
}