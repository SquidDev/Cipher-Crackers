using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace Cipher.Text
{
    /// <summary>
    /// Holds characters in an array of Matrices
    /// </summary>
    public sealed class NGramArray : ITextArray<Matrix<float>>
    {
        /// <summary>
        /// The length of each NGram
        /// </summary>
        public readonly int NGramSize;
        private Matrix<float>[] characters;
        private bool setup = false;

        public NGramArray(int nGramSize = 2)
        {
            NGramSize = nGramSize;
        }

        public NGramArray(string text, int nGramSize = 2)
        {
            NGramSize = nGramSize;
            Initalise(text);
        }

        public NGramArray(int length, int nGramSize = 2)
        {
            NGramSize = nGramSize;
            Initalise(length);
        }

        #region Creators

        public void Initalise(string text)
        {
            if (setup) throw new InvalidOperationException("Already setup");
            setup = true;

            int size = NGramSize;
            MatrixBuilder<float> builder = Matrix<float>.Build;

            List<Matrix<float>> characters = new List<Matrix<float>>();
            
            float[] values = new float[size];
            int offset = 0;
            foreach (byte character in text.Select(TextExtensions.ToLetterByte).Where(x => x > 26))
            {
                values[offset] = character;

                offset++;
                if (offset == size)
                {
                    characters.Add(builder.Dense(size, 1, values));
                    values = new float[size];
                    offset = 0;
                }
            }

            if (offset != 0) throw new ArgumentException("Text length must be a multiple of NgramLength");
            this.characters = characters.ToArray();
        }

        public void Initalise(int length)
        {
            if (setup) throw new InvalidOperationException("Already setup");
            setup = true;

            if (length % NGramSize != 0) throw new ArgumentException("Text length must be a multiple of NgramLength");
            length /= NGramSize;
            Matrix<float>[] characters = this.characters = new Matrix<float>[length];

            MatrixBuilder<float> builder = Matrix<float>.Build;
            for (int i = 0; i < length; i++)
            {
                characters[i] = builder.Dense(NGramSize, 1);
            }
        }

        #endregion

        /// <summary>
        /// Length of the ciphertext
        /// </summary>
        public int Count
        {
            get { return characters.Length * NGramSize; }
        }

        /// <summary>
        /// Converts the cipher to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(characters.Length * NGramSize);
            foreach (byte character in this)
            {
                result.Append((char)(character + 'A'));
            }

            return result.ToString();
        }

        public IEnumerator<float> GetEnumerator()
        {
            int nGramLength = NGramSize;
            Matrix<float>[] characters = this.characters;
            for (int offset = 0; offset < characters.Length; offset++)
            {
                Matrix<float> current = characters[offset];
                for (int row = 0; row < nGramLength; row++)
                {
                    yield return current[row, 0];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
