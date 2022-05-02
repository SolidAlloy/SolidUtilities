namespace SolidUtilities.Editor
{
    using System.Collections.Generic;
    using UnityEditor;

    public static class SerializedPropertyHelper
    {
        public static IEnumerable<SerializedProperty> FindPropertiesOfType(IEnumerable<SerializedObject> serializedObjects, string type)
        {
            foreach (var serializedObject in serializedObjects)
            {
                foreach (var serializedProperty in FindPropertiesOfType(serializedObject, type))
                {
                    yield return serializedProperty;
                }
            }
        }

        private static IEnumerable<SerializedProperty> FindPropertiesOfType(SerializedObject serializedObject, string type)
        {
            var prop = serializedObject.GetIterator();

            if (!prop.Next(true))
                yield break;

            do
            {
                if (prop.type.GetSubstringBefore('`') != type)
                    continue;
                
                if (prop.isArray)
                {
                    int arrayLength = prop.arraySize;

                    for (int i = 0; i < arrayLength; i++)
                    {
                        yield return prop.GetArrayElementAtIndex(i);
                    }
                }
                else
                {
                    yield return prop;
                }
            }
            while (prop.NextVisible(true));
        }
    }
}