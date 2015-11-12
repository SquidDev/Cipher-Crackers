using Cipher.Frequency;
using System;
using System.Text;

namespace Cipher.Text
{
    /// <summary>
    /// Text array for characters instead of bytes
    /// </summary>
    public class CharacterTextArray : ITextArray<char>
    {
        private byte[] characters;
        private bool setup = false;

        public CharacterTextArray()
        {
        }

        public CharacterTextArray(string text)
        {
            Initalise(text);
        }

        public CharacterTextArray(int length)
        {
            Initalise(length);
        }

        public CharacterTextArray(char[] characters)
        {
            Initalise(characters);
        }

        public void Initalise(int length)
        {
            Initalise(new byte[length]);
        }

        public virtual void Initalise(string text)
        {
            Initalise(text.ToCharArray());
        }

        protected void Initalise(char[] characters)
        {
            if (setup) throw new InvalidOperationException("Already setup");

            this.characters = characters;
            setup = true;
        }

        public override string ToString()
        {
            StringBuilder Result = new StringBuilder(characters.Length);
            foreach (byte Character in characters)
            {
                Result.Append((char)(Character + 'A'));
            }

            return Result.ToString();
        }

        public int Count { get { return characters.Length; } }

        public byte this [int index]
        {
            get { return characters[index]; }
            set { characters[index] = value; }
        }
    }
}
