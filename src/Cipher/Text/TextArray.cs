namespace Cipher.Text
{
    public abstract class TextArray<T>
    {
        public T[] Characters;

        // Basic constructors
        public TextArray()
        {
        }
        public TextArray(string Text)
        {
            Initalise(Text);
        }
        public TextArray(int Length)
        {
            Initalise(Length);
        }
        public TextArray(T[] Characters)
        {
            Initalise(Characters);
        }

        // Hacky functions required for creating generics
        public abstract void Initalise(string Text);
        public virtual void Initalise(int Length)
        {
            Characters = new T[Length];
        }
        public virtual void Initalise(T[] Characters)
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

        public virtual void CopyTo(TextArray<T> Destination)
        {
            Characters.CopyTo(Destination.Characters, 0);
        }

        public virtual void Swap(int A, int B)
        {
            T Temp = Characters[A];
            Characters[A] = Characters[B];
            Characters[B] = Temp;
        }

        public virtual string Substring(int Start)
        {
            return Substring(Start, Length - Start);
        }
        public abstract string Substring(int Start, int Length);
    }
}
