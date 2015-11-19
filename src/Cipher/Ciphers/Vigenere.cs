using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;

namespace Cipher.Ciphers
{
	public class Vigenere<TText> : DefaultCipher<byte[], TText>, IInvertableCipher<byte[], TText>
    	where TText : ITextArray<byte>, new()
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

        public Vigenere()
            : base(TextScorers.ScoreMonograms)
        {
        }

        public override TText Decode(TText cipher, byte[] key, TText decoded)
        {
            int length = cipher.Count;
            int keyLength = key.Length;

            for (int index = 0; index < length; index++)
            {
                decoded[index] = (byte)((cipher[index] + 26 - key[index % keyLength]) % 26);
            }

            return decoded;
        }

        public int GuessKeyLength(TText cipher)
        {
            Dictionary<string, List<int>> positions = new Dictionary<string, List<int>>();
            int length = cipher.Count;
            for (int wordLength = MinWordLength; wordLength <= MaxWordLength; wordLength++)
            {
                int end = length - wordLength + 1;
                for (int position = 0; position < end; position++)
                {
                    positions.GetOrCreate(cipher.Substring(position, wordLength)).Add(position);
                }
            }

            // Calculate position differences
            int[] factors = new int[MaxKeyLength - MinKeyLength];
            foreach (KeyValuePair<string, List<int>> word in positions)
            {
                int positionLength = word.Value.Count;
                // Cull non-repeating sequences
                if (positionLength > 1)
                {
                    positionLength--;

                    for (int position = 0; position < positionLength; position++)
                    {
                        int diff = word.Value[position + 1] - word.Value[position];
                        for (int n = MinKeyLength; n < MaxKeyLength; n++)
                        {
                            if (diff % n == 0)
                            {
                                factors[n - MinKeyLength]++;
                            }
                        }
                    }

                }
            }

            return factors.MaxIndex() + MinKeyLength;
        }
        
        public override ICipherResult<byte[], TText> Crack(TText cipher)
        {
        	return Crack(cipher, 0);
        }
        
        public ICipherResult<byte[], TText> Crack(string cipher, int keyLength = 0)
        {
        	return Crack(Create(cipher), keyLength);
        }
    		
		public ICipherResult<byte[], TText> Crack(TText cipher, int keyLength = 0)
		{
			if (keyLength <= 0) keyLength = GuessKeyLength(cipher);
            
            byte[] key = new byte[keyLength];
            int length = cipher.Count;

            byte[] decoded = new byte[length];

            List<byte>[] Items = new List<byte>[keyLength];
            // Fill array
            for (int KeyNo = 0; KeyNo < keyLength; KeyNo++)
            {
                Items[KeyNo] = new List<byte>(length / keyLength);
            }

            // Split characters
            for (int Index = 0; Index < length; Index++)
            {
                Items[Index % keyLength].Add(cipher[Index]);
            }

            MonogramCaeserShift<TText> shift = new MonogramCaeserShift<TText>();

            // Solve ciphers and rebuild
            for (int KeyNo = 0; KeyNo < keyLength; KeyNo++)
            {
            	TText partialCipher = new TText();
            	partialCipher.Initalise(Items[KeyNo]);
                ICipherResult<byte, TText> Result = shift.Crack(partialCipher);

                key[KeyNo] = Result.Key;
                int ResultLength = Result.Contents.Count;
                for (int Index = 0; Index < ResultLength; Index++)
                {
                    decoded[Index * keyLength + KeyNo] = Result.Contents[Index];
                }
            }

            return GetResult(cipher, key);
		}
		
		public byte[] Invert(byte[] key)
		{
			byte[] result = new byte[key.Length];
			for(int i = 0; i < key.Length; i++)
			{
				result[i] = (byte)(26 - key[i]);
			}
			
			return result;
		}
    }
}
