using Cipher.Ciphers.Keys;
using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cipher.Ciphers
{
    public class Playfair<TArray> : DefaultCipher<CachingGridArray, TArray>
        where TArray : ITextArray<byte>, new()
	{
        public const byte WIDTH = 5;
        public const byte HEIGHT = 5;

		public int MaxIterations = 5;
		public int InternalIterations = 10000;
        public int OtherIterations = 20;
        public double OtherIterationsChange = 0.2;

		public Playfair(TextScorer scorer) : base(scorer) { }

        public override TArray Decode(TArray cipher, CachingGridArray key, TArray decoded)
        {
            return Decode(cipher, key, decoded, CreateCharacters(cipher, key));
        }

        public TArray Decode(TArray Text, CachingGridArray Key, TArray Decoded, CachingGridArray.Vector[] Characters)
		{
            int Length = Text.Count;
            for (int Index = 0; Index < Length; Index += 2)
			{
                CachingGridArray.Vector A = Characters[Index];
                CachingGridArray.Vector B = Characters[Index + 1];

                byte AColumn = A.X;
                byte ARow = A.Y;

                byte BColumn = B.X;
                byte BRow = B.Y;

                if(ARow == BRow)
                {
                    // Modulus hack to work with negative number
                    Decoded[Index] = Key.Elements[(AColumn - 1 + WIDTH) % WIDTH, ARow];
                    Decoded[Index + 1] = Key.Elements[(BColumn - 1 + WIDTH) % WIDTH, BRow];
                }
                else if (AColumn == BColumn)
                {
                    Decoded[Index] = Key.Elements[AColumn, (ARow - 1 + HEIGHT) % HEIGHT];
                    Decoded[Index + 1] = Key.Elements[AColumn, (BRow - 1 + HEIGHT) % HEIGHT];
                }
                else
                {
                    Decoded[Index] = Key.Elements[BColumn, ARow];
                    Decoded[Index + 1] = Key.Elements[AColumn, BRow];
                }
			}
			return Decoded;
		}

		public override ICipherResult<CachingGridArray, TArray> Crack(TArray Text)
		{
            byte[] Original = new byte[WIDTH * HEIGHT];
            int OriginalLength = WIDTH * HEIGHT;
            for (byte I = 0; I < OriginalLength; I++)
            {
                Original[I] = (byte)(I >= 9 ? I + 1 : I);
            }
            CachingGridArray BestKey = new CachingGridArray(WIDTH, HEIGHT, Original);
			double BestScore = Double.NegativeInfinity;

            CachingGridArray ParentKey = new CachingGridArray((byte[,])BestKey.Elements.Clone());
			double ParentScore = BestScore;

			TArray Decoded = Create(Text.Count);
            CachingGridArray ChildKey = new CachingGridArray(WIDTH, HEIGHT);
            ParentKey.CopyTo(ChildKey);
			double ChildScore;

            CachingGridArray.Vector[] Characters = CreateCharacters(Text, ChildKey);

			for (int Iteration = 0; Iteration < MaxIterations; Iteration++)
			{
				ParentKey.Shuffle();
				Decoded = Decode(Text, ParentKey, Decoded);
				ParentScore = scorer(Decoded);

				ParentKey.CopyTo(ChildKey);

                for(double Iter = OtherIterations; Iter >= 0; Iter -= OtherIterationsChange)
                {
                    for(int Count = 0; Count < InternalIterations; Count++)
                    {
                        switch (MathsUtilities.RandomInstance.Next(50))
                        {
                            case 0:
                                ChildKey.SwapRows();
                                break;
                            case 1:
                                ChildKey.SwapColumns();
                                break;
                            case 2:
                                ChildKey.ReverseSquare();
                                break;
                            case 3:
                                ChildKey.ReverseColumns();
                                break;
                            case 4:
                                ChildKey.ReverseRows();
                                break;
                            default:
                                ChildKey.Swap();
                                break;
                        }

                        Decoded = Decode(Text, ChildKey, Decoded, Characters);
                        ChildScore = scorer(Decoded);

                        double DiffScore = ChildScore - ParentScore;
                        if(DiffScore >= 0)
                        {
                            ParentScore = ChildScore;
                            ChildKey.CopyTo(ParentKey);
                        }
                        else if(Iter > 0)
                        {
                            double Prob = Math.Exp(DiffScore / Iter);

                            // If random probability
                            if(Prob > MathsUtilities.RandomInstance.NextDouble())
                            {
                                ParentScore = ChildScore;
                                ChildKey.CopyTo(ParentKey);
                            }
                        }
                        else
                        {
                            ParentKey.CopyTo(ChildKey);
                        }
                    }
                }

				if (ParentScore > BestScore)
				{
					BestScore = ParentScore;
					ParentKey.CopyTo(BestKey);
				}
			}

			return GetResult(Text, BestScore, BestKey, Decoded);
		}

        public CachingGridArray.Vector[] CreateCharacters(TArray Text, CachingGridArray Key)
        {
            int Length = Text.Count;
            CachingGridArray.Vector[] Characters = new CachingGridArray.Vector[Length];
            for (int Index = 0; Index < Length; Index++)
            {
                Characters[Index] = Key.cache[Text[Index]];
            }

            return Characters;
        }
    }
}
