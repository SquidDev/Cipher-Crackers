using Cipher.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Ciphers
{
    public class Substitution<TArray> : BaseCipher<LetterArray, TArray, byte>
        where TArray : TextArray<byte>, new()
	{
		public int MaxIterations = 5;
		public int InternalIterations = 1000;

		public Substitution(string Text) : base(Text) { }
		public Substitution(TArray Text) : base(Text) { }

        public override TArray Decode(LetterArray Key, TArray Decoded)
		{
			int Length = Text.Length;
			for (int Index = 0; Index < Length; Index++)
			{
				Decoded[Index] = Key[Text[Index]];
			}

			return Decoded;
		}

		public override CipherResult Crack()
		{
            LetterArray BestKey = new LetterArray("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
			double BestScore = Double.NegativeInfinity;

            LetterArray ParentKey = new LetterArray((byte[])BestKey.Characters.Clone());
			double ParentScore = BestScore;

			TArray Decoded = Create(Text.Length);
            LetterArray ChildKey = new LetterArray(BestKey.Length);
			double ChildScore;

			for (int Iteration = 0; Iteration < MaxIterations; Iteration++)
			{
				ParentKey.Characters.Shuffle<byte>();
				Decoded = Decode(ParentKey, Decoded);
				ParentScore = Decoded.ScoreText();

				ParentKey.CopyTo(ChildKey);

				int Count = 0;
				while (Count < InternalIterations)
				{
					//Swap characters
					ChildKey.Swap(MathsUtilities.RandomInstance.Next(26), MathsUtilities.RandomInstance.Next(26));

					Decoded = Decode(ChildKey, Decoded);
					ChildScore = Decoded.ScoreText();

					//Reset parent score
					if (ChildScore > ParentScore)
					{
						ParentScore = ChildScore;
						Count = 0;

						ChildKey.CopyTo(ParentKey); //Backup this key
					}
					else
					{
						ParentKey.CopyTo(ChildKey); // Reset ChildKey
					}

					Count++;
				}

				if (ParentScore > BestScore)
				{
					BestScore = ParentScore;
					ParentKey.CopyTo(BestKey);
				}
			}

			return GetResult(BestScore, BestKey, Decoded);
		}
	}
}
