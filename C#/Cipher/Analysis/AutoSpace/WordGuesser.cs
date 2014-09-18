using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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

            /*prob = [[-99e99]*maxwordlen for _ in range(len(text))]
        strs = [['']*maxwordlen for _ in range(len(text))]
        for j in range(maxwordlen):
            prob[0][j] = self.cPw(text[:j+1])
            strs[0][j] = [text[:j+1]]
        for i in range(1,len(text)):
            for j in range(maxwordlen):
                if i+j+1 > len(text): break
                candidates = [(prob[i-k-1][k] + self.cPw(text[i:i+j+1],strs[i-k-1][k][-1]),
                               strs[i-k-1][k] + [text[i:i+j+1]] ) for k in range(min(i,maxwordlen))]
                prob[i][j], strs[i][j] = max(candidates)
        ends = [(prob[-i-1][i],strs[-i-1][i]) for i in range(min(len(text),maxwordlen))]
        return max(ends)
             */
            int TextLength = Input.Length;
            double[,] Probabilities = new double[TextLength, MAX_WORD_LENGTH];
            List<string>[,] Strings = new List<string>[TextLength, MAX_WORD_LENGTH];

            for(int X = 1; X < TextLength; X++)
            {
                for(int Y = 0; Y < MAX_WORD_LENGTH; Y++)
                {
                    Probabilities[X, Y] = double.NegativeInfinity;
                    Strings[X, Y] = new List<string>();
                }
            }

            for(int Y = 0; Y < MAX_WORD_LENGTH; Y++)
            {
                string Sub = Input.Substring(0, Y + 1);
                Probabilities[0, Y] = ConditionalWordProbability(Sub);
                Strings[0, Y] = new List<string> { Sub };
            }
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
