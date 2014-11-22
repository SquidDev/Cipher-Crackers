using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Text
{
    /// <summary>
    /// Holds characters in an array of Matrices
    /// </summary>
    public class NGramArray : TextArray<Matrix<float>>, IEnumerable<float>
    {
        /// <summary>
        /// The length of each NGram
        /// </summary>
        public int NGramLength = 2;

        public NGramArray() : this(2) { }

        public NGramArray(int ngramLength = 2) : base()
        {
            NGramLength = ngramLength;
        }
        public NGramArray(string Text, int ngramLength = 2)
        {
            NGramLength = ngramLength;
            Initalise(Text);
        }
        public NGramArray(int length, int ngramLength = 2)
        {
            NGramLength = ngramLength;
            Initalise(length);
        }

        #region Creators
        public override void Initalise(string Text)
        {
            MatrixBuilder<float> builder = Matrix<float>.Build;
            List<Matrix<float>> characters = new List<Matrix<float>>();

            int ngramLength = NGramLength;
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

        public override void Initalise(int length)
        {
            if (length % NGramLength != 0) throw new ArgumentException("Text length must be a multiple of NgramLength");
            length /= NGramLength;
            Characters = new Matrix<float>[length];

            MatrixBuilder<float> builder = Matrix<float>.Build;
            for (int i = 0; i < length; i++)
            {
                Characters[i] = builder.Dense(1, NGramLength);
            }
        }
        #endregion

        public override double ScoreText()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Length of the ciphertext
        /// </summary>
        public new int Length
        {
            get { return Characters.Length * NGramLength; }
        }

        /// <summary>
        /// Converts the cipher to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(Characters.Length * NGramLength);
            foreach (Matrix<float> ngram in Characters)
            {
                foreach(float character in ngram.Enumerate())
                {
                    result.Append((char)(character + 'A'));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets a portion of the string.
        /// </summary>
        /// <remarks>This is not an efficient implementation, it simply exists for completion</remarks>
        public override string Substring(int start, int length)
        {
            return ToString().Substring(start, length);
        }

        public IEnumerator<float> GetEnumerator()
        {
            int nGramLength = NGramLength;
            Matrix<float>[] characters = Characters;
            for(int offset = 0; offset < characters.Length; offset++)
            {
                Matrix<float> current = characters[offset];
                for(int column = 0; column < nGramLength; column++)
                {
                    yield return current[0, column];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
