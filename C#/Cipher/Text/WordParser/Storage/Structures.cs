using System;
namespace Cipher.Text.WordParser.Storage
{
    /// <summary>
    /// Basic Dictionary entry
    /// </summary>
    public class DictionaryStorage
    {
        public DictionaryItem[] Words = new DictionaryItem[0];
        public string[] Remove = new string[0];

        /// <summary>
        /// Adds and removes words from the <see cref="FrequencyStorage"/>
        /// </summary>
        /// <param name="Frequency">Storage to work with</param>
        /// <param name="DefaultScore">Default score of words to add</param>
        public void Process(FrequencyStorage Frequency, double DefaultScore = 0)
        {
            foreach (DictionaryItem Item in Words)
            {
                Item.Process(Frequency, DefaultScore);
            }

            foreach (string Word in Remove)
            {
                Frequency.Remove(Word);
            }
        }
    }

    /// <summary>
    /// A scored word in a dictionary
    /// </summary>
    public class WordItem
    {
        public const double MinScore = double.NegativeInfinity;

        public string Word;
        public double Score = MinScore;
        public double DefaultScore = MinScore;

        /// <summary>
        /// Adds this elements to the <see cref="FrequencyStorage"/>
        /// </summary>
        /// <param name="Frequency">Storage to add to</param>
        /// <param name="BasicScore">Default score of items when creating them.</param>
        public void Process(IFrequencyDistribution Distribution, double BasicScore = 0)
        {
            if (String.IsNullOrWhiteSpace(Word)) return;

            // Normalise word
            Word = Word.ToUpper();

            // If we have a score...
            if (Distribution.GetFollowing(Word) > 0)
            {
                // Overwrite the score
                if (Score > MinScore)
                {
                    Distribution.Score(Word, Score);
                }
            }
            else if (DefaultScore > MinScore)
            {
                // Try to set it to the default score...
                Distribution.Score(Word, DefaultScore);
            }
            else if (Score > MinScore)
            {
                /// ... and then the set it to the normal score if not
                Distribution.Score(Word, DefaultScore);
            }
            else
            {
                Distribution.Score(Word, BasicScore);
            }
        }
    }

    /// <summary>
    /// A scored word with following words
    /// </summary>
    public class DictionaryItem : WordItem
    {
        public WordItem[] Following = new WordItem[0];

        /// <summary>
        /// Adds this elements to the <see cref="FrequencyStorage"/>
        /// </summary>
        /// <param name="Frequency">Storage to add to</param>
        /// <param name="BasicScore">Default score of items when creating them.</param>
        public void Process(FrequencyStorage Frequency, double BasicScore = 0)
        {
            if (String.IsNullOrWhiteSpace(Word)) return;

            // Normalise word
            Word = Word.ToUpper();

            IFrequencyDistribution Distribution;
            if (Frequency.TryGetValue(Word, out Distribution))
            {
                if (Score > MinScore)
                {
                    Distribution.N = Score;
                }
            }
            else
            {
                // If no frequency exists create it
                Distribution = Frequency.GetDefault(Word);

                // Try to set it to the default score and then the score if not
                if (DefaultScore > MinScore)
                {
                    Distribution.N = DefaultScore;
                }
                else if (Score > MinScore)
                {
                    Distribution.N = Score;
                }
                else
                {
                    Distribution.N = BasicScore;
                }

                Frequency[Word] = Distribution;
            }

            foreach (WordItem Item in Following)
            {
                Item.Process(Distribution, DefaultScore);
            }
        }
    }
}
