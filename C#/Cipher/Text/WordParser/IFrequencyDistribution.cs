namespace Cipher.Text.WordParser
{
    /// <summary>
    /// Used for scoring words and following words
    /// </summary>
    public interface IFrequencyDistribution
    {
        /// <summary>
        /// The Word this <see cref="FrequencyDistribution"/> stores
        /// </summary>
        string Word { get; }

        /// <summary>
        /// Number of words this <see cref="FrequencyDistribution"/> stores
        /// </summary>
        double N { get; set; }

        /// <summary>
        /// Converts every 'score' into a decimal between 0 and 1
        /// Should only ever be called once. 
        /// </summary>
        /// <param name="TotalWords">The total number of words analysed</param>
        void Normalise(double TotalWords);

        /// <summary>
        /// Increment a word
        /// </summary>
        /// <param name="Word">Word to incremenet</param>
        /// <param name="AddN">Add to N</param>
        void Increment(string Word, bool AddN = true);

        /// <summary>
        /// Score a word
        /// </summary>
        /// <param name="Word">Word to score</param>
        void Score(string Word, double Score);

        /// <summary>
        /// Gets the score of the following word
        /// </summary>
        /// <param name="Word">The word to get the score of</param>
        /// <returns>The score of the word or 0 if it does not follow</returns>
        double GetFollowing(FrequencyDistribution Word);

        /// <summary>
        /// Gets the score of the following word
        /// </summary>
        /// <param name="Word">The word to get the score of</param>
        /// <returns>The score of the word or 0 if it does not follow</returns>
        double GetFollowing(string Word);

        
    }
}
