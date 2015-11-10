using Cipher.Frequency;
using System;
using System.Text;

namespace Cipher.Text
{
    /// <summary>
    /// Text array for characters instead of bytes
    /// </summary>
    public class CharacterArray : TextArray<char>
    {
        public CharacterArray()
            : base()
        {
        }
        public CharacterArray(string Text)
            : base(Text)
        {
        }
        public CharacterArray(int Length)
            : base(Length)
        {
        }
        public CharacterArray(char[] Characters)
            : base(Characters)
        {
        }

        public override void Initalise(string Text)
        {
            Characters = Text.ToCharArray();
        }

        public override double ScoreText()
        {
            throw new NotImplementedException();
        }

        public override string Substring(int Start, int Length)
        {
            int End = Start + Length;
            StringBuilder Out = new StringBuilder();
            for (int Pos = Start; Pos < End; Pos++)
            {
                Out.Append(Characters[Pos]);
            }

            return Out.ToString();
        }

        /// <summary>
        /// Converts the cipher to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder Result = new StringBuilder(Characters.Length);
            foreach (char Character in Characters)
            {
                Result.Append(Character);
            }

            return Result.ToString();
        }
    }

    public class QuadgramScoredCharacterArray : CharacterArray
    {
        public QuadgramScoredCharacterArray()
            : base()
        {
        }
        public QuadgramScoredCharacterArray(string Text)
            : base(Text)
        {
        }
        public QuadgramScoredCharacterArray(int Length)
            : base(Length)
        {
        }
        public QuadgramScoredCharacterArray(char[] Characters)
            : base(Characters)
        {
        }

        /// <summary>
        /// Scores this ciphertext using quadgrams
        /// </summary>
        /// <returns>The score of the text</returns>
        public override double ScoreText()
        {
            double Score = 0;

            int Length = Characters.Length;

            // Have to build up 3 previous
            int[] Previous = new int[3];
            int I = 0;
            for (int C = 0; C < 3; C++)
            {
                for (; I < Length; I++)
                {
                    byte This = Characters[I].ToLetterByte();

                    if (This < 26)
                    {
                        Previous[C] = This * (int)Math.Pow(26, 3 - C);
                        I++;
                        break;
                    }
                }
            }

            for (; I < Length; I++)
            {
                byte ThisCharacter = Characters[I].ToLetterByte();
                if (ThisCharacter < 26)
                {
                    Score += LetterStatistics.Quadgrams[
                        Previous[0] + Previous[1] + Previous[2] + ThisCharacter
                    ];

                    Previous[0] = Previous[1] * 26;
                    Previous[1] = Previous[2] * 26;
                    Previous[2] = ThisCharacter * 26;
                }
            }

            return Score;
        }
    }
}
