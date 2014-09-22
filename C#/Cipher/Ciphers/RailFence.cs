using Cipher.Text;
using System;

namespace Cipher.Ciphers
{
    public class RailFence<TArray, TArrayType> : BaseCipher<int, TArray, TArrayType>
        where TArray : TextArray<TArrayType>, new()
    {
        public RailFence(string CipherText) : base(CipherText) { }
        public RailFence(TArray CipherText) : base(CipherText) { }

        public override TArray Decode(int Key, TArray Decoded)
        {
            int Length = Text.Length;
            Key--;

            int K = 0;
            int Diff = 2 * Key;
            for(int Line = 0; Line < Key; Line++)
            {
                int Skip = 2 * (Key - Line);
                int J = 0;
                for (int I = Line; I < Length; )
                {
                    Decoded[I] = Text[K++];
                    if (Line == 0 || J % 2 == 0)
                    {
                        I += Skip;
                    }
                    else
                    {
                        I += Diff - Skip;
                    }
                    J++;
                }
            }
            for (int I = Key; I < Length; I+=Diff)
            {
                Decoded[I] = Text[K++];
            }

            return Decoded;
        }

        public override CipherResult Crack()
        {
            double BestScore = Double.NegativeInfinity;
            int BestKey = 0;

            int Length = Text.Length;

            TArray Decoded = new TArray();
            Decoded.Initalise(Length);

            Length++;
            for (int Key = 2; Key < Length; Key++)
            {
                Decoded = Decode(Key, Decoded);
                double Score = Decoded.ScoreText();

                if (Score > BestScore)
                {
                    BestScore = Score;
                    BestKey = Key;
                }
            }

            return GetResult(BestScore, BestKey, Decoded);
        }
    }
}
