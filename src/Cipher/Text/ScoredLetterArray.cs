using Cipher.Frequency;
using Cipher.Utils;

namespace Cipher.Text
{
    public class QuadgramScoredLetterArray : LetterArray
    {
        public QuadgramScoredLetterArray()
            : base()
        {
        }
        public QuadgramScoredLetterArray(string Text)
            : base(Text)
        {
        }
        public QuadgramScoredLetterArray(int Length)
            : base(Length)
        {
        }
        public QuadgramScoredLetterArray(byte[] Characters)
            : base(Characters)
        {
        }

        /// <summary>
        /// Scores this ciphertext using quadgrams
        /// </summary>
        /// <returns>The score of the text</returns>
        public override double ScoreText()
        {
            double score = 0;

            int length = Characters.Length - 3;
            for (int I = 0; I < length; I++)
            {
                score += LetterStatistics.Quadgrams[
                    (17576 * Characters[I]) + (676 * Characters[I + 1]) +
                    (26 * Characters[I + 2]) + Characters[I + 3]
                ];
            }

            return score;
        }
    }

    public class MonogramScoredLetterArray : LetterArray
    {
        public MonogramScoredLetterArray()
            : base()
        {
        }
        public MonogramScoredLetterArray(string Text)
            : base(Text)
        {
        }
        public MonogramScoredLetterArray(int Length)
            : base(Length)
        {
        }
        public MonogramScoredLetterArray(byte[] Characters)
            : base(Characters)
        {
        }

        /// <summary>
        /// Calculate the Chi-Squared statistic (single letter)
        /// </summary>
        /// <returns></returns>
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
            foreach (byte character in Characters)
            {
                frequency[character]++;
            }

            return frequency;
        }
    }
    
    public class BigramScoredLetterArray : LetterArray
    {
        public BigramScoredLetterArray()
            : base()
        {
        }
        public BigramScoredLetterArray(string Text)
            : base(Text)
        {
        }
        public BigramScoredLetterArray(int Length)
            : base(Length)
        {
        }
        public BigramScoredLetterArray(byte[] Characters)
            : base(Characters)
        {
        }

        /// <summary>
        /// Scores this ciphertext using quadgrams
        /// </summary>
        /// <returns>The score of the text</returns>
        public override double ScoreText()
        {
            double score = 0;

            int length = Characters.Length - 1;
            for (int i = 0; i < length; i++)
            {
                score += LetterStatistics.Bigrams[(26 * Characters[i + 1]) + Characters[i]];
            }

            return score;
        }
    }
}
