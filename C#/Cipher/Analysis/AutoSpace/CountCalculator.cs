using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Analysis.AutoSpace
{
    public static partial class AutoSpaceData
    {
    	public const double TOKEN_COUNT = 1024908267229;
		public const byte UNSEEN_COUNT = 50;

		public static double[] Unseen;

    	static AutoSpaceData()
    	{
			foreach (string Key in WordOne.Keys.ToArray())
            {
                WordOne[Key] = Math.Log10(WordOne[Key] / TOKEN_COUNT);
            }

            foreach (string Key in WordTwo.Keys.ToArray())
            {
                string[] Words = Key.Split(' ');
                if (Words.Length < 2) continue;

                double Word1Result;
                if (WordOne.TryGetValue(Words[0], out Word1Result))
                {
                    WordTwo[Key] = Math.Log10(WordTwo[Key] / TOKEN_COUNT) - Word1Result;
                }
                else
                {
                    WordTwo[Key] = Math.Log10(WordTwo[Key] / TOKEN_COUNT);
                }
            }

            Unseen = new double[UNSEEN_COUNT];
            for(int L = 0; L < UNSEEN_COUNT; L++)
            {
                Unseen[L] = Math.Log10(10 / (TOKEN_COUNT * Math.Pow(10, L)));
            }
    	}
    }
}
