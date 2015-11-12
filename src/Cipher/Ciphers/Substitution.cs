using Cipher.Text;
using Cipher.Utils;
using System;
using System.Linq;

namespace Cipher.Ciphers
{
    public class Substitution<TText> : DefaultCipher<byte[], TText>
    	where TText : ITextArray<byte>, new()
    {
        public const int MaxIterations = 5;
        public const int InternalIterations = 1000;

        public Substitution(TextScorer scorer)
            : base(scorer)
        {
        }

        public override TText Decode(TText cipher, byte[] key, TText decoded)
        {
            int length = cipher.Count;
            for (int index = 0; index < length; index++)
            {
                decoded[index] = key[cipher[index]];
            }

            return decoded;
        }
        
        private ICipherResult<byte[], TText> CrackSingle(TText cipher)
        {
        	byte[] bestKey = ListUtilities.RangeByte(26);
        	bestKey.Shuffle();
        	
        	TText decoded = Create(cipher.Count);
        	Decode(cipher, bestKey, decoded);
        	double bestScore = scorer(decoded);
        	
            byte[] currentKey = new byte[bestKey.Length];
            bestKey.CopyTo(currentKey);
            
            int count = 0;
            while (count < InternalIterations)
            {
                //Swap characters
                currentKey.Swap(MathsUtilities.RandomInstance.Next(26), MathsUtilities.RandomInstance.Next(26));

                Decode(cipher, currentKey, decoded);
                double currentScore = scorer(decoded);

                if (currentScore > bestScore)
                {
                	// Reset count, and set new key
                	count = 0;
                	
                    bestScore = currentScore;
                    currentKey.CopyTo(bestKey);
                }
                else
                {
                	// Revert to old key
                    bestKey.CopyTo(currentKey);
                }

                count++;
            }
            
            var x = GetResult(cipher, bestScore, bestKey, decoded);
            Console.WriteLine("Iteration: " + KeyConverters.String.ToString(bestKey) + " =>\n" + x.Contents.ToString());
            return x;
        }

        public override ICipherResult<byte[], TText> Crack(TText cipher)
        {
        	return AsyncUtils.RunAsync(MaxIterations, () => CrackSingle(cipher)).MaxWith(x => x.Score);
        }
    }
}
