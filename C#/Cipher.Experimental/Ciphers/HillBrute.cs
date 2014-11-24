using System;
using Cipher.Text;
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
        { }
        public HillBrute(TArray text) : base(text) { }

        public override CipherResult Crack()
        {
            if (NGramLength != 2) throw new NotImplementedException();

            TArray decoded = new TArray();
            decoded.NGramLength = 2;
            decoded.Initalise(Text.Length);

            Matrix<float> key = Matrix<float>.Build.Dense(2, 2, 0);

            Matrix<float> bestKey = key.Clone();
            double bestScore = Double.NegativeInfinity;

            for (int a = 0; a < 26; a++)
            {
                key[0, 0] = a;
                for (int b = 0; b < 26; b++)
                {
                    Console.WriteLine("{0}, {1}", a, b);
                    key[0, 1] = b;
                    for (int c = 0; c < 26; c++)
                    {
                        key[1, 0] = c;
                        for (int d = 0; d < 26; d++)
                        {
                            key[1, 1] = d;

                            decoded = Decode(key, decoded);
                            double score = decoded.ScoreText();
                            if (score > bestScore)
                            {
                                bestScore = score;
                                key.CopyTo(bestKey);
                            }

                        }
                    }
                }
            }

            return GetResult(bestScore, bestKey, decoded);
        }
    }
}
