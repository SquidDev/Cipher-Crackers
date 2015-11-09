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
    public class CaeserShift<TArray> : BaseCipher<byte, TArray, byte>
        where TArray : TextArray<byte>, new()
    {
    	public CaeserShift(string CipherText) : base(CipherText) { }
    	public CaeserShift(TArray CipherText) : base(CipherText) { }
    	
        /// <summary>
        /// Decode the CipherText
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public override TArray Decode(byte Key, TArray Decoded)
        {
            byte InverseKey = (byte)((26 - Key) % 26);
            int Length = Text.Length;
            for (int Index = 0; Index < Length; Index++)
            {
                Decoded[Index] = (byte)((Text[Index] + InverseKey) % 26);
            }
            
            return Decoded;
        }

        public override CipherResult Crack()
        {
            double BestScore = Double.NegativeInfinity;
            byte BestKey = 0;

            TArray Decoded = new TArray();
            Decoded.Initalise(Text.Length);

            for (byte Key = 0; Key < 26; Key++)
            {
                Decoded = Decode(Key, Decoded);
                double Score = Decoded.ScoreText();
                
                if(Score > BestScore){
                    BestScore = Score;
                    BestKey = Key;
                }
            }

            return GetResult(BestScore, BestKey, Decoded);
        }
    }

    /// <summary>
    /// Optimised CaeserShift for monograms
    /// </summary>
    public class MonogramCaeserShift : CaeserShift<MonogramScoredLetterArray>
    {
        public MonogramCaeserShift(string CipherText) : base(CipherText) { }
        public MonogramCaeserShift(MonogramScoredLetterArray CipherText) : base(CipherText) { }

        public override CipherResult Crack()
        {
            int[] Frequencies = Text.Frequencies();

            double BestScore = Double.PositiveInfinity;
            byte BestKey = 0;

            int Length = Text.Length;
            for (byte Shift = 0; Shift < 26; Shift++)
            {
                double Score = 0;
                for (byte Letter = 0; Letter < 26; Letter++)
                {
                    Score += MathsUtilities.Chi(Frequencies[Letter], Length, LetterStatistics.Monograms[(Letter + Shift) % 26]);
                }
                
                if (Score < BestScore)
                {
                    BestScore = Score;
                    BestKey = Shift;
                }
            }

            return GetResult(BestScore, (byte)((26 - BestKey) % 26));
        }
    }
}
