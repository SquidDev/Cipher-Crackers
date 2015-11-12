using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;

namespace Cipher.Ciphers
{
    public class Vigenere<TArray> : BaseCipher<byte[], TArray>
        where TArray : ITextArray<byte>
    {
        #region Guessing variables

        /// <summary>
        /// Maximum length used for finding repeated sequences when guessing the key length
        /// </summary>
        /// /// <seealso cref="MinWordLength"/>
        public const int MaxWordLength = 5;

        /// <summary>
        /// Minimum length used for finding repeated sequences when guessing the key length
        /// </summary>
        /// <seealso cref="MaxWordLength"/>
        public const int MinWordLength = 3;

        /// <summary>
        /// Minimum length used for key length the key length
        /// </summary>
        /// <seealso cref="MaxKeyLength"/>
        public const int MinKeyLength = 2;

        /// <summary>
        /// Maximum length used for key length the key length
        /// </summary>
        /// <seealso cref="MaxKeyLength"/>
        public const int MaxKeyLength = 20;

        #endregion

        public Vigenere(TextScorer scorer)
            : base(scorer)
        {
        }

        public override TArray Decode(TArray Text, byte[] Key, TArray Decoded)
        {
            int Length = Text.Count;
            int KeyLength = Key.Count;

            for (int Index = 0; Index < Length; Index++)
            {
                Decoded[Index] = (byte)((Text[Index] + 26 - Key[Index % KeyLength]) % 26);
            }

            return Decoded;
        }

        public int GuessKeyLength(TArray Text)
        {
            Dictionary<string, List<int>> Positions = new Dictionary<string, List<int>>();
            int Length = Text.Count;
            for (int WordLength = MinWordLength; WordLength <= MaxWordLength; WordLength++)
            {
                int End = Length - WordLength + 1;
                for (int Position = 0; Position < End; Position++)
                {
                    Positions.GetOrCreate(Text.Substring(Position, WordLength)).Add(Position);
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

    public class MonogramVigenere<TText> : Vigenere<TText>
        where TText : ITextArray<byte>
    {
        public MonogramVigenere(TextScorer scorer)
            : base(scorer)
        {
        }

        public ICipherResult<byte[], TText> Crack(TText Text, int KeyLength = -1)
        {
            if (KeyLength <= 0) KeyLength = GuessKeyLength();
            
            byte[] Key = new byte[KeyLength];
            int Length = Text.Count;

            byte[] Decoded = new byte[Length];

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

            MonogramCaeserShift<TText> Shift = new MonogramCaeserShift<TText>();

            // Solve ciphers and rebuild
            for (int KeyNo = 0; KeyNo < KeyLength; KeyNo++)
            {
                ICipherResult<byte, TText> Result = Shift.Crack(Create(Items[KeyNo]));

                Key[KeyNo] = Result.Key;
                int ResultLength = Result.Contents.Count;
                for (int Index = 0; Index < ResultLength; Index++)
                {
                    Decoded[Index * KeyLength + KeyNo] = Result.Contents[Index];
                }
            }

            return GetResult(Key);
        }
    }
}
