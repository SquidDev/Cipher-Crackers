using Cipher.Frequency;
using Cipher.Utils;

namespace Cipher.Text
{
    public class QuadgramScoredLetterArray : LetterArray
    {
        public QuadgramScoredLetterArray() : base() { }
        public QuadgramScoredLetterArray(string Text) : base(Text) { }
        public QuadgramScoredLetterArray(int Length) : base(Length) { }
        public QuadgramScoredLetterArray(byte[] Characters) : base(Characters) { }

        /// <summary>
        /// Scores this ciphertext using quadgrams
        /// </summary>
        /// <returns>The score of the text</returns>
        public override double ScoreText()
        {
            double Score = 0;

            int Length = Characters.Length - 3;
            for (int I = 0; I < Length; I++)
            {
                Score += LetterStatistics.Quadgrams[
                    (17576 * Characters[I]) + (676 * Characters[I + 1]) +
                    (26 * Characters[I + 2]) + Characters[I + 3]
                ];
            }

            return Score;
        }
    }

    public class MonogramScoredLetterArray : LetterArray
    {
        public MonogramScoredLetterArray() : base() { }
        public MonogramScoredLetterArray(string Text) : base(Text) { }
        public MonogramScoredLetterArray(int Length) : base(Length) { }
        public MonogramScoredLetterArray(byte[] Characters) : base(Characters) { }

        /// <summary>
        /// Calculate the Chi-Squared statistic (single letter)
        /// </summary>
        /// <returns></returns>
        public override double ScoreText()
        {
            double Score = 0;

            int Length = this.Length;
            int[] Frequency = Frequencies();

            for (byte Letter = 0; Letter < 26; Letter++)
            {
                Score += MathsUtilities.Chai(Frequency, Length, Letter);
            }

            // 'Invert' score so largest is better
            return 1 / Score;
        }

        /// <summary>
        /// Calculate letter frequencies
        /// </summary>
        public int[] Frequencies()
        {
            int[] Frequency = new int[26];
            foreach (byte Character in Characters)
            {
                Frequency[Character]++;
            }

            return Frequency;
        }
    }
}
