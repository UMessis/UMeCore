namespace UMeGames.Core.Extensions
{
    using System;

    public static class ArrayExtensions
    {
        public static bool Contains<T>(this T[] array, T element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(element))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static T[] Add<T>(this T[] array, T newElement)
        {
            T[] newArray = new T[array.Length + 1];
            Array.Copy(array, newArray, array.Length);
            newArray[array.Length] = newElement;
            return newArray;
        }
        
        public static T[] AddRange<T>(this T[] array, T[] elementsToAdd)
        {
            T[] newArray = new T[array.Length + elementsToAdd.Length];
            Array.Copy(array, newArray, array.Length);
            Array.Copy(elementsToAdd, 0, newArray, array.Length, elementsToAdd.Length);
            return newArray;
        }
    }
}