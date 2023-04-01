using System;
using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek.Utils
{
    public static class Extensions
    {
        public static T MaxBy<T, U>(this IEnumerable<T> items, Func<T, U> selector)
        {
            if (!items.Any())
            {
                throw new InvalidOperationException("Empty input sequence");
            }

            var comparer = Comparer<U>.Default;
            T maxItem = items.First();
            U maxValue = selector(maxItem);

            foreach (T item in items.Skip(1))
            {
                U value = selector(item);
                if (comparer.Compare(value, maxValue) > 0)
                {
                    maxValue = value;
                    maxItem = item;
                }
            }

            return maxItem;
        }

        public static float CalculateRatio(this float value, float newMin, float newMax, float oldMin = 0, float oldMax = 1)
        {
            float step = (value - oldMin) / (oldMax - oldMin);
            float newLength = newMax - newMin;
            return newMin + step * newLength;
        }

        /// <summary>
        /// Get random number excluded some.
        /// Excluded values should be sorted in ascending order
        /// </summary>
        public static int RandomExceptValues(int min, int max, int[] excluded)
        {
            int result = UnityEngine.Random.Range(min, max + 1);

            for (int i = 0; i < excluded.Length; i++)
            {
                if (result < excluded[i]) return result;

                result++;
            }

            return result;
        }

        /// <summary>
        /// Get random number excluded some.
        /// Excluded values should be sorted in ascending order
        /// </summary>
        public static int RandomExceptValues(int min, int max, List<int> excluded)
        {
            int result = UnityEngine.Random.Range(min, max - excluded.Count);

            for (int i = 0; i < excluded.Count; i++)
            {
                if (result < excluded[i]) return result;

                result++;
            }

            return result;
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                throw new ArgumentException("Array must be longer than one element");
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list.Count == 0)
            {
                throw new ArgumentException("List must be longer than one element");
            }

            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}
