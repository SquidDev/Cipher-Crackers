using System.Collections;
using System.Collections.Generic;

namespace Cipher.Text.WordParser
{
    public class ScoreInstance : IEnumerable<string>
    {
        public ScoreInstance Previous { get; protected set; }
        public string Word { get; protected set; }
        public double Score { get; set; }

        public int Count { get; protected set; }

        public ScoreInstance(string Word, double Score, ScoreInstance Previous = null)
        {
            this.Word = Word;
            this.Score = Score;
            this.Previous = Previous;

            if (Previous == null)
            {
                Count = 1;
            }
            else
            {
                Count = Previous.Count + 1;
            }
        }

        public ScoreInstance CreateChild(string Word, double Score = 0)
        {
            return new ScoreInstance(Word, this.Score + Score, this);
        }

        #region IEnumerator Methods
        public IEnumerator<string> GetEnumerator()
        {
            ScoreInstance Instance = this;
            while (Instance != null)
            {
                yield return Instance.Word;
                Instance = Instance.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
