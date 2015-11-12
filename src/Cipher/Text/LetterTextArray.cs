using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Text
{
    public class LetterTextArray : ITextArray<byte>
    {
        private byte[] characters;
        private bool setup = false;

        public LetterTextArray()
        {
        }

        public LetterTextArray(string text)
        {
            Initalise(text);
        }

        public LetterTextArray(int length)
        {
            Initalise(length);
        }

        public LetterTextArray(byte[] characters)
        {
            Initalise(characters);
        }

        public void Initalise(int length)
        {
            Initalise(new byte[length]);
        }

        public virtual void Initalise(string text)
        {
            Initalise(text.ToLetterArray());
        }
        
        public void Initalise(IReadOnlyList<byte> text)
        {
        	Initalise(text.ToArray());
        }

        public void Initalise(byte[] text)
        {
        	if (setup) throw new InvalidOperationException("Already setup");
			setup = true;
			
            characters = text;
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
    	
		public IEnumerator<byte> GetEnumerator()
		{
			return ((IEnumerable<byte>)characters).GetEnumerator();
		}
    	
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return characters.GetEnumerator();
		}
    }
}
