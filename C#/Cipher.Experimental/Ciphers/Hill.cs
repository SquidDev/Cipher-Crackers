using System;
using Cipher.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    public class Hill : BaseCipher<Matrix<float>, NgramArray, Matrix<float>>
    {
        public Hill(string CipherText, int ngramLength = 2)
            : this(new NgramArray(CipherText, ngramLength))
        { }
        public Hill(NgramArray CipherText) : base(CipherText) { }

        public override CipherResult Crack()
        {
            throw new NotImplementedException();
        }

        public override NgramArray Decode(Matrix<float> Key)
        {
            NgramArray Decoded = new NgramArray(Text.Length, Text.NgramLength);
            return Decode(Key, Decoded);
        }

        public override NgramArray Decode(Matrix<float> Key, NgramArray Decoded)
        {
            // Calculate the inverse of the matrix, 
            Matrix<float> Inverse = Key.Inverse();
            Inverse.Modulus(26, Inverse);
            Inverse.Map(f => (float)Math.Floor(f), Inverse);

            Matrix<float>[] text = Text.Characters;
            Matrix<float>[] decodedText = Decoded.Characters;
            int length = text.Length;

            for (int pos = 0; pos < length; pos++)
            {
                Matrix<float> thisDecoded = decodedText[pos];
                text[pos].Multiply(Key, thisDecoded);
                thisDecoded.Modulus(26, thisDecoded);
            }

            return Decoded;
        }
    }
}
