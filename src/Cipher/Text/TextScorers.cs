using Cipher.Frequency;
using Cipher.Utils;
using System.Collections.Generic;
using System;

namespace Cipher.Text
{
    public static class TextScorers
    {
        public static double Score(this IEnumerable<byte> text, double[] scores, int length)
        {
            double score = 0;
            double[] quadgrams = LetterStatistics.Quadgrams;

            // Have to build up 'n' previous
            IEnumerator<byte> enumerator = text.GetEnumerator();
            int[] previous = new int[length - 1];

            for (int i = 0; i < length - 1; i++)
            {
                enumerator.MoveNext();
                previous[i] = (int)(enumerator.Current * Math.Pow(26, length - 1 - i));
            }

            while (enumerator.MoveNext())
            {
                int thisCharacter = (int)enumerator.Current;

                int sum = thisCharacter;
                for (int i = 0; i < length - 2; i++)
                {
                    sum += previous[i];
                    previous[i] = 26 * previous[i + 1];
                }

                score += quadgrams[sum];
                previous[length - 2] = thisCharacter * 26;
            }

            return score;
        }

        public static double ScoreQuadgrams(this IEnumerable<byte> text)
        {
            return Score(text, LetterStatistics.Quadgrams, 4);
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

        public static double ScoreBigrams(this ITextArray text)
        {
            return Score(text, LetterStatistics.Bigrams, 2);
        }
    }
}
