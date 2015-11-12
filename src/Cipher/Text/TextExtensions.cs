using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Text
{
    public static class TextExtensions
    {
        /// <summary>
        /// Extracts the Letters from the string
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static byte[] ToLetterArray(this IEnumerable<char> Text)
        {
        	return Text.ToLetters().ToArray();
        }
        
        public static IEnumerable<byte> ToLetters(this IEnumerable<char> Text)
        {
        	return Text.Select(x => x.ToLetterByte()).Where(x => x < 26);
        }

        public static byte[] ToLetterNumber(this IEnumerable<char> Text)
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
        
        public static string Substring(this ITextArray array, int start, int length)
        {
        	StringBuilder builder = new StringBuilder(length);
        	for(int i = start; i < start + length; i++)
        	{
        		byte value = array[i];
        		if(value < 26) builder.Append(value + 'A');
        	}
        	
        	return builder.ToString();
        }
    }
}
