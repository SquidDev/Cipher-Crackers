using System;
using System.Collections.Generic;
using System.Linq;
using Cipher.Utils;
using Cipher.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Cracks the hill cipher using a crib
    /// </summary>
    public class HillCribbed<TArray> : Hill<TArray>
        where TArray : NGramArray, new()
    {
        protected Dictionary<string, string> Cribs = new Dictionary<string, string>();

        public HillCribbed(string text, int nGramLength = 2)
            : base(text, nGramLength)
        { }
        public HillCribbed(TArray text) : base(text) { }

        /// <summary>
        /// Adds two letters that map to each other
        /// </summary>
        /// <param name="ciphertext">The ciphertext</param>
        /// <param name="plaintext">Associated plaintext</param>
        public void Add(string ciphertext, string plaintext)
        {
            if(
                ciphertext.Length == 0 || ciphertext.Length % NGramLength != 0 || 
                plaintext.Length == 0 || plaintext.Length % NGramLength != 0
            ) 
            {
                throw new ArgumentException("Lengths must be a multiple of NGramLength");
            } else if(ciphertext.Length != plaintext.Length)
            {
                throw new ArgumentException("Lengths must be equal");
            }

            int length = ciphertext.Length;
            int nGramLength = NGramLength;
            for(int index = 0; index < length; index += nGramLength)
            {
                Cribs[ciphertext.Substring(index, nGramLength)] = plaintext.Substring(index, nGramLength);
            }
        }

        public override CipherResult Crack()
        {
            if (Cribs.Count < 2) throw new Exception("HillCribbed required at least two cribs");

            int nGramLength = NGramLength;

            List<float> plainList = new List<float>();
            List<float> cipherList = new List<float>();
            foreach(KeyValuePair<string, string> characters in Cribs.Take(2))
            {
                foreach(char character in characters.Key)
                {
                    cipherList.Add(character.ToLetterByte());
                }

                foreach (char character in characters.Value)
                {
                    plainList.Add(character.ToLetterByte());
                }
            }

            MatrixBuilder<float> builder = Matrix<float>.Build;
            Matrix<float> plain = builder.Dense(nGramLength, nGramLength, plainList.ToArray());
            Matrix<float> cipher = builder.Dense(nGramLength, nGramLength, cipherList.ToArray());

            Console.WriteLine("Plain");
            Console.WriteLine(plain.ToMatrixString());
            Console.WriteLine("Cipher");
            Console.WriteLine(cipher.ToMatrixString());

            float determinate = ((cipher.Determinant() % 26) + 26) % 26;
            float determinateInverse = 0;
            for (; determinateInverse < 26; determinateInverse++)
            {
                if ((determinate * determinateInverse) % 26 == 1) break;
            }

            Matrix<float> key = cipher.Adjugate();
            Console.WriteLine("Key {0} ", key);
            
            key.Multiply(determinateInverse);
            Console.WriteLine("Key * det({1}) {0} ", key, determinateInverse);
            
            key = key.Modulus(26);
            Console.WriteLine("Key % 26 {0} ", key);
            
            key = plain * key;
            Console.WriteLine("Plain * Key {0} ", key);
            
            key = key.Modulus(26);
            Console.WriteLine("Key % 26 {0} ", key);


            return GetResult(0, key);
        }
    }
}
