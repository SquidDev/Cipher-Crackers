using System;
using System.Linq;
using Cipher.Text;
using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Cracks the Hill cipher using brute force
    /// </summary>
    public sealed class HillBrute : Hill
    {
        public HillBrute(TextScorer scorer, int nGramSize = 2)
            : base(scorer, nGramSize)
        {
        }

        private readonly static float[] combinations = ListUtilities.RangeFloat(26);

        public override ICipherResult<Matrix<float>, NGramArray> Crack(NGramArray cipher)
        {
            NGramArray decoded = Create(cipher.Count);
            Matrix<float> key = Matrix<float>.Build.Dense(NGramSize, NGramSize);

            Matrix<float> bestKey = key.Clone();
            double bestScore = Double.NegativeInfinity;

            for (int rowIndex = 0; rowIndex < NGramSize; rowIndex++)
            {
                bestKey.CopyTo(key);
                foreach (float[] currentRow in combinations.PermutationsRepeat(NGramSize))
                {
                    key.SetRow(rowIndex, currentRow);
            		
                    decoded = Decode(key, decoded);
                    double score = scorer(decoded);
                    if (score > bestScore)
                    {
                        key.CopyTo(bestKey);
                        bestScore = score;
                    }
                }
            }
                
            return GetResult(bestScore, bestKey);
        }
    }
}
