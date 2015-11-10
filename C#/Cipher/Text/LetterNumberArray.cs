using System;
using System.Text;

namespace Cipher.Text
{
    public class LetterNumberArray : LetterArray
    {
        public const byte NUM_SYMBOLS = 26 + 1 + 10;
        // Letters, #, numbers

        public LetterNumberArray()
            : base()
        {
        }
        public LetterNumberArray(string Text)
            : this(Text.ToLetterArray())
        {
        }
        public LetterNumberArray(int Length)
            : base(Length)
        {
        }
        public LetterNumberArray(byte[] Characters)
            : base(Characters)
        {
        }

        public override void Initalise(string Text)
        {
            Initalise(Text.ToLetterArray());
        }

        /// <summary>
        /// Converts the cipher to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Result = new StringBuilder(Characters.Length);
            foreach (byte Character in Characters)
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

        public override double ScoreText()
        {
            // Need to throw as errors
            throw new NotImplementedException();
        }

        public override string Substring(int Start, int Length)
        {
            throw new NotImplementedException();
        }
    }
}
