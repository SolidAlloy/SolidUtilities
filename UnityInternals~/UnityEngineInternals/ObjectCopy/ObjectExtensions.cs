namespace SolidUtilities.UnityEngineInternals
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using JetBrains.Annotations;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates a shallow copy of the object.
        /// </summary>
        /// <param name="obj">Object to copy.</param>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <returns>A shallow copy of the object</returns>
        [PublicAPI]
        public static T ShallowCopy<T>(this T obj) => typeof(T).IsClass ? (T) obj.MemberwiseClone() : obj;

        /// <summary>
        /// Creates a deep copy of the object.
        /// </summary>
        /// <param name="obj">Object to copy.</param>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <returns>A deep copy of the object.</returns>
        /// <remarks>The code is from https://github.com/chivandikwa/net-object-deep-copy excluding custom attributes
        /// to do shallow copy / ignore copy.</remarks>
        [PublicAPI]
        public static T DeepCopy<T>(this T obj)
        {
            return Cloner.Create().Clone(obj);
        }

        /// <summary> Determines whether this instance is primitive. </summary>
        /// <param name="type"> The type. </param>
        /// <returns> <c>true</c> if the type is primitive. </returns>
        private static bool IsPrimitive(this Type type)
        {
            return (type.IsValueType && type.IsPrimitive)
                   || type == typeof(string)
                   || type == typeof(decimal)
                   || type == typeof(DateTime);
        }

        private readonly struct Cloner
        {
            private readonly Dictionary<Type, List<FieldInfo>> _fieldsRequiringDeepClone;

            public static Cloner Create() => new Cloner(new Dictionary<Type, List<FieldInfo>>());

            private Cloner(Dictionary<Type, List<FieldInfo>> emptyDict) => _fieldsRequiringDeepClone = emptyDict;

            public T Clone<T>(T obj)
            {
                return (T) ExecuteClone(obj, new Dictionary<object, object>(new ReferenceEqualityComparer()),
                    true);
            }

            private object ExecuteClone(object originalObject, Dictionary<object, object> visited, bool checkObjectGraph)
            {
                if (originalObject == null)
                    return null;

                Type typeToReflect = originalObject.GetType();

                if (typeToReflect.IsPrimitive())
                {
                    return originalObject;
                }

                if (checkObjectGraph && visited.ContainsKey(originalObject))
                    return visited[originalObject];

                if (originalObject is Delegate)
                    return null;

                object cloneObject;

                if (typeToReflect.IsArray)
                {
                    Type arrayType = typeToReflect.GetElementType();
                    var originalArray = (Array) originalObject;
                    var clonedArray = Array.CreateInstance(arrayType, originalArray.Length);
                    cloneObject = clonedArray;

                    if (checkObjectGraph) visited.Add(originalObject, cloneObject);

                    if (arrayType.IsPrimitive())
                    {
                        // ignore array of primitive, shallow copy will suffice
                    }
                    else if ( ! typeToReflect.IsPrimitive())
                    {
                        if (clonedArray.LongLength != 0)
                        {
                            var walker = new ArrayTraverse(clonedArray);

                            do
                            {
                                clonedArray.SetValue(
                                    ExecuteClone(originalArray.GetValue(walker.Position), visited, !arrayType.IsValueType),
                                    walker.Position);
                            }
                            while (walker.Step());
                        }
                    }
                    else
                    {
                        Array.Copy(originalArray, clonedArray, clonedArray.Length);
                    }
                }
                else
                {
                    cloneObject = originalObject.MemberwiseClone();

                    if (checkObjectGraph)
                        visited.Add(originalObject, cloneObject);
                }

                CopyFields(originalObject, visited, cloneObject, typeToReflect);
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
                return cloneObject;
            }

            private void RecursiveCopyBaseTypePrivateFields(object originalObject, Dictionary<object, object> visited,
                object cloneObject, Type typeToReflect)
            {
                if (typeToReflect.BaseType == null)
                    return;

                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, info => info.IsPrivate);
            }

            private List<FieldInfo> CachedFieldsRequiringDeepClone(Type typeToReflect)
            {
                if (_fieldsRequiringDeepClone.TryGetValue(typeToReflect, out var result))
                    return result;

                result = FieldsRequiringDeepClone(typeToReflect);
                _fieldsRequiringDeepClone[typeToReflect] = result;
                return result;
            }

            private static List<FieldInfo> FieldsRequiringDeepClone(Type typeToReflect)
            {
                const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

                var typeFields = typeToReflect.GetFields(bindingFlags);

                var totalFields = new List<FieldInfo>(typeFields.Length);

                for (int i = 0; i < typeFields.Length; i++)
                {
                    totalFields.Add(typeFields[i]);
                }

                while (typeToReflect.BaseType != null)
                {
                    typeToReflect = typeToReflect.BaseType;

                    typeFields = typeToReflect.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

                    for (int i = 0; i < typeFields.Length; i++)
                    {
                        var fieldInfo = typeFields[i];

                        if (fieldInfo.IsPrivate)
                            totalFields.Add(fieldInfo);
                    }
                }

                return totalFields;
            }

            private void CopyFields(object originalObject, Dictionary<object, object> visited, object cloneObject,
                Type typeToReflect, Func<FieldInfo, bool> filter = null)
            {
                foreach (FieldInfo fieldInfo in CachedFieldsRequiringDeepClone(typeToReflect))
                {
                    if ((filter != null && ! filter(fieldInfo)) || fieldInfo.FieldType.IsPrimitive())
                        continue;

                    object originalFieldValue = fieldInfo.GetValue(originalObject);
                    object clonedFieldValue = ExecuteClone(originalFieldValue, visited, ! fieldInfo.FieldType.IsValueType);
                    fieldInfo.SetValue(cloneObject, clonedFieldValue);
                }
            }
        }
    }
}