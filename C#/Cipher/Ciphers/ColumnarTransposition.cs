using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;

namespace Cipher.Ciphers
{
    public class ColumnarTransposition<TArray, TArrayType> : BaseCipher<byte[], TArray, TArrayType>
        where TArray : TextArray<TArrayType>, new()
	{
		public byte MaxKeyLength = 10;
        public byte MinKeyLength = 2;

		public ColumnarTransposition(string Text) : base(Text) { }
        public ColumnarTransposition(TArray Text) : base(Text) { }

        public override TArray Decode(byte[] Key, TArray Decoded)
		{
			int Length = Text.Length;
            int KeyLength = Key.Length;
			for (int Index = 0; Index < Length; Index++)
			{
                int Mod = Index % KeyLength;
                int Offset = Key[Mod];
                int Row = Index - Mod;

                Decoded[Index] = Text[Offset + Row];
			}

			return Decoded;
		}

		public override CipherResult Crack()
		{
            byte[] BestKey = null;
			double BestScore = Double.NegativeInfinity;

            int Length = Text.Length;
            TArray Decoded = new TArray();
            Decoded.Initalise(Length);

            List<byte> TriedKeys = new List<byte>();
            for (byte KeyLength = MaxKeyLength; KeyLength >= MinKeyLength; KeyLength--)
            {
                // At the moment the key must be a factor of the string length;
                if (Length % KeyLength != 0) continue;
                // Don't bother decoding for factors of this key
                foreach(byte OldKey in TriedKeys)
                {
                    if (OldKey % KeyLength == 0) continue;
                }

                TriedKeys.Add(KeyLength);
                foreach(byte[] Key in ListUtilities.Range(KeyLength).Permutations())
                {
                    Decoded = Decode(Key, Decoded);
                    double Score = Decoded.ScoreText();

                    if(Score > BestScore)
                    {
                        BestKey = (byte[])Key.Clone();
                        BestScore = Score;
                    }
                }
            }

            return GetResult(BestScore, BestKey, Decoded);
		}
	}
}
