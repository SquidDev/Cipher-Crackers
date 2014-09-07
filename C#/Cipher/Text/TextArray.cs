using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Text
{
    public abstract class TextArray<T>
    {
        public T[] Characters;

        // Basic constructors
        public TextArray() { }
        public TextArray(string Text) { Initalise(Text); }
        public TextArray(int Length) { Initalise(Length); }
        public TextArray(T[] Characters) { Initalise(Characters); }

        // Hacky functions required for creating generics
        public abstract void Initalise(string Text);
        public void Initalise(int Length)
        {
            Characters = new T[Length];
        }
        public void Initalise(T[] Characters)
        {
            this.Characters = Characters;
        }

        /// <summary>
        /// Scores this ciphertext
        /// </summary>
        /// <returns>The score of the text</returns>
        public abstract double ScoreText();

        /// <summary>
        /// Length of the ciphertext
        /// </summary>
        public int Length
        {
            get { return Characters.Length; }
        }

        public T this[int Index]
        {
            get { return Characters[Index]; }
            set { Characters[Index] = value; }
        }

        public void CopyTo(TextArray<T> Destination)
        {
            Characters.CopyTo(Destination.Characters, 0);
        }

        public void Swap(int A, int B)
        {
            T Temp = Characters[A];
            Characters[A] = Characters[B];
            Characters[B] = Temp;
        }
    }
}
