using Cipher.Text;
using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Cipher.Analysis.AutoSpace
{
    public class WordGuesser
    {
        public byte MaxWordLength = 20;

        public string Result { get; protected set; }
        public double Score { get; protected set; }
        public WordGuesser(string Input)
        {
            if (String.IsNullOrWhiteSpace(Input))
            {
                throw new ArgumentException("Input cannot be null or whitespace");
            }

            // Trim whitespace and uppercase
            Input = Input.UpperNoSpace();
            int TextLength = Input.Length;

            MaxWordLength = (byte)Math.Min(MaxWordLength, TextLength);

            double[,] Probabilities = new double[TextLength, MaxWordLength];
            string[,][] Strings = new string[TextLength, MaxWordLength][];

            for(int X = 1; X < TextLength; X++)
            {
                for(int Y = 0; Y < MaxWordLength; Y++)
                {
                    Probabilities[X, Y] = Double.NegativeInfinity;
                    Strings[X, Y] = new string[] { " " };
                }
            }

            for(int Y = 0; Y < MaxWordLength; Y++)
            {
                string Sub = Input.Substring(0, Y + 1);
                Probabilities[0, Y] = ConditionalWordProbability(Sub);
                Strings[0, Y] = new string[]{ Sub };
            }

            double BestProbability = Double.NegativeInfinity;
            string[] BestStrings = null;
           

            for(int I = 1; I < TextLength; I++)
            {
                int Min = Math.Min(I, MaxWordLength);

                for(int J = 0; J < MaxWordLength; J++)
                {
                    if (I + J + 1 > TextLength) break;

                    BestProbability = Double.NegativeInfinity;

                    for(int K = 0; K < Min; K++)
                    {
                        string[] OldStrings = Strings[I-K-1, K];
                        string ThisString = Input.Substring(I, J + 1);
                        double ThisProbability = Probabilities[I - K - 1, K] + ConditionalWordProbability(ThisString, OldStrings.LastValue());

                        if(ThisProbability > BestProbability)
                        {
                            int OldLength = OldStrings.Length;
                            BestProbability = ThisProbability;
                            BestStrings = new string[OldLength + 1];
                            OldStrings.CopyTo(BestStrings, 0);
                            BestStrings[OldLength] = ThisString;
                        }
                    }

                    Probabilities[I, J] = BestProbability;
                    Strings[I, J] = BestStrings;
                }
            }

            int Minimum = Math.Min(TextLength, MaxWordLength);

            BestProbability = Double.NegativeInfinity;

            for (int I = 0; I < Minimum; I++)
            {
                double ThisProbability = Probabilities[TextLength - I - 1, I];
                if(ThisProbability > BestProbability)
                {
                    BestStrings = Strings[TextLength - I - 1, I];
                    BestProbability = ThisProbability;
                }
            }

            Result = String.Join(" ", BestStrings);
            Score = BestProbability;
        }

        protected double ConditionalWordProbability(string Word, string Previous = "<UNK>")
        {
            double SingleScore, DoubleScore;
            if (AutoSpaceData.WordOne.TryGetValue(Word, out SingleScore))
            {

                if (AutoSpaceData.WordTwo.TryGetValue(Previous + " " + Word, out DoubleScore))
                {
                    return DoubleScore;
                }
                else
                {
                    return SingleScore;
                }
            }
            else
            {
                return Unseen[Word.Length];
            }
        }
        
    }
}
