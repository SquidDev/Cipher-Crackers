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
    public class CribSpace
    {
        private readonly List<KeyValuePair<string, string>> cribs = new List<KeyValuePair<string, string>>();
        public readonly int NGramSize;

        public IReadOnlyList<KeyValuePair<string, string>> Cribs { get { return cribs; } }

        public CribSpace(int size)
        {
            NGramSize = size;
        }

        /// <summary>
        /// Adds two letters that map to each other
        /// </summary>
        /// <param name="ciphertext">The ciphertext</param>
        /// <param name="plaintext">Associated plaintext</param>
        public void Add(string ciphertext, string plaintext)
        {
            if (
                ciphertext.Length == 0 || ciphertext.Length % NGramSize != 0 ||
                plaintext.Length == 0 || plaintext.Length % NGramSize != 0)
            {
                throw new ArgumentException("Lengths must be a multiple of NGramLength");
            }
            else if (ciphertext.Length != plaintext.Length)
            {
                throw new ArgumentException("Lengths must be equal");
            }

            int length = ciphertext.Length;
            int nGramLength = NGramSize;
            for (int index = 0; index < length; index += nGramLength)
            {
                cribs.Add(new KeyValuePair<string, string>(ciphertext.Substring(index, nGramLength), plaintext.Substring(index, nGramLength)));
            }
        }
    }

    /// <summary>
    /// Cracks the hill cipher using a crib
    /// </summary>
    public class HillCribbed : Hill
    {
        public HillCribbed(TextScorer scorer, int nGramSize = 2)
            : base(scorer, nGramSize)
        {
        }

        public ICipherResult<Matrix<float>, NGramArray> CrackSingle(NGramArray cipherText, string cribStr, string plainStr)
        {
            MatrixBuilder<float> builder = Matrix<float>.Build;
            Matrix<float> plain = builder.Dense(NGramSize, NGramSize, plainStr.Select(x => (float)x.ToLetterByte()).ToArray());
            Matrix<float> cipher = builder.Dense(NGramSize, NGramSize, cribStr.Select(x => (float)x.ToLetterByte()).ToArray());
            
            BigInteger det = (BigInteger)cipher.Determinant();
            det = Euclid.Modulus(det, 26);

            BigInteger inverseDet, inverseMod;
            if (Euclid.ExtendedGreatestCommonDivisor(det, 26, out inverseDet, out inverseMod) != 1)
            {
                throw new ArgumentException(det + " is not coprime with 26");
            }
            
            Matrix<float> adjugate = cipher.Adjugate();
            Matrix<float> inverse = (adjugate * (float)inverseDet);
            inverse = inverse.Modulus(26);

            Matrix<float> key = (plain * inverse).Modulus(26);
            return GetResult(cipherText, key);
        }
        
        public ICipherResult<Matrix<float>, NGramArray> Crack(string cipher, CribSpace cribs)
        {
        	return Crack(Create(cipher), cribs);
        }

        public ICipherResult<Matrix<float>, NGramArray> Crack(NGramArray cipher, CribSpace cribs)
        {
        	if(cribs.NGramSize != NGramSize) throw new ArgumentException("Incorrect NGram size for cribs");
            if (cribs.Cribs.Count < NGramSize) throw new ArgumentException("Must have " + NGramSize + " cribs", "cribs");

            StringBuilder builder = new StringBuilder();
            ICipherResult<Matrix<float>, NGramArray> best = null;
            foreach (KeyValuePair<string, string>[] pair in cribs.Cribs.Permutations(2))
            {
                try
                {
                    ICipherResult<Matrix<float>, NGramArray> result = CrackSingle(cipher, pair[0].Key + pair[1].Key, pair[0].Value + pair[1].Value);
                    if (best == null || result.Score > best.Score) best = result;
                }
                catch (Exception e)
                {
                    builder.AppendLine(e.Message);
                }
            }
            
            if (best == null) throw new ArgumentException("Cannot find permutation\n" + builder.ToString());
            
            return best;
        }
    }
}
