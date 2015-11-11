using System;
using System.Linq;
using Cipher.Text;
using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Hacky class for solving 2x2 HillCiphers
    /// </summary>
    /// <typeparam name="TArray"></typeparam>
    public class HillBrute<TArray> : Hill<TArray>
        where TArray : NGramArray, new()
    {
        public HillBrute(string text, int nGramLength = 2)
            : base(text, nGramLength)
        {
        }
        public HillBrute(TArray text)
            : base(text)
        {
        }

        public override CipherResult Crack()
        {
            TArray decoded = new TArray();
            decoded.NGramLength = NGramLength;
            decoded.Initalise(Text.Length);

            Matrix<float> key = Matrix<float>.Build.Dense(NGramLength, NGramLength);

            Matrix<float> bestKey = key.Clone();
            double bestScore = Double.NegativeInfinity;
            
            float[] combinations = ListUtilities.FloatRange(26);

            for (int rowIndex = 0; rowIndex < NGramLength; rowIndex++)
            {
            	bestKey.CopyTo(key);
            	foreach(float[] currentRow in combinations.PermutationsRepeat(NGramLength))
            	{
            		key.SetRow(rowIndex, currentRow);
            		
            		decoded = Decode(key, decoded);
            		double score = decoded.ScoreText();
            		if(score > bestScore)
            		{
            			key.CopyTo(bestKey);
            			bestScore = score;
            		}
            	}
            }

            Console.WriteLine(bestKey);
            return GetResult(bestScore, bestKey);
        }
    }
}
