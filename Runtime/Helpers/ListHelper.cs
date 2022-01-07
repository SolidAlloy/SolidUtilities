namespace SolidUtilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class ListHelper
    {
        private static readonly Dictionary<Type, IList> _lists = new Dictionary<Type, IList>();

        public static List<T> Empty<T>()
        {
            Type type = typeof(T);

            if (_lists.TryGetValue(type, out IList list))
                return (List<T>) list;

            var newList = new List<T>();
            _lists.Add(type, newList);
            return newList;
        }
    }
}