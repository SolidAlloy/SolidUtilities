namespace SolidUtilities.Editor
{
    using System.Collections.Generic;
    using Editor;
    using JetBrains.Annotations;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Provides methods that allow to copy values from one <see cref="UnityEngine.Object"/> to another.
    /// It works with <see cref="SerializedObject"/> too.
    /// </summary>
    public static class SerializedObjectValuesCopier
    {
        /// <summary>
        /// Copies values of the visible properties from <paramref name="otherObject"/> to <paramref name="thisSerializedObject"/>.
        /// </summary>
        /// <param name="thisSerializedObject">Destination object.</param>
        /// <param name="otherObject">Source object.</param>
        /// <param name="excludeValues">Names of properties to exclude from copying.</param>
        [PublicAPI]
        public static void CopyValuesFrom(this SerializedObject thisSerializedObject, Object otherObject, HashSet<string> excludeValues = null)
        {
            var otherSerializedObject = new SerializedObject(otherObject);
            thisSerializedObject.CopyValuesFrom(otherSerializedObject, excludeValues);
        }

        /// <summary>
        /// Copies values of the visible properties from <paramref name="otherObject"/> to <paramref name="thisObject"/>.
        /// </summary>
        /// <param name="thisObject">Destination object.</param>
        /// <param name="otherObject">Source object.</param>
        /// <param name="excludeValues">Names of properties to exclude from copying.</param>
        [PublicAPI]
        public static void CopyValuesFrom(this Object thisObject, Object otherObject,
            HashSet<string> excludeValues = null)
        {
            var thisSerializedObject = new SerializedObject(thisObject);
            var otherSerializedObject = new SerializedObject(otherObject);
            thisSerializedObject.CopyValuesFrom(otherSerializedObject, excludeValues);
        }

        /// <summary>
        /// Copies values of the visible properties from <paramref name="otherObject"/> to <paramref name="thisObject"/>.
        /// </summary>
        /// <param name="thisObject">Destination object.</param>
        /// <param name="otherObject">Source object.</param>
        /// <param name="excludeValues">Names of properties to exclude from copying.</param>
        [PublicAPI]
        public static void CopyValuesFrom(this Object thisObject, SerializedObject otherObject,
            HashSet<string> excludeValues = null)
        {
            var thisSerializedObject = new SerializedObject(thisObject);
            thisSerializedObject.CopyValuesFrom(otherObject, excludeValues);
        }

        /// <summary>
        /// Copies values of the visible properties from <paramref name="otherObject"/> to <paramref name="thisObject"/>.
        /// </summary>
        /// <param name="thisObject">Destination object.</param>
        /// <param name="otherObject">Source object.</param>
        /// <param name="excludeValues">Names of properties to exclude from copying.</param>
        [PublicAPI]
        public static void CopyValuesFrom(this SerializedObject thisObject, SerializedObject otherObject,
            HashSet<string> excludeValues = null)
        {
            var otherObjectProps = new ChildProperties(otherObject);

            foreach (SerializedProperty childProperty in otherObjectProps)
            {
                if (excludeValues?.Contains(childProperty.name) == true)
                    continue;

                thisObject.CopyFromSerializedPropertyIfDifferent(childProperty);
            }

            if (thisObject.hasModifiedProperties)
                thisObject.ApplyModifiedProperties();
        }
    }
}