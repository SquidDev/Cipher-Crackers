using Cipher.Frequency;
using Cipher.Utils;
using System.Collections.Generic;
using System;

namespace Cipher.Text
{
    public static class TextScorers
    {
    	private const int MaxPower = 10;
    	private static readonly int[] powers;
    	
    	static TextScorers()
    	{
    		powers = new int[MaxPower];
    		for(int i = 0; i < MaxPower; i++)
    		{
    			powers[i] = (int)Math.Pow(26, i);
    		}
    	}

        public static double Score(this IEnumerable<byte> text, double[] scores, int length)
        {
            double score = 0;

            IEnumerator<byte> enumerator = text.GetEnumerator();
            
            // Have to build up 'n' previous
            int[] previous = new int[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                enumerator.MoveNext();
                previous[i] = (int)enumerator.Current * powers[length - 1 - i];
            }

            while (enumerator.MoveNext())
            {
            	// The previous array is composed of [i * 26 * 26, (i + 1) * 26, i + 2] 
                int thisCharacter = (int)enumerator.Current;

                int sum = thisCharacter;
                for (int i = 0; i < length - 2; i++)
                {
                    sum += previous[i];
                    previous[i] = previous[i + 1] * 26;                    
                }
                
                sum += previous[length - 2];

                score += scores[sum];
                previous[length - 2] = thisCharacter * 26;
            }

            return score;
        }

        public static double ScoreQuadgrams(this IEnumerable<byte> text)
        {
            return Score(text, LetterStatistics.Quadgrams, 4);
        }
        
        public static double ScoreBigrams(this ITextArray text)
        {
            return Score(text, LetterStatistics.Bigrams, 2);
        }

        public static double ScoreMonograms(this ITextArray text)
        {
            double score = 0;

            int length = text.Count;
            int[] frequency = Frequencies(text);

            for (byte letter = 0; letter < 26; letter++)
            {
                score += MathsUtilities.Chi(frequency, length, letter);
            }

            // 'Invert' score so largest is better
            return 1 / score;
        }

        public static int[] Frequencies(this ITextArray text)
        {
            int[] frequency = new int[26];
            foreach (byte character in text)
            {
                frequency[character]++;
            }

            return frequency;
        }
    }
}
