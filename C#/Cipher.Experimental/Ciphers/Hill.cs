using System;
using Cipher.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Implements the Hill Cipher
    /// </summary>
    public class Hill<TArray> : BaseCipher<Matrix<float>, TArray, Matrix<float>>
        where TArray : NGramArray, new()
    {
        public Hill(string cipherText, int ngramLength = 2)
        {
            Text = new TArray();
            Text.NGramLength = ngramLength;
            Text.Initalise(cipherText);
        }
        public Hill(TArray cipherText) : base(cipherText) { }

        public override CipherResult Crack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Decode the ciphertext
        /// </summary>
        /// <param name="key">The Key to use</param>
        /// <returns>The decoded text</returns>
        public override TArray Decode(Matrix<float> key)
        {
            TArray decoded = new TArray();
            decoded.NGramLength = Text.NGramLength;
            decoded.Initalise(Text.Length);
            return Decode(key, decoded);
        }

        /// <summary>
        /// Decode the ciphertext
        /// </summary>
        /// <param name="key">The key to use</param>
        /// <param name="decoded">The variable to </param>
        /// <returns></returns>
        public override TArray Decode(Matrix<float> key, TArray decoded)
        {
            // Calculate the inverse of the matrix, 
            Matrix<float> inverse = key.Inverse();
            inverse.Modulus(26, inverse);
            inverse.Map(f => (float)Math.Floor(f), inverse);

            Matrix<float>[] text = Text.Characters;
            Matrix<float>[] decodedText = decoded.Characters;
            int length = text.Length;

            for (int pos = 0; pos < length; pos++)
            {
                Matrix<float> thisDecoded = decodedText[pos];
                text[pos].Multiply(inverse, thisDecoded);
                thisDecoded.Modulus(26, thisDecoded);
            }

            return decoded;
        }
    }
}
