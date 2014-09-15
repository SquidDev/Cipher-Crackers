using System;
using System.Collections.Generic;

namespace Cipher.Text.WordParser
{
    public class WordGuesser
    {
        const double NO_MATCH_SCORE = -1;
        const double WORD_LENGTH_MULTIPLIER = 0.5;
        const double PREVIOUS_SCORE_MULTIPLIER = 1.5;


        protected FrequencyStorage Frequencies;
        protected ICollection<string> Words;

        protected Dictionary<string, ScoreInstance> ScoreCache = new Dictionary<string, ScoreInstance>();

        protected string SpacedString;
        protected int StringLength;
        

        public ScoreInstance Result { get; protected set; }

        public WordGuesser(string ToGuess, FrequencyStorage Frequencies)
        {
            this.Frequencies = Frequencies;
            Words = Frequencies.Keys;

            SpacedString = ToGuess.ToUpper();
            StringLength = SpacedString.Length;

            Result = GuessAndScore();
        }

        protected ScoreInstance GuessAndScore(int Position = 0, IFrequencyDistribution Current = null, bool UsePrevious = true)
        {
            if (Position >= StringLength)
            {
                return new ScoreInstance("", 0);
            }

            ScoreInstance Result;
            string Key;
            if (Current == null)
            {
                Key = "-1";
            }
            else 
            {
                Key = Position.ToString() + "|" + Current.Word;
            }
            

            if (ScoreCache.TryGetValue(Key, out Result))
            {
                return Result;
            }

            IFrequencyDistribution Previous = UsePrevious ? Current : null;

            double BestScore = Double.NegativeInfinity;

            string Substring = SpacedString.Substring(Position);
            foreach (string Word in Words)
            {
                if (Substring.StartsWith(Word))
                {
                    IFrequencyDistribution Match = Frequencies[Word];
                    ScoreInstance ThisResult = GuessAndScore(Position + Word.Length, Match)
                        .CreateChild(Word, ScoreSingle(Match, Previous));

                    double ThisScore = ThisResult.Score / ThisResult.Count;

                    if (ThisScore > BestScore)
                    {
                        BestScore = ThisScore;
                        Result = ThisResult;
                    }
                }
            }

            // If there were no matches
            if (Result == null)
            {
                string Match = SpacedString[Position].ToString();
                Result = GuessAndScore(Position + 1, new BasicFrequencyDistribution(Match), false)
                    .CreateChild(Match, NO_MATCH_SCORE);
            }

            ScoreCache[Key] = Result;
            return Result;
        }

        protected double ScoreSingle(IFrequencyDistribution Match, IFrequencyDistribution Previous)
        {
            double PreviousScore = Previous == null ? 0 : Previous.GetFollowing(Match.Word);
            return Match.N + (PreviousScore * PREVIOUS_SCORE_MULTIPLIER) + (Match.Word.Length * WORD_LENGTH_MULTIPLIER);
        }

    }
}
