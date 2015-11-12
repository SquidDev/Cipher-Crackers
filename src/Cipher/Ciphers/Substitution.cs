using Cipher.Text;
using Cipher.Utils;
using System;
using System.Linq;

namespace Cipher.Ciphers
{
    public class Substitution<TText> : DefaultCipher<byte[], TText>
    	where TText : ITextArray<byte>, new()
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
                decoded[index] = key[cipher[index]];
            }

            return decoded;
        }

        public override ICipherResult<byte[], TText> Crack(TText cipher)
        {
            byte[] bestKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLetterArray();
            double bestScore = Double.NegativeInfinity;

            byte[] parentKey = bestKey.ToArray();
            double parentScore = bestScore;

            TText decoded = Create(cipher.Count);
            byte[] childKey = new byte[bestKey.Length];
            double childScore;

            for (int iteration = 0; iteration < MaxIterations; iteration++)
            {
                parentKey.Shuffle<byte>();
                decoded = Decode(cipher, parentKey, decoded);
                parentScore = scorer(decoded);

                parentKey.CopyTo(childKey);

                int count = 0;
                while (count < InternalIterations)
                {
                    //Swap characters
                    childKey.Swap(MathsUtilities.RandomInstance.Next(26), MathsUtilities.RandomInstance.Next(26));

                    decoded = Decode(cipher, childKey, decoded);
                    childScore = scorer(decoded);

                    //Reset parent score
                    if (childScore > parentScore)
                    {
                        parentScore = childScore;
                        count = 0;

                        childKey.CopyTo(parentKey); //Backup this key
                    }
                    else
                    {
                        parentKey.CopyTo(childKey); // Reset ChildKey
                    }

                    count++;
                }

                if (parentScore > bestScore)
                {
                    bestScore = parentScore;
                    parentKey.CopyTo(bestKey);
                }
            }

            return GetResult(cipher, bestScore, bestKey, decoded);
        }
    }
}
