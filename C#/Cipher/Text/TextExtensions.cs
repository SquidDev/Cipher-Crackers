using System;
using System.Collections.Generic;

namespace Cipher.Text
{
    public static class TextExtensions
    {
        /// <summary>
        /// Extracts the Letters from the string
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static byte[] ToLetterArray(this String Text)
        {
            List<byte> Chars = new List<byte>();
            foreach (char Character in Text)
            {
                byte BChar = Character.ToLetterByte();
                if(BChar != byte.MaxValue) Chars.Add(BChar);
            }

            return Chars.ToArray();
        }

        public static byte ToLetterByte(this Char Character)
        {
            if (Character >= 'A' && Character <= 'Z')
            {
                return (byte)(Character - 'A');
            }
            else if (Character >= 'a' && Character <= 'z')
            {
                return (byte)(Character - 'z');
            }

            return byte.MaxValue;
        }
    }
}
