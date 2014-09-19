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
        #region Loading
        protected const double TOKEN_COUNT = 1024908267229;
        protected const byte UNSEEN_COUNT = 50;

        protected static Dictionary<string, double> MonogramCounts;
        protected static Dictionary<string, double> BigramCounts;
        protected static double[] Unseen;
        protected static void LoadFiles()
        {
            if (MonogramCounts != null) return;

            MonogramCounts = LoadFile("1WordScores");
            // Calculate first order log probabilities
            foreach (string Key in MonogramCounts.Keys.ToArray())
            {
                MonogramCounts[Key] = Math.Log10(MonogramCounts[Key] / TOKEN_COUNT);
            }

            BigramCounts = LoadFile("2WordScores");
            // Calculate second order log probabilities
            foreach (string Key in BigramCounts.Keys.ToArray())
            {
                string[] Words = Key.Split(' ');
                if (Words.Length < 2) continue;

                double Word1Result;
                if (MonogramCounts.TryGetValue(Words[0], out Word1Result))
                {
                    BigramCounts[Key] = Math.Log10(BigramCounts[Key] / TOKEN_COUNT) - Word1Result;
                }
                else
                {
                    BigramCounts[Key] = Math.Log10(BigramCounts[Key] / TOKEN_COUNT);
                }
            }

            Unseen = new double[UNSEEN_COUNT];
            for(int L = 0; L < UNSEEN_COUNT; L++)
            {
                Unseen[L] = Math.Log10(10 / (TOKEN_COUNT * Math.Pow(10, L)));
            }
        }

        protected static Dictionary<string, double> LoadFile(string FileName)
        {
            Assembly ThisAssembly = Assembly.GetExecutingAssembly();

            Dictionary<string, double> ToLoad = new Dictionary<string, double>();
            using(Stream ResourceStream = ThisAssembly.GetManifestResourceStream("Cipher.Analysis.AutoSpace." + FileName + ".txt"))
            {
                using(StreamReader Reader = new StreamReader(ResourceStream))
                {
                    while(Reader.Peek() >= 0)
                    {
                        string[] Line = Reader.ReadLine().Split('\t');
                        if (Line.Length < 2) continue;

                        string Name = Line[0].ToUpper();
                        double Score = 0;
                        try
                        {
                            Score = Convert.ToDouble(Line[1]);
                            double OldScore = 0;
                            ToLoad.TryGetValue(Name, out OldScore);
                            ToLoad[Name] = Score + OldScore;
                        }
                        catch { }
                    }
                }
            }
            return ToLoad;
        }
        #endregion

        public const byte MAX_WORD_LENGTH = 20;

        public string Result { get; protected set; }
        public double Score { get; protected set; }
        public WordGuesser(string Input)
        {
            LoadFiles();

            // Trim whitespace and uppercase
            Input = Input.UpperNoSpace();
            int TextLength = Input.Length;

            double[,] Probabilities = new double[TextLength, MAX_WORD_LENGTH];
            string[,][] Strings = new string[TextLength, MAX_WORD_LENGTH][];

            for(int X = 1; X < TextLength; X++)
            {
                for(int Y = 0; Y < MAX_WORD_LENGTH; Y++)
                {
                    Probabilities[X, Y] = Double.NegativeInfinity;
                    Strings[X, Y] = new string[] { " " };
                }
            }

            for(int Y = 0; Y < MAX_WORD_LENGTH; Y++)
            {
                string Sub = Input.Substring(0, Y + 1);
                Probabilities[0, Y] = ConditionalWordProbability(Sub);
                Strings[0, Y] = new string[]{ Sub };
            }

            double BestProbability = Double.NegativeInfinity;
            string[] BestStrings = null;
           

            for(int I = 1; I < TextLength; I++)
            {
                int Min = Math.Min(I, MAX_WORD_LENGTH);

                for(int J = 0; J < MAX_WORD_LENGTH; J++)
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

            int Minimum = Math.Min(TextLength, MAX_WORD_LENGTH);

            BestProbability = Double.NegativeInfinity;

            for (int I = 0; I < Minimum; I++)
            {
                double ThisProbability = Probabilities[TextLength - I - 1, I];
                if(ThisProbability > BestProbability)
                {
                    BestStrings = Strings[TextLength - I - 1, I];
                }
            }

            Result = String.Join(" ", BestStrings);
            Score = BestProbability;
        }

        protected double ConditionalWordProbability(string Word, string Previous = "<UNK>")
        {
            double SingleScore, DoubleScore;
            if (MonogramCounts.TryGetValue(Word, out SingleScore))
            {

                if (BigramCounts.TryGetValue(Previous + " " + Word, out DoubleScore))
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
