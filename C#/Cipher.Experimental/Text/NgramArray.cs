using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Text
{
    public class NgramArray : TextArray<Matrix<float>>
    {
        public readonly int NgramLength = 2;

        public NgramArray() : this(2) { }
        public NgramArray(int ngramLength = 2) : base()
        {
            NgramLength = ngramLength;
        }
        public NgramArray(string Text, int ngramLength = 2)
        {
            NgramLength = ngramLength;
            Initalise(Text);
        }
        public NgramArray(int length, int ngramLength = 2)
        {
            NgramLength = ngramLength;
            Initalise(length / ngramLength);
        }

        public override void Initalise(string Text)
        {
            MatrixBuilder<float> builder = Matrix<float>.Build;
            List<Matrix<float>> characters = new List<Matrix<float>>();

            int ngramLength = NgramLength;
            float[] values = new float[ngramLength];
            int offset = 0;
            foreach (char character in Text)
            {
                float bChar = character.ToLetterByte();
                if (bChar < 26)
                {
                    values[offset] = bChar;

                    offset++;
                    if(offset == ngramLength)
                    {
                        characters.Add(builder.Dense(1, ngramLength, values));
                        values = new float[ngramLength];
                        offset = 0;
                    }
                }
            }

            if (offset != 0) throw new ArgumentException("Text length must be a multiple of NgramLength");

            Characters = characters.ToArray();
        }

        public override void Initalise(int Length)
        {
            if (Length % NgramLength != 0) throw new ArgumentException("Text length must be a multiple of NgramLength");
            Characters = new Matrix<float>[Length / NgramLength];
        }

        public override double ScoreText()
        {
            throw new NotImplementedException();
        }

        public override string Substring(int Start, int Length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the cipher to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Result = new StringBuilder(Characters.Length * NgramLength);
            foreach (Matrix<float> NGram in Characters)
            {
                foreach(float Character in NGram.Enumerate())
                {
                    Result.Append((char)(Character + 'A'));
                }
            }

            return Result.ToString();
        }
    }
}
