using Cipher.Frequency;
using Cipher.Text;
using Cipher.Utils;
using System;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Solves the caeser shift
    /// </summary>
    /// <typeparam name="TArray">
    ///     <see cref="TextArray"/> to use to store the characters 
    ///     and score the result
    /// </typeparam>
    public class CaeserShift<TText> : DefaultCipher<byte, TText>
    	where TText : ITextArray<byte>, new()
    {
        public CaeserShift(TextScorer scorer)
            : base(scorer)
        {
        }

        /// <summary>
        /// Decode the CipherText
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public override TText Decode(TText cipher, byte key, TText decoded)
        {
            byte inverseKey = (byte)((26 - key) % 26);
            int length = cipher.Count;
            for (int Index = 0; Index < length; Index++)
            {
                decoded[Index] = (byte)((cipher[Index] + inverseKey) % 26);
            }
            
            return decoded;
        }

        public override ICipherResult<byte, TText> Crack(TText cipher)
        {
            double bestScore = Double.NegativeInfinity;
            byte bestKey = 0;

            TText decoded = new TText();
            decoded.Initalise(cipher.Count);

            for (byte key = 0; key < 26; key++)
            {
                decoded = Decode(cipher, key, decoded);
                double score = scorer(decoded);
                
                if (score > bestScore)
                {
                    bestScore = score;
                    bestKey = key;
                }
            }

            return GetResult(cipher, bestScore, bestKey, decoded);
        }
    }

    /// <summary>
    /// Optimised CaeserShift for monograms
    /// </summary>
    public class MonogramCaeserShift<TText> : CaeserShift<TText>
    	where TText : ITextArray<byte>, new()
    {
        public MonogramCaeserShift()
            : base(TextScorers.ScoreMonograms)
        {
        }

        public override ICipherResult<byte, TText> Crack(TText cipher)
        {
            int[] frequencies = cipher.Frequencies();

            double bestScore = Double.PositiveInfinity;
            byte bestKey = 0;

            int Length = cipher.Count;
            for (byte shift = 0; shift < 26; shift++)
            {
                double score = 0;
                for (byte Letter = 0; Letter < 26; Letter++)
                {
                    score += MathsUtilities.Chi(frequencies[Letter], Length, LetterStatistics.Monograms[(Letter + shift) % 26]);
                }
                
                if (score < bestScore)
                {
                    bestScore = score;
                    bestKey = shift;
                }
            }

            return GetResult(cipher, bestScore, (byte)((26 - bestKey) % 26));
        }
    }
}
