using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Utils
{
    public static class ListUtilities
    {
        public static void Swap<T>(this IList<T> Items, int A, int B)
        {
            if (A == B) return;

            T Temp = Items[A];
            Items[A] = Items[B];
            Items[B] = Temp;
        }

        /// <summary>
        /// Permutations of a List
        /// </summary>
        /// <param name="Items">List to use</param>
        /// <returns>Enumerable of all permutations</returns>
        public static IEnumerable<T[]> Permutations<T>(this IList<T> Items)
        {
            return Items.Permutations(Items.Count);
        }

        /// <summary>
        /// Permutations of a List
        /// </summary>
        /// <param name="Items">List to use</param>
        /// <param name="R">Number to choose from</param>
        /// <returns>Enumerable of all permutations</returns>
        public static IEnumerable<T[]> Permutations<T>(this IList<T> Items, int R)
        {
            int N = Items.Count;
            T[] Result = new T[N];
            for (int I = 0; I < N; I++)
            {
                Result[I] = Items[I];
            }

            return Permutations(Result, 0, N - 1);
        }

        internal static IEnumerable<T[]> Permutations<T>(T[] Results, int K, int M)
        {
            if (K == M)
            {
                yield return Results;
            }
            else
            {
                for (int I = K; I <= M; I++)
                {
                    Results.Swap(K, I);
                    foreach (T[] Item in Permutations(Results, K + 1, M))
                    {
                        yield return Item;
                    }
                    Results.Swap(K, I);
                }

            }
        }

        public static byte[] Range(byte Count = 0)
        {
            byte[] Result = new byte[Count];
            for (byte I = 0; I < Count; I++)
            {
                Result[I] = I;
            }
            return Result;
        }
    }
}
