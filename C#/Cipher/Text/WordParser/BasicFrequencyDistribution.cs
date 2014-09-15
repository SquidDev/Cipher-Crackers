using System;

namespace Cipher.Text.WordParser
{
    public class BasicFrequencyDistribution : IFrequencyDistribution
    {
        /// <summary>
        /// The Word this <see cref="FrequencyDistribution"/> stores
        /// </summary>
        public string Word { get; protected set; }

        /// <summary>
        /// Number of words this <see cref="FrequencyDistribution"/> stores
        /// </summary>
        public double N { get; set; }


        public BasicFrequencyDistribution(string Word)
        {
            this.Word = Word;
        }

        #region IFrequencyDistribution Members
        public void Normalise(double TotalWords) { }
        public void Increment(string Word, bool AddN = true) { }
        public void Score(string Word, double Score) { }

        public double GetFollowing(FrequencyDistribution Word)
        {
            return 0;
        }

        public double GetFollowing(string Word)
        {
            return 0;
        }
        #endregion
    }
}
