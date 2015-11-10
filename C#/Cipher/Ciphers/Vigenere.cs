using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;

namespace Cipher.Ciphers
{
    public class Vigenere<TArray> : BaseCipher<LetterArray, TArray, byte>
        where TArray : TextArray<byte>, new()
    {
        #region Guessing variables
        /// <summary>
        /// Maximum length used for finding repeated sequences when guessing the key length
        /// </summary>
        /// /// <seealso cref="MinWordLength"/>
        public int MaxWordLength = 5;

        /// <summary>
        /// Minimum length used for finding repeated sequences when guessing the key length
        /// </summary>
        /// <seealso cref="MaxWordLength"/>
        public int MinWordLength = 3;

        /// <summary>
        /// Minimum length used for key length the key length
        /// </summary>
        /// <seealso cref="MaxKeyLength"/>
        public int MinKeyLength = 2;

        /// <summary>
        /// Maximum length used for key length the key length
        /// </summary>
        /// <seealso cref="MaxKeyLength"/>
        public int MaxKeyLength = 20;
        #endregion

        public Vigenere(string Text)
            : base(Text)
        {
        }
        public Vigenere(TArray Text)
            : base(Text)
        {
        }

        public override TArray Decode(LetterArray Key, TArray Decoded)
        {
            int Length = Text.Length;
            int KeyLength = Key.Length;

            for (int Index = 0; Index < Length; Index++)
            {
                Decoded[Index] = (byte)((Text[Index] + 26 - Key[Index % KeyLength]) % 26);
            }

            return Decoded;
        }
    
        public override BaseCipher<LetterArray,TArray,byte>.CipherResult Crack()
        {
            throw new NotImplementedException();
        }

        public int GuessKeyLength()
        {
            BasicDefaultDict<string, List<int>> Positions = new BasicDefaultDict<string, List<int>>();
            int Length = Text.Length;
            for (int WordLength = MinWordLength; WordLength <= MaxWordLength; WordLength++)
            {
                int End = Length - WordLength + 1;
                for (int Position = 0; Position < End; Position++)
                {
                    Positions.GetOrDefault(Text.Substring(Position, WordLength)).Add(Position);
                }
            }

            // Calculate position differences
            int[] Factors = new int[MaxKeyLength - MinKeyLength];
            foreach (KeyValuePair<string, List<int>> Word in Positions)
            {
                int PositionLength = Word.Value.Count;
                // Cull non-repeating sequences
                if (PositionLength > 1)
                {
                    PositionLength--;

                    for (int Position = 0; Position < PositionLength; Position++)
                    {
                        int Diff = Word.Value[Position + 1] - Word.Value[Position];
                        for (int N = MinKeyLength; N < MaxKeyLength; N++)
                        {
                            if (Diff % N == 0)
                            {
                                Factors[N - MinKeyLength]++;
                            }
                        }
                    }

                }
            }

            return Factors.MaxIndex() + MinKeyLength;
        }
    }

    public class MonogramVigenere : Vigenere<LetterArray>
    {
        public MonogramVigenere(string CipherText)
            : base(CipherText)
        {
        }
        public MonogramVigenere(LetterArray CipherText)
            : base(CipherText)
        {
        }

        public CipherResult Crack(int KeyLength = -1)
        {
            if (KeyLength <= 0) KeyLength = GuessKeyLength();
            
            LetterArray Key = new LetterArray(KeyLength);
            int Length = Text.Length;

            LetterArray Decoded = new LetterArray(Length);

            List<byte>[] Items = new List<byte>[KeyLength];
            // Fill array
            for (int KeyNo = 0; KeyNo < KeyLength; KeyNo++)
            {
                Items[KeyNo] = new List<byte>();
            }

            // Split characters
            for (int Index = 0; Index < Length; Index++)
            {
                Items[Index % KeyLength].Add(Text[Index]);
            }

            // Solve ciphers and rebuild
            for (int KeyNo = 0; KeyNo < KeyLength; KeyNo++)
            {
                MonogramCaeserShift Shift = new MonogramCaeserShift(new MonogramScoredLetterArray((byte[])Items[KeyNo].ToArray()));
                MonogramCaeserShift.CipherResult Result = Shift.Crack();

                Key[KeyNo] = Result.Key;
                int ResultLength = Result.Text.Length;
                for (int Index = 0; Index < ResultLength; Index++)
                {
                    Decoded[Index * KeyLength + KeyNo] = Result.Text[Index];
                }
            }

            return new CipherResult(Decoded, 0, Key);
        }
    }
}
