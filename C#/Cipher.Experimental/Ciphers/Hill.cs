using System;
using Cipher.Text;
using Cipher.Utils;
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

        public int NGramLength
        {
            get { return Text.NGramLength; }
        }

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
            decoded.NGramLength = NGramLength;
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

            /*
            inverse.Map(f =>
            {
                if (Single.IsInfinity(f) || Single.IsNaN(f))
                {
                    throw new ArgumentException("Non invertable matrix");
                }
                return (float)Math.Floor(f);
            }, inverse);*/

            //Console.WriteLine(key.ToMatrixString());
            //Console.WriteLine(inverse.ToMatrixString());

            //Console.WriteLine("Key: {0}", inverse);

            Matrix<float>[] text = Text.Characters;
            Matrix<float>[] decodedText = decoded.Characters;
            int length = text.Length;

            for (int pos = 0; pos < length; pos++)
            {
                Matrix<float> thisDecoded = decodedText[pos];
                key.Multiply(text[pos], thisDecoded);
                thisDecoded.Modulus(26, thisDecoded);
            }

            return decoded;
        }
    }
}
