using System;

namespace Cipher.Ciphers
{
    public sealed class CipherResult<TKey, TText> : ICipherResult<TKey, TText>
    	where TText : ITextArray
    {
        private readonly double score;
        private readonly TKey key;
        private readonly TText text;
        public double Score { get { return score; } }

        public TKey Key { get { return key; } }

        public TText Contents { get { return text; } }

        public CipherResult(TKey key, TText text, double score)
        {
            this.key = key;
            this.text = text;
            this.score = score;
        }

        public CipherResult(TKey key, TText text, TextScorer scorer)
        {
            this.key = key;
            this.text = text;
            this.score = scorer(text);
        }
    }
}

