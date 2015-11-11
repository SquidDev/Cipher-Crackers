using System;
using System.Collections.Generic;
using Cipher.Frequency;
using Cipher.Utils;

namespace Cipher.Text
{
    /// <summary>
    /// NGramArray which scores words with Quadgrams
    /// </summary>
    public class QuadgramScoredNGramArray : NGramArray
    {
        public QuadgramScoredNGramArray()
            : base()
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
    
    /// <summary>
    /// NGramArray which scores words with Quadgrams
    /// </summary>
    public class BigramScoredNGramArray : NGramArray
    {
        public BigramScoredNGramArray()
            : base()
        {
        }
        public BigramScoredNGramArray(int ngramLength = 2)
            : base(ngramLength)
        {
        }
        public BigramScoredNGramArray(string Text, int ngramLength = 2)
            : base(Text, ngramLength)
        {
        }
        public BigramScoredNGramArray(int length, int ngramLength = 2)
            : base(length, ngramLength)
        {
        }

        public override double ScoreText()
        {
            if (Length < 2) return 0;

            double score = 0;
            double[] bigrams = LetterStatistics.Bigrams;

            IEnumerator<float> enumerator = GetEnumerator();
            enumerator.MoveNext();
            int previous = (int)enumerator.Current * 26;

            while (enumerator.MoveNext())
            {
                int thisCharacter = (int)enumerator.Current;
                score += bigrams[thisCharacter + previous];

                previous = thisCharacter * 26;
            }

            return score;
        }
    }
    
    public class MonogramScoredNGramArray : NGramArray
    {
        public MonogramScoredNGramArray()
            : base()
        {
        }
        public MonogramScoredNGramArray(int ngramLength = 2)
            : base(ngramLength)
        {
        }
        public MonogramScoredNGramArray(string Text, int ngramLength = 2)
            : base(Text, ngramLength)
        {
        }
        public MonogramScoredNGramArray(int length, int ngramLength = 2)
            : base(length, ngramLength)
        {
        }

        public override double ScoreText()
        {
            double score = 0;

            int length = Length;
            int[] frequency = Frequencies();

            for (byte letter = 0; letter < 26; letter++)
            {
                score += MathsUtilities.Chi(frequency, length, letter);
            }

            // 'Invert' score so largest is better
            return 1 / score;
        }
        
        /// <summary>
        /// Calculate letter frequencies
        /// </summary>
        public int[] Frequencies()
        {
            int[] frequency = new int[26];
            foreach (float character in this)
            {
            	frequency[(byte)character]++;
            }

            return frequency;
        }
    }
}
