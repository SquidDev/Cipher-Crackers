using Cipher.Frequency;
using System;
using System.Collections.Generic;

namespace Cipher.Utils
{
    public static class MathsUtilities
    {
        public static Random RandomInstance = new Random();

        /// <summary>
        /// Fisher Yates shuffel
        /// </summary>
        public static void Shuffle<T>(this IList<T> Collection)
        {
            Random ThisRandom = RandomInstance;
            for (int Index = Collection.Count; Index > 1; Index--)
            {
                // Pick random element to swap.
                int SwapIndex = ThisRandom.Next(Index);
                // Swap.
                T tmp = Collection[SwapIndex];
                Collection[SwapIndex] = Collection[Index - 1];
                Collection[Index - 1] = tmp;
            }
        }

        public static double Chai(int[] Frequency, int TotalCount, int Letter)
        {
            return Chai(Frequency[Letter], TotalCount, LetterStatistics.Monograms[Letter]);
        }

        public static double Chai(int Value, int TotalCount, double Probability)
        {
            return Chai(Value, TotalCount * Probability);
        }

        public static double Chai(int Value, double Expected)
        {
            return Math.Pow((Value - Expected), 2) / Expected;
        }
    }
}
