namespace SolidUtilities.Editor
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents information about object found with the matching value of a variable. It contains the object type
    /// (scene object, prefab, etc.) and details about where the variable with the matching value was found (path to
    /// the asset, the component where it was found, etc.)
    /// </summary>
    public readonly struct FoundObject : IEnumerable<KeyValuePair<string, string>>
    {
        public readonly ObjectType Type;
        private readonly Dictionary<string, string> _details;

        public FoundObject(ObjectType objectType)
        {
            Type = objectType;
            _details = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            _details[key] = value;
        }

        public Dictionary<string, string>.Enumerator GetEnumerator() => _details.GetEnumerator();

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}