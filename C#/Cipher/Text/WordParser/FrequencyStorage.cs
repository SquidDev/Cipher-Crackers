using Cipher.Utils;
using System.Text.RegularExpressions;

namespace Cipher.Text.WordParser
{
    /// <summary>
    /// Parses text and loads words and their frequencies
    /// </summary>
    public class FrequencyStorage : DefaultDict<string, IFrequencyDistribution>
    {
        /// <summary>
        /// The regex used for extracting words
        /// </summary>
        protected static Regex WordRegex = new Regex(@"\w+|[^\w\s]+");

        /// <summary>
        /// Parse a string and extract words
        /// </summary>
        /// <param name="Text">The text to use</param>
        public void ReadText(string Text)
        {
            IFrequencyDistribution Previous = null;
            foreach(Match Word in WordRegex.Matches(Text.ToUpperInvariant()))
            {
                IFrequencyDistribution This = GetOrDefault(Word.Value);
                if(Previous != null)
                {
                    Previous.Increment(This.Word);
                }

                Previous = This;
            }
        }

        /// <summary>
        /// Recalculate all frequencies so they use a decimal between 0 and 1 (percentage rather than count)
        /// </summary>
        /// <remarks>Should be called after loading text and not before</remarks>
        public void Normalise()
        {
            double Total = 0;
            foreach (IFrequencyDistribution Value in Values)
            {
                Total += Value.N;
            }

            foreach (IFrequencyDistribution Value in Values)
            {
                Value.Normalise(Total);
            }
        }

        #region Overrides
        public override IFrequencyDistribution GetDefault(string Key)
        {
            return new FrequencyDistribution(Key);
        }
        #endregion
    }
}
