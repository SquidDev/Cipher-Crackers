using Cipher.Ciphers.Keys;
using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cipher.Ciphers
{
    public class Playfair : DefaultCipher<CachingGridArray, LetterTextArray>
	{
        public const byte Width = 5;
        public const byte Height = 5;

		public int MaxIterations = 10;
		public int InternalIterations = 10000;
        public int OtherIterations = 20;
        public double OtherIterationsChange = 0.2;

		public Playfair(TextScorer scorer) : base(scorer) { }
		
		public CachingGridArray.Vector[] GenerateLookup(LetterTextArray text, CachingGridArray key)
		{
			CachingGridArray.Vector[] lookup = new CachingGridArray.Vector[text.Count];
			for(int i = 0; i < text.Count; i++)
			{
				lookup[i] = key.Cache[text[i]];
			}
			
			return lookup;
		}
		
		public override LetterTextArray Decode(LetterTextArray text, CachingGridArray key, LetterTextArray decoded)
		{
			return Decode(GenerateLookup(text, key), key, decoded);
		}

		protected LetterTextArray Decode(CachingGridArray.Vector[] text, CachingGridArray key, LetterTextArray decoded)
		{
            for (int i = 0; i < text.Length; i += 2)
			{
            	CachingGridArray.Vector a = text[i];
            	CachingGridArray.Vector b = text[i + 1];

                byte aX = a.X;
                byte aY = a.Y;

                byte bX = b.X;
                byte bY = b.Y;

                if(aY == bY)
                {
                    // Modulus hack to work with negative number
                    decoded[i] = key.Elements[(aX - 1 + Width) % Width, aY];
                    decoded[i + 1] = key.Elements[(bX - 1 + Width) % Width, bY];
                }
                else if (aX == bX)
                {
                    decoded[i] = key.Elements[aX, (aY - 1 + Height) % Height];
                    decoded[i + 1] = key.Elements[aX, (bY - 1 + Height) % Height];
                }
                else
                {
                    decoded[i] = key.Elements[bX, aY];
                    decoded[i + 1] = key.Elements[aX, bY];
                }
			}
			return decoded;
		}

		public override ICipherResult<CachingGridArray, LetterTextArray> Crack(LetterTextArray cipher)
		{
			// We improve the key every time, so cannot run in parallel :(.

			var result = CrackSingle(cipher, 0);
        	for(int i = 1; i < MaxIterations; i++)
        	{
        		CachingGridArray currentKey = new CachingGridArray(Width, Height, 26);
				result.Key.CopyTo(currentKey);
        		var newRes = CrackSingle(cipher, i, currentKey);
        		if(newRes.Score > result.Score) result = newRes;
        	}
        	
        	return result;
		}

        protected void ModifyKey(Random random, CachingGridArray key)
        {
        	switch (random.Next(50))
	        {
	            case 0:
	                key.SwapRows(random);
	                break;
	            case 1:
	                key.SwapColumns(random);
	                break;
	            case 2:
	                key.ReverseSquare();
	                break;
	            case 3:
	                key.ReverseColumns();
	                break;
	            case 4:
	                key.ReverseRows();
	                break;
	            default:
	                key.Swap(random);
	                break;
	        }
        }
        
        protected ICipherResult<CachingGridArray, LetterTextArray> CrackSingle(LetterTextArray text, int iter)
        {
        	byte[] original = new byte[Width * Height];
            int originalLength = Width * Height;
            for (byte i = 0; i < originalLength; i++)
            {
                original[i] = (byte)(i >= 9 ? i + 1 : i);
            }
            
        	CachingGridArray bestKey = new CachingGridArray(Width, Height, original, 26);
            bestKey.Shuffle();
            return CrackSingle(text, iter, bestKey);
        }
        
        protected ICipherResult<CachingGridArray, LetterTextArray> CrackSingle(LetterTextArray text, int iter, CachingGridArray bestKey)
        {
        	Console.WriteLine("Starting " + iter + " with " + bestKey);

        	CachingGridArray currentKey = new CachingGridArray(Width, Height, 26);
			bestKey.CopyTo(currentKey);
			CachingGridArray.Vector[] lookup = GenerateLookup(text, currentKey);

			LetterTextArray decoded = new LetterTextArray(text.Count);
			double bestScore = scorer(Decode(lookup, bestKey, decoded));
			
			CachingGridArray parentKey = new CachingGridArray(Width, Height, 26);
			bestKey.CopyTo(parentKey);
			double parentScore = bestScore; 
			
			Random random = MathsUtilities.RandomInstance;
			for(double temp = OtherIterations; temp >= 0; temp -= OtherIterationsChange)
            {
                for(int i = 0; i < InternalIterations; i++)
                {
                	ModifyKey(random, currentKey);

                    double childScore = scorer(Decode(lookup, currentKey, decoded));
                    double deltaScore = childScore - parentScore;

                    if(deltaScore >= 0)
                    {
                    	// Improved score, copy over
                        parentScore = childScore;
                        currentKey.CopyTo(parentKey);
                        
                        // Best score so far
                        if(parentScore > bestScore)
                        {
                        	bestScore = parentScore;
                        	parentKey.CopyTo(bestKey);
                        }
                    }
                    else if(temp > 0)
                    {
                        double probability = Math.Exp(deltaScore / temp);

                        // If random probability, keep it anyway
                        if(probability > random.NextDouble())
                        {
                            parentScore = childScore;
                            currentKey.CopyTo(parentKey);
                        }
                        else
                        {
                        	// Otherwise fall back to the previous key
                        	parentKey.CopyTo(currentKey);
                        }
                    }
                    else
                    {
                    	// Otherwise fall back to the previous key
                        parentKey.CopyTo(currentKey);
                    }
                }
            }
			
			Console.WriteLine("Got " + bestScore + " using " + bestKey + " => " + Decode(lookup, bestKey, decoded));
            
            return GetResult(text, bestScore, bestKey, decoded);
        }
    }
}
