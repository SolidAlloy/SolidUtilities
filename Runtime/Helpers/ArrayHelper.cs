namespace SolidUtilities
{
    using System;

    public static class ArrayHelper
    {
        public static void Add<T>(ref T[] array, T item)
        {
            if (array == null)
            {
                array = new T[] { item };
                return;
            }

            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        public static void RemoveAt<T>(ref T[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException($"The passed index {index} is out of bounds of the array with length {array.Length}");

            var newArray = new T[array.Length - 1];

            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                    continue;

                newArray[j] = array[i];
                j++;
            }

            array = newArray;
        }

        public static bool Remove<T>(ref T[] array, T item)
        {
            int index = Array.IndexOf(array, item);

            if (index == -1)
                return false;

            RemoveAt(ref array, index);
            return true;
        }
    }
}