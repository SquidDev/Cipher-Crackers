using Cipher.Text;
using Cipher.Utils;
using System;
using System.Linq;

namespace Cipher.Ciphers
{
    public class Substitution<TText> : BaseCipher<byte[], TText>
        where TText : ITextArray<byte>
    {
        public const int MaxIterations = 5;
        public const int InternalIterations = 1000;

        public Substitution(TextScorer scorer)
            : base(scorer)
        {
        }

        public override TText Decode(TText cipher, byte[] key, TText decoded)
        {
            int length = cipher.Count;
            for (int index = 0; index < length; index++)
            {
                decoded[index] = key[Text[index]];
            }

            return decoded;
        }

        public override ICipherResult<byte[], TText> Crack(TText cipher)
        {
            byte[] BestKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLetterArray();
            double BestScore = Double.NegativeInfinity;

            byte[] ParentKey = BestKey.ToArray();
            double ParentScore = BestScore;

            TText Decoded = Create(cipher.Count);
            byte[] ChildKey = new byte[BestKey.Count];
            double ChildScore;

            for (int Iteration = 0; Iteration < MaxIterations; Iteration++)
            {
                ParentKey.Shuffle<byte>();
                Decoded = Decode(ParentKey, Decoded);
                ParentScore = scorer(Decoded);

                ParentKey.CopyTo(ChildKey);

                int Count = 0;
                while (Count < InternalIterations)
                {
                    //Swap characters
                    ChildKey.Swap(MathsUtilities.RandomInstance.Next(26), MathsUtilities.RandomInstance.Next(26));

                    Decoded = Decode(ChildKey, Decoded);
                    ChildScore = scorer(Decoded);

                    //Reset parent score
                    if (ChildScore > ParentScore)
                    {
                        ParentScore = ChildScore;
                        Count = 0;

                        ChildKey.CopyTo(ParentKey); //Backup this key
                    }
                    else
                    {
                        ParentKey.CopyTo(ChildKey); // Reset ChildKey
                    }

                    Count++;
                }

                if (ParentScore > BestScore)
                {
                    BestScore = ParentScore;
                    ParentKey.CopyTo(BestKey);
                }
            }

            return GetResult(BestScore, BestKey, Decoded);
        }
    }
}
