using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Cipher.Ciphers
{
    public class ColumnarTransposition<TArray, TArrayType> : BaseCipher<byte[], TArray, TArrayType>
        where TArray : TextArray<TArrayType>, new()
    {
        public byte MaxKeyLength = 10;
        public byte MinKeyLength = 2;

        public ColumnarTransposition(string Text)
            : base(Text)
        {
            KeyStringify = K => K.PrettyString();
        }

        public ColumnarTransposition(TArray Text)
            : base(Text)
        {
            KeyStringify = K => K.PrettyString();
        }

        public override TArray Decode(byte[] key, TArray decoded)
        {
            int length = Text.Length;
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

        public override CipherResult Crack()
        {
            int length = Text.Length;

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

        public CipherResult Crack(byte keyLength)
        {
            TArray decoded = Create(Text.Length);
            byte[] bestKey = new byte[keyLength];
            double bestScore = Double.NegativeInfinity;

            foreach (byte[] key in ListUtilities.RangeByte(keyLength).Permutations())
            {
                decoded = Decode(key, decoded);
                double score = decoded.ScoreText();

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
