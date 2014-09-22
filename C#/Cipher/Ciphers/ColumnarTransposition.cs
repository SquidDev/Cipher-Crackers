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

		public ColumnarTransposition(string Text) : base(Text)
        {
            KeyStringify = K => K.PrettyString();
        }
        public ColumnarTransposition(TArray Text) : base(Text)
        {
            KeyStringify = K => K.PrettyString();
        }

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
            TArray Decoded = Create(Length);

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

                BaseCipher<byte[], TArray, TArrayType>.CipherResult Result = InternalCrack(KeyLength, Decoded);

                if(Result.Score > BestScore)
                {
                    BestKey = Result.Key;
                    BestScore = Result.Score;
                }
            }

            return GetResult(BestScore, BestKey, Decoded);
		}

        public CipherResult Crack(byte KeyLength)
        {
            return InternalCrack(KeyLength, Create(Text.Length));
        }

        protected CipherResult InternalCrack(byte KeyLength, TArray Decoded)
        {
            byte[] BestKey = new byte[KeyLength];
            double BestScore = Double.NegativeInfinity;

            foreach (byte[] Key in ListUtilities.Range(KeyLength).Permutations())
            {
                Decoded = Decode(Key, Decoded);
                double Score = Decoded.ScoreText();

                if (Score > BestScore)
                {
                    Key.CopyTo(BestKey, 0);
                    BestScore = Score;
                }
            }

            return GetResult(BestScore, BestKey, Decoded);
        }
	}
}
