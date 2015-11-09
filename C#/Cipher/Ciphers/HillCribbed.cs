using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using Cipher.Text;
using Cipher.Utils;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Cracks the hill cipher using a crib
    /// </summary>
    public class HillCribbed<TArray> : Hill<TArray>
        where TArray : NGramArray, new()
    {
        protected List<KeyValuePair<string, string>> Cribs = new List<KeyValuePair<string, string>>();

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
            	Cribs.Add(new KeyValuePair<string, string>(ciphertext.Substring(index, nGramLength), plaintext.Substring(index, nGramLength)));
            }
        }
        
        public CipherResult CrackSingle(string cribStr, string plainStr)
        {
        	int nGramLength = NGramLength;
        	
        	MatrixBuilder<float> builder = Matrix<float>.Build;
        	Matrix<float> plain = builder.Dense(nGramLength, nGramLength, plainStr.Select(x => (float)x.ToLetterByte()).ToArray());
            Matrix<float> cipher = builder.Dense(nGramLength, nGramLength, cribStr.Select(x => (float)x.ToLetterByte()).ToArray());
            
            BigInteger det = (BigInteger)cipher.Determinant();
            det = Euclid.Modulus(det, 26);
            
            // http://planetcalc.com/3311/
            BigInteger inverseDet, inverseMod;
            if(Euclid.ExtendedGreatestCommonDivisor(det, 26, out inverseDet, out inverseMod) != 1)
            {
            	throw new ArgumentException(det + " is not coprime with 26");
            }
            
            Console.WriteLine("Inverses:");
            Console.WriteLine(det);
            Console.WriteLine(inverseDet);
            
            Matrix<float> adjugate = cipher.Adjugate();
            Matrix<float> inverse = (adjugate * (float)inverseDet);
            inverse = inverse.Modulus(26);

            Matrix<float> key = (plain * inverse).Modulus(26);
            return GetResult(0, key);
        }

        public override CipherResult Crack()
        {
            if (Cribs.Count < NGramLength) throw new Exception("HillCribbed required at least two cribs");

            StringBuilder builder = new StringBuilder();
            foreach(IReadOnlyList<KeyValuePair<string, string>> pair in Cribs.Permutations(2))
            {
            	try
            	{
            		return CrackSingle(pair[0].Key + pair[1].Key, pair[0].Value + pair[1].Value);
            	}
            	catch (Exception e)
            	{
            		builder.AppendLine(e.Message);
            	}
            }
            
            throw new ArgumentException("Cannot find permutation\n" + builder.ToString());
        }
    }
}
