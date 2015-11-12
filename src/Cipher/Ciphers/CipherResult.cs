using System;

namespace Cipher
{
    public sealed class CipherResult<TKey, TText> : ICipherResult<TKey, TText>
    {
        private readonly double score;
        private readonly TKey key;
        private readonly TText text;

        private readonly Formatter<TKey> keyFormatter;

        public double Score { get { return score; } }

        public TKey Key { get { return key; } }

        public TText Text { get { return text; } }

        public CipherResult(TKey key, TText text, double score, Formatter<TKey> formatter = x => x.ToString())
        {
            this.key = key;
            this.text = text;
            this.score = score;
            this.keyFormatter = keyFormatter;
        }

        public CipherResult(TKey key, TText text, TextScorer<TText> scorer, Formatter<TKey> formatter = x => x.ToString())
        {
            this.key = key;
            this.text = text;
            this.score = scorer(text);
            this.keyFormatter = keyFormatter;
        }

        public static explicit operator CipherResult<string, string>(CipherResult<TKey, TText> result)
        {
            return new CipherResult<string, string>(keyFormatter(result), result.Text.ToString(), result.Score);
        }
    }
}

