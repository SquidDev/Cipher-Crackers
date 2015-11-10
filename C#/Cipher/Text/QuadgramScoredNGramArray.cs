using System;
using System.Collections.Generic;
using Cipher.Frequency;

namespace Cipher.Text
{
    /// <summary>
    /// NGramArray which scores words with Quadgrams
    /// </summary>
    public class QuadgramScoredNGramArray : NGramArray
    {
        public QuadgramScoredNGramArray()
            : base(2)
        {
        }
        public QuadgramScoredNGramArray(int ngramLength = 2)
            : base(ngramLength)
        {
        }
        public QuadgramScoredNGramArray(string Text, int ngramLength = 2)
            : base(Text, ngramLength)
        {
        }
        public QuadgramScoredNGramArray(int length, int ngramLength = 2)
            : base(length, ngramLength)
        {
        }

        public override double ScoreText()
        {
            if (Length < 4) return 0;

            double score = 0;
            double[] quadgrams = LetterStatistics.Quadgrams;

            // Have to build up 3 previous
            IEnumerator<float> enumerator = GetEnumerator();
            int[] previous = new int[3];
            
            for (int i = 0; i < 3; i++)
            {
                enumerator.MoveNext();
                previous[i] = (int)(enumerator.Current * Math.Pow(26, 3 - i));
            }

            
            while (enumerator.MoveNext())
            {
                int thisCharacter = (int)enumerator.Current;
                score += quadgrams[
                    previous[0] + previous[1] + previous[2] + thisCharacter
                ];

                previous[0] = previous[1] * 26;
                previous[1] = previous[2] * 26;
                previous[2] = thisCharacter * 26;
            }

            return score;
        }
    }
}
