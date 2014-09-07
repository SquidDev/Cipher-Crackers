using Cipher.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Ciphers
{
    public class Vigenere<TArray> : BaseCipher<LetterArray, TArray, byte>
        where TArray : TextArray<byte>, new()
    {
        public Vigenere(string Text) : base(Text) { }
        public Vigenere(TArray Text) : base(Text) { }

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
    }

    public class MonogramVigenere : Vigenere<LetterArray>
    {
        public MonogramVigenere(string CipherText) : base(CipherText) { }
        public MonogramVigenere(LetterArray CipherText) : base(CipherText) { }

        public override CipherResult Crack()
        {
            int KeyLength = 7;
            
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
