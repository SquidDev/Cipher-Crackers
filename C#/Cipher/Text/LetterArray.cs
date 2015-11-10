using System;
using System.Text;

namespace Cipher.Text
{
    public class LetterArray : TextArray<byte>
    {
        public LetterArray()
            : base()
        {
        }
        public LetterArray(string Text)
            : this(Text.ToLetterArray())
        {
        }
        public LetterArray(int Length)
            : base(Length)
        {
        }
        public LetterArray(byte[] Characters)
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
                Result.Append((char)(Character + 'A'));
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
            int End = Start + Length;
            StringBuilder Out = new StringBuilder();
            for (int Pos = Start; Pos < End; Pos++)
            {
                Out.Append((char)(this[Pos] + 'A'));
            }

            return Out.ToString();
        }
    }
}
