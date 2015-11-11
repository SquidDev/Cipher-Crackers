using System;
using System.Collections.Generic;
using System.Linq;

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
                if (BChar != byte.MaxValue && BChar < 26) Chars.Add(BChar);
            }

            return Chars.ToArray();
        }

        public static byte[] ToLetterNumber(this String Text)
        {
            List<byte> Chars = new List<byte>();
            foreach (char Character in Text)
            {
                byte BChar = Character.ToLetterByte();
                if (BChar != byte.MaxValue) Chars.Add(BChar);
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
                return (byte)(Character - 'a');
            }
            else if (Character == '#')
            {
                return 26;
            }
            else if (Character >= '0' && Character <= '9')
            {
                return (byte)(Character - '0' + 27); // After '#' symbol
            }

            return byte.MaxValue;
        }

        /// <summary>
        /// Removes whitespace and capitalises
        /// </summary>
        public static string UpperNoSpace(this string Text)
        {
            return new String(Text.Where(C => !Char.IsWhiteSpace(C)).Select(C => Char.ToUpper(C)).ToArray());
        }
    }
}
