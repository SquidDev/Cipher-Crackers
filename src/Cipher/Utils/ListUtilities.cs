using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Utils
{
    public static class ListUtilities
    {
        public static void Swap<T>(this IList<T> items, int x, int y)
        {
            if (x == y) return;

            T Temp = items[x];
            items[x] = items[y];
            items[y] = Temp;
        }

        public static IEnumerable<T[]> PermutationsRepeat<T>(this IEnumerable<T> items, int count)
        {
            return PermutationsRepeat(items, new T[count], count - 1);
        }

        private static IEnumerable<T[]> PermutationsRepeat<T>(IEnumerable<T> items, T[] results, int count)
        {
            foreach (T item in items)
            {
                results[count] = item;
                if (count == 0)
                {
                    yield return results;
                }
                else
                {
                    foreach (T[] res in PermutationsRepeat(items, results, count - 1))
                    {
                        yield return res;
                    }
                }
            }
        }

        /// <summary>
        /// Permutations of a List
        /// </summary>
        /// <param name="items">List to use</param>
        /// <returns>Enumerable of all permutations</returns>
        public static IEnumerable<T[]> Permutations<T>(this IList<T> items)
        {
            return items.Permutations(items.Count);
        }

        /// <summary>
        /// Permutations of a List
        /// </summary>
        /// <param name="items">List to use</param>
        /// <param name="r">Number to choose from</param>
        /// <returns>Enumerable of all permutations</returns>
        public static IEnumerable<T[]> Permutations<T>(this IEnumerable<T> items, int r)
        {
        	T[] results = items.ToArray();
            return Permutations(results, 0, results.Length - 1);
        }

        private static IEnumerable<T[]> Permutations<T>(T[] results, int k, int m)
        {
            if (k == m)
            {
                yield return results;
            }
            else
            {
                for (int i = k; i <= m; i++)
                {
                    results.Swap(k, i);
                    foreach (T[] item in Permutations(results, k + 1, m))
                    {
                        yield return item;
                    }
                    results.Swap(k, i);
                }

            }
        }

        public static byte[] RangeByte(byte count = 0)
        {
            byte[] result = new byte[count];
            for (byte i = 0; i < count; i++)
            {
                result[i] = i;
            }
            return result;
        }

        public static int[] RangeInt(int count = 0)
        {
            int[] result = new int[count];
            for (byte i = 0; i < count; i++)
            {
                result[i] = i;
            }
            return result;
        }

        public static float[] RangeFloat(byte count = 0)
        {
            float[] result = new float[count];
            for (byte i = 0; i < count; i++)
            {
                result[i] = i;
            }
            return result;
        }
        
        public static void CopyTo<T>(this T[] source, T[] destination)
        {
        	source.CopyTo(destination, 0);
        }
    }
}
