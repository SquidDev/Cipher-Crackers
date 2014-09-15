using Cipher.Utils;
using System.Collections.Generic;

namespace Cipher.Text.WordParser
{
    /// <summary>
    /// Scores one word and the word following it
    /// </summary>
    public class FrequencyDistribution : Dictionary<string, double>, IFrequencyDistribution
    {
        /// <summary>
        /// The Word this <see cref="FrequencyDistribution"/> stores
        /// </summary>
        public string Word { get; protected set; }

        /// <summary>
        /// Number of words this <see cref="FrequencyDistribution"/> stores
        /// </summary>
        public double N { get; set; }

        #region Constructors
        protected internal FrequencyDistribution() : base() { }

        /// <summary>
        /// Create a <see cref="FrequencyDistribution"/>
        /// </summary>
        /// <param name="Word">The word to use</param>
        public FrequencyDistribution(string Word) : base()
        {
            this.Word = Word;
        }

        /// <summary>
        /// Create a <see cref="FrequencyDistribution"/>
        /// </summary>
        /// <param name="Word">The word to use</param>
        /// <param name="Frequencies">The frequencies of following letters</param>
        public FrequencyDistribution(string Word, Dictionary<string, double> Frequencies) : base(Frequencies)
        {
            this.Word = Word;
            CalculateN();
        }
        #endregion
        #region Utilities
        /// <summary>
        /// Calcuates N, used when first setting the Frequencies
        /// </summary>
        protected internal void CalculateN()
        {
            double _N = 0;
            foreach (double Value in Values)
            {
                _N += Value;
            }
            N = _N;
        }

        /// <summary>
        /// Converts every 'score' into a decimal between 0 and 1
        /// Should only ever be called once. 
        /// </summary>
        /// <param name="TotalWords">The total number of words analysed</param>
        public void Normalise(double TotalWords)
        {
            double N = this.N;
            foreach (string Key in new List<string>(Keys))
            {
                this[Key] /= N;
            }
            this.N = N / TotalWords;
        }
        #endregion
        #region Access
        /// <summary>
        /// Increment a word
        /// </summary>
        /// <param name="Word">Word to incremenet</param>
        public void Increment(string Word, bool AddN = true)
        {
            double Current;
            TryGetValue(Word, out Current);
            this[Word] = Current + 1;
            
            if(AddN) N += 1;
        }

        /// <summary>
        /// Score a word
        /// </summary>
        /// <param name="Word">Word to score</param>
        public void Score(string Word, double Score)
        {
            this[Word] = Score;
        }

        /// <summary>
        /// Gets the score of the following word
        /// </summary>
        /// <param name="Word">The word to get the score of</param>
        /// <returns>The score of the word or 0 if it does not follow</returns>
        public double GetFollowing(string Word)
        {
            double Result;
            TryGetValue(Word, out Result);
            return Result;
        }

        /// <summary>
        /// Gets teh score of the following word
        /// </summary>
        /// <param name="Word">The word to get the score of</param>
        /// <returns>The score of the word or 0 if it does not follow</returns>
        public double GetFollowing(FrequencyDistribution Word)
        {
            return GetFollowing(Word.Word);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return Word ?? "<Word>";
        }
        #endregion
    }
}
