using System;
using System.Collections.Generic;
using System.Threading;

using Cipher.Frequency;

namespace Cipher.Utils
{
    public static class MathsUtilities
    {
        private static int seed = Environment.TickCount;
    	private static readonly ThreadLocal<Random> RandomFactory = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

    	public static Random RandomInstance
    	{
    		get { return RandomFactory.Value; }
    	}

        /// <summary>
        /// Fisher Yates shuffle
        /// </summary>
        public static void Shuffle<T>(this IList<T> collection)
        {
            Random thisRandom = RandomInstance;
            for (int Index = collection.Count; Index > 1; Index--)
            {
                // Pick random element to swap.
                int SwapIndex = thisRandom.Next(Index);
                // Swap.
                T tmp = collection[SwapIndex];
                collection[SwapIndex] = collection[Index - 1];
                collection[Index - 1] = tmp;
            }
        }

        #region Chi
        /// <summary>
        /// Calculate the Chi Squared statistic of one letter
        /// </summary>
        /// <param name="Frequency">The frequencies of each letters</param>
        /// <param name="TotalCount">The total frequency count</param>
        /// <param name="Letter">The letter index</param>
        public static double Chi(int[] Frequency, int TotalCount, int Letter)
        {
            return Chi(Frequency[Letter], TotalCount, LetterStatistics.Monograms[Letter]);
        }

        /// <summary>
        /// Calculate the chai squared statistic for one value
        /// </summary>
        /// <param name="Value">The count of that value</param>
        /// <param name="TotalCount">The total count of all values</param>
        /// <param name="Probability">The probability of that value occuring</param>
        public static double Chi(int Value, int TotalCount, double Probability)
        {
            return Chi(Value, TotalCount * Probability);
        }

        /// <summary>
        /// Calculate the chi squared statistic for one value
        /// </summary>
        /// <param name="Value">The value received</param>
        /// <param name="Expected">The expected value</param>
        public static double Chi(int Value, double Expected)
        {
            return Math.Pow((Value - Expected), 2) / Expected;
        }
        #endregion
    }
}
