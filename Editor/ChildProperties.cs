namespace SolidUtilities.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Extensions;
    using JetBrains.Annotations;
    using UnityEditor;

    /// <summary>
    /// Allows iterating over child properties of a serialized object without entering nested properties.
    /// </summary>
    /// <example><code>
    /// var childProperties = new ChildProperties(serializedObject, true);
    /// foreach (var child in childProperties)
    /// {
    ///     FieldInfo field = targetType.GetFieldAtPath(child.propertyPath);
    ///     Draw(field);
    /// }
    /// </code></example>
    [PublicAPI] public class ChildProperties : IEnumerator<SerializedProperty>, IEnumerable<SerializedProperty>
    {
        private readonly SerializedObject _parentObject;
        private readonly bool _enterChildren;
        private readonly bool _excludeBuiltInProperties;
        private SerializedProperty _currentProp;
        private bool _nextPropertyExists;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildProperties"/> class.
        /// </summary>
        /// <param name="parentObject">The parent serialized object which child properties you want to inspect.</param>
        /// <param name="enterChildren">Whether to iterate through child properties recursively. <c>false</c> by default.</param>
        /// <param name="excludeBuiltInProperties">Whether to exclude built-in properties from the iteration. <c>true</c> by default.</param>
        public ChildProperties(SerializedObject parentObject, bool enterChildren = false, bool excludeBuiltInProperties = true)
        {
            _parentObject = parentObject;
            _enterChildren = enterChildren;
            _excludeBuiltInProperties = excludeBuiltInProperties;
            Reset();
        }

        SerializedProperty IEnumerator<SerializedProperty>.Current => _currentProp;
        object IEnumerator.Current => _currentProp;

        bool IEnumerator.MoveNext()
        {
            if ( ! _nextPropertyExists)
                return false;

            _nextPropertyExists = _currentProp.Next(_enterChildren);

            if (_excludeBuiltInProperties)
            {
                while (_nextPropertyExists && _currentProp.IsBuiltIn())
                    _nextPropertyExists = _currentProp.Next(_enterChildren);
            }

            return _nextPropertyExists;
        }

        public void Reset()
        {
            _currentProp = _parentObject.GetIterator();
            _nextPropertyExists = _currentProp.Next(true);
        }

        void IDisposable.Dispose() { }

        IEnumerator<SerializedProperty> IEnumerable<SerializedProperty>.GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => this;
    }
}