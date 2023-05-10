using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HideAndSeek.Utils
{
    public static class Extensions
    {
        public static T GetNearest<T>(this IEnumerable<T> objects, Func<T, Vector3> getPosition, Vector3 position)
        {
            if (objects.Count() == 1) return objects.First();

            T nearest = default;
            float minDistance = Mathf.Infinity;

            foreach (T obj in objects)
            {
                float distance = Vector3.Distance(position, getPosition.Invoke(obj));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = obj;
                }
            }

            return nearest;
        }

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

        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            if (array.Count() == 0)
            {
                throw new ArgumentException("Array must be longer than one element");
            }

            return array.ElementAt(UnityEngine.Random.Range(0, array.Count()));
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
