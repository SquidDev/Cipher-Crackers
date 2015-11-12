using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cipher.Frequency;

namespace Cipher.Text
{
    /// <summary>
    /// Text array for characters instead of bytes
    /// </summary>
    public class CharacterTextArray : ITextArray<char>
    {
        private char[] characters;
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
            Initalise(new char[length]);
        }

        public virtual void Initalise(string text)
        {
            Initalise(text.ToCharArray());
        }
        
        protected void Initalise(char[] text)
        {
        	if (setup) throw new InvalidOperationException("Already setup");
			setup = true;
        	
			characters = text;            
        }

        public void Initalise(IReadOnlyList<char> text)
		{
        	Initalise(text.ToArray());
		}

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(characters.Length);
            foreach (char character in characters)
            {
                result.Append(character);
            }

            return result.ToString();
        }

        public int Count { get { return characters.Length; } }

        byte IReadOnlyList<byte>.this[int index]
        {
        	get { return characters[index].ToLetterByte(); }
        }
    	
        public char this[int index] {
        	get { return characters[index]; }
        	set { characters[index] = value; }
		}
    	
		public IEnumerator<byte> GetEnumerator()
		{
			return characters.ToLetters().GetEnumerator();
		}
    	
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return characters.ToLetters().GetEnumerator();
		}
    }
}
