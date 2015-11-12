using System;
using Cipher.Text;
using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Implements the Hill Cipher
    /// </summary>
    public class Hill : BaseCipher<Matrix<float>, NGramArray>
    {
        public readonly int NGramSize;

        public Hill(TextScorer scorer, int nGramSize = 2)
            : base(scorer)
        {
            NGramSize = nGramSize;
        }

        public override ICipherResult<Matrix<float>, NGramArray> Crack(NGramArray cipher)
        {
            throw new NotImplementedException();
        }

        protected override NGramArray Create(string text)
        {
            return new NGramArray(text, NGramSize);
        }

        protected override NGramArray Create(int length)
        {
            return new NGramArray(length, NGramSize);
        }

        public override NGramArray Decode(NGramArray cipher, Matrix<float> key, NGramArray decoded)
        {
            int length = cipher.Count / NGramSize;

            for (int pos = 0; pos < length; pos++)
            {
                Matrix<float> thisDecoded = decoded[pos];
                key.Multiply(cipher[pos], thisDecoded);
                thisDecoded.Modulus(26, thisDecoded);
            }

            return decoded;
        }
    }
}
