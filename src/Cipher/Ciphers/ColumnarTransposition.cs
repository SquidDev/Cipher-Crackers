using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Cipher.Ciphers
{
    public class ColumnarTransposition<TText, TTextType> : BaseCipher<byte[], TText>
        where TText : ITextArray<TTextType>
    {
        public const byte MaxLength = 10;
        public const byte MinLength = 2;

        public readonly byte MaxKeyLength;
        public readonly byte MinKeyLength;

        public ColumnarTransposition(TextScorer scorer, byte maxLength = MaxLength, byte minLength = MinLength)
            : base(scorer, K => K.PrettyString())
        {
            MaxKeyLength = maxLength;
            MinKeyLength = minLength;
        }

        public override TText Decode(TText cipher, byte[] key, TText decoded)
        {
            int length = cipher.Count;
            int keyLength = key.Length;
            for (int index = 0; index < length; index++)
            {
                int mod = index % keyLength;
                int offset = key[mod];
                int row = index - mod;

                decoded[index] = Text[offset + row];
            }

            return decoded;
        }

        public override ICipherResult<byte[], TText> Crack(TText cipher)
        {
            int length = cipher.Count;

            List<byte> keys = new List<byte>();
            for (byte key = MaxKeyLength; key >= MinKeyLength; key--)
            {
                // At the moment the key must be a factor of the string length;
                if (length % key != 0) continue;
                // Don't bother decoding for factors of this key
                if (keys.Exists(old => old % key == 0)) continue;

                keys.Add(key);
            }

            return keys.RunAsync(Crack).Max((x, y) => x.Score.CompareTo(y.Score));
        }

        public ICipherResult<byte[], TText> Crack(TText cipher, byte keyLength)
        {
            TText decoded = Create(cipher.Count);
            byte[] bestKey = new byte[keyLength];
            double bestScore = Double.NegativeInfinity;

            foreach (byte[] key in ListUtilities.RangeByte(keyLength).Permutations())
            {
                decoded = Decode(key, decoded);
                double score = scorer(decoded);

                if (score > bestScore)
                {
                    key.CopyTo(bestKey, 0);
                    bestScore = score;
                }
            }

            return GetResult(bestScore, bestKey, decoded);
        }
    }
}
