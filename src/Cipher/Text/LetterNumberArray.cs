using System;
using System.Text;

namespace Cipher.Text
{
    public class LetterNumberArray : LetterTextArray
    {
        public const byte NUM_SYMBOLS = 26 + 1 + 10;
        // Letters, #, numbers

        public LetterNumberArray()
            : base()
        {
        }

        public LetterNumberArray(string text)
            : base()
        {
        }

        public LetterNumberArray(int length)
            : base(length)
        {
        }

        public LetterNumberArray(byte[] characters)
            : base(characters)
        {
        }

        public override void Initalise(string Text)
        {
            Initalise(Text.ToLetterNumber());
        }

        /// <summary>
        /// Converts the cipher to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Result = new StringBuilder(Count);
            foreach (byte Character in this)
            {
                if (Character < 26)
                {
                    Result.Append((char)(Character + 'A'));
                }
                else if (Character == 26)
                {
                    Result.Append(26);
                }
                else
                {
                    Result.Append((char)(Character + '0' - 27));
                }
            }

            return Result.ToString();
        }
    }
}
