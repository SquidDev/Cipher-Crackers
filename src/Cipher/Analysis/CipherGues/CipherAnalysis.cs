using Cipher.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cipher.Analysis.CipherGuess
{
    public class CipherAnalysis
    {
        public const byte MAX_PERIOD = 15;
        public const byte LR_COUNT = 11;

        public readonly Dictionary<CipherType, double> Deviations = new Dictionary<CipherType,double>();
        public readonly StatsType TextData;

        public readonly LetterNumberArray Text;

        public CipherAnalysis(string Text)
        {
            this.Text = new LetterNumberArray(Text);

            TextData = new StatsType
            {
                IC = GetIC(),
                MIC = GetMaxPeriodicIC(),
                MKA = GetMaxKappa(),
                DIC = GetDIC(),
                EDI = GetEvenDIC(),
                LR = GetLR(),
                ROD = GetROD(),
                LDI = GetLogDI(),
                SDD = GetSDD(),
            };

            foreach (CipherType Cipher in CipherData.Ciphers)
            {
                double Score = 
                    ScoreCipher(TextData.IC, Cipher.Standard.IC + 0.001, Cipher.Average.IC) +
                    ScoreCipher(TextData.MIC, Cipher.Standard.MIC, Cipher.Average.MIC) +
                    ScoreCipher(TextData.MKA, Cipher.Standard.MKA, Cipher.Average.MKA) +
                    ScoreCipher(TextData.DIC, Cipher.Standard.DIC, Cipher.Average.DIC) +
                    ScoreCipher(TextData.EDI, Cipher.Standard.EDI, Cipher.Average.EDI) +
                    ScoreCipher(TextData.LR, Cipher.Standard.LR, Cipher.Average.LR) +
                    ScoreCipher(TextData.ROD, Cipher.Standard.ROD, Cipher.Average.ROD) +
                    ScoreCipher(TextData.LDI, Cipher.Standard.LDI, Cipher.Average.LDI) +
                    ScoreCipher(TextData.SDD, Cipher.Standard.SDD, Cipher.Average.SDD);

                Deviations.Add(Cipher, Score);
            }
        }

        protected double ScoreCipher(double Value, double Standard, double Average)
        {
            if (Average == 0) return Value;
            return Math.Abs((Value - Average) / Standard);
        }

        #region Calculations
        /// <summary>
        /// Index of Coincidence times 1000
        /// </summary>
        protected double GetIC()
        {
            int Length = Text.Count;
            int[] CT = new int[LetterNumberArray.NUM_SYMBOLS];

            foreach (byte Character in Text.Characters)
            {
                CT[Character]++;
            }

            double Sum = CT.Sum(Value => Value * (Value - 1));
            return (Sum / (Length * (Length - 1))) * 1000;
        }

        /// <summary>
        /// Max IC for periods 1-MAX_PERIOD, times 1000
        /// </summary>
        protected double GetMaxPeriodicIC()
        {
            int Length = Text.Count;
            int Size = MAX_PERIOD * LetterNumberArray.NUM_SYMBOLS;
            int[,] CT = new int[MAX_PERIOD, LetterNumberArray.NUM_SYMBOLS];

            double Max = 0;
            for (byte Period = 1; Period < MAX_PERIOD; Period++)
            {
                // Reset
                Array.Clear(CT, 0, Size);

                int Index = 0;
                foreach (char Character in Text.Characters)
                {
                    CT[Index, Character]++;
                    Index = (Index + 1) % Period;
                }

                double Z = 0;
                for (byte P = 0; P < Period; P++)
                {
                    double X = 0;
                    double Y = 0;

                    for (int J = 0; J < LetterNumberArray.NUM_SYMBOLS; J++)
                    {
                        int Value = CT[P, J];
                        X += Value * (Value - 1);
                        Y += Value;
                    }

                    if (Y > 1) Z += X / (Y * (Y - 1));
                }

                Z /= Period;
                if (Z > Max) Max = Z;
            }

            return 1000 * Max;
        }

        /// <summary>
        /// Max kappa for periods 1-MAX_PERIOD, times 1000
        /// </summary>
        protected double GetMaxKappa()
        {
            double Max = 0;
            int Length = Text.Count;
            for (byte Period = 1; Period < MAX_PERIOD; Period++)
            {
                if (Period >= Length) break;

                double CT = 0;
                int ShortLength = Length - Period;
                for (int I = 0; I < ShortLength; I++)
                {
                    if (Text[I] == Text[I + Period])
                    {
                        CT++;
                    }
                }

                double Z = CT / ShortLength;
                if (Z > Max) Max = Z;
            }

            return Max * 1000;
        }

        /// <summary>
        /// Digraphic Index of Coincidence, times 10000. 
        /// </summary>
        protected double GetDIC()
        {
            int[] CT = new int[LetterNumberArray.NUM_SYMBOLS * LetterNumberArray.NUM_SYMBOLS];
            int Length = Text.Count;

            for (int Pos = 0; Pos < Length - 1; Pos++)
            {
                CT[Text[Pos] + LetterNumberArray.NUM_SYMBOLS * Text[Pos + 1]]++;
            }

            Length--;
            double Sum = CT.Sum(Value => Value * (Value - 1));
            return (Sum / (Length * (Length - 1))) * 10000;
        }

        /// <summary>
        /// DIC for even numbered pairs, times 10000.
        /// </summary>
        protected double GetEvenDIC()
        {
            int[] CT = new int[LetterNumberArray.NUM_SYMBOLS * LetterNumberArray.NUM_SYMBOLS];
            int Length = Text.Count;

            int N = 0;
            for (int Pos = 0; Pos < Length - 1; Pos += 2)
            {
                CT[Text[Pos] + LetterNumberArray.NUM_SYMBOLS * Text[Pos + 1]]++;
                N++;
            }

            double Sum = CT.Sum(Value => Value * (Value - 1));
            return (Sum / (N * (N - 1))) * 10000;
        }

        /// <summary>
        /// Long Repeat (percentage of 3 symbol repeats)
        /// </summary>
        protected double GetLR()
        {
            int[] Reps = new int[LR_COUNT];
            int Length = Text.Count;
            for (int I = 0; I < Length; I++)
            {
                for (int J = I + 1; J < Length; J++)
                {
                    int N = 0;
                    while (J + N < Length && Text[I + N] == Text[J + N])
                    {
                        N++;
                    }

                    if (N > (LR_COUNT - 1)) N = LR_COUNT - 1;
                    Reps[N]++;
                }
            }

            return 1000 * Math.Sqrt(Reps[3]) / Length;
        }

        /// <summary>
        /// Percentage of odd-spaced repeats to all repeats.
        /// </summary>
        /// <remarks>
        /// Could optimise by combining with <see cref="GetLR"/>
        /// </remarks>
        protected double GetROD()
        {
            int SumAll = 0;
            int SumOdd = 0;

            int Length = Text.Count;

            for (int I = 0; I < Length; I++)
            {
                for (int J = I + 1; J < Length; J++)
                {
                    int N = 0;
                    while (J + N < Length && Text[I + N] == Text[J + N])
                    {
                        N++;
                    }

                    if (N > 1)
                    {
                        SumAll++;
                        if (((J - I) & 1) > 0)
                        {
                            SumOdd++;
                        }
                    }
                }
            }

            if (SumAll == 0) return 50;
            return 100 * ((double)SumOdd / SumAll);
        }

        /// <summary>
        /// average English log digraph score
        /// </summary>
        protected double GetLogDI()
        {
            int Length = Text.Count - 1;
            int Score = 0;

            for (int I = 0; I < Length; I++)
            {
                byte A = Text[I];
                byte B = Text[I + 1];
                if (A > 25 || B > 25) continue;

                Score += CipherData.LogDi[A, B];
            }

            return ((double)Score * 100) / Length;
        }

        /// <summary>
        /// Average English single letter - digraph discrepancy score
        /// </summary>
        /// <remarks>
        /// Could optimise by combining with <see cref="GetROD"/>
        /// </remarks>
        protected double GetSDD()
        {
            int Length = Text.Count - 1;
            int Score = 0;

            for (int I = 0; I < Length; I++)
            {
                byte A = Text[I];
                byte B = Text[I + 1];
                if (A > 25 || B > 25) continue;

                Score += CipherData.Sdd[A, B];
            }

            return ((double)Score * 100) / Length;
        }
        #endregion
    }
}
