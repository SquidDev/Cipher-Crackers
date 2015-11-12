using Cipher.Text;
using System;

namespace Cipher.Ciphers
{
    public class RailFence<TText, TTextType> : DefaultCipher<int, TText>
    	where TText : ITextArray<TTextType>, new()
    {
        public RailFence(TextScorer scorer)
            : base(scorer)
        {
        }

        public override TText Decode(TText cipher, int key, TText decoded)
        {
            int length = cipher.Count;
            key--;

            int k = 0;
            int diff = 2 * key;
            for (int line = 0; line < key; line++)
            {
                int skip = 2 * (key - line);
                int j = 0;
                for (int i = line; i < length;)
                {
                    decoded[i] = cipher[k++];
                    if (line == 0 || j % 2 == 0)
                    {
                        i += skip;
                    }
                    else
                    {
                        i += diff - skip;
                    }
                    j++;
                }
            }
            for (int I = key; I < length; I += diff)
            {
                decoded[I] = cipher[k++];
            }

            return decoded;
        }

        public override ICipherResult<int, TText> Crack(TText cipher)
        {
            double bestScore = Double.NegativeInfinity;
            int bestKey = 0;

            int length = cipher.Count;

            TText decoded = Create(length);

            length++;
            for (int key = 2; key < length; key++)
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
}
