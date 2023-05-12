using System.Collections.Generic;

namespace HideAndSeek.Utils
{
    public static class Utilities
    {
        public static bool GetChance(float chance)
        {
            System.Random rand = new System.Random();
            int value = rand.Next(1, 101);

            return value <= chance;
        }

        /// <summary>
        /// Get random number excluded some.
        /// Excluded values should be sorted in ascending order
        /// </summary>
        public static int RandomExceptValues(int min, int max, params int[] excluded)
        {
            int result = UnityEngine.Random.Range(min, max);

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
    }
}
