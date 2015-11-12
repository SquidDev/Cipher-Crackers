using Cipher.Text;
using System;

namespace Cipher.Ciphers
{
    /// <summary>
    /// Base class for ciphers
    /// </summary>
    /// <typeparam name="TKey">The type of key</typeparam>
    /// <typeparam name="TArray">The type of text array</typeparam>
    /// <typeparam name="TArrayType">The type the text array contains</typeparam>
    public abstract class BaseCipher<TKey, TText> : ICipher<TKey, TText>
        where TText : ITextArray
    {
        protected readonly Formatter<TKey> keyFormatter;
        protected readonly TextScorer scorer;

        public BaseCipher(TextScorer scorer, Formatter<TKey> formatter = x => x.ToString())
        {
            this.keyFormatter = formatter;
            this.scorer = scorer;
        }

        public abstract TText Decode(TText cipher, TKey key, TText decoded);

        public abstract ICipherResult<TKey, TText> Crack(TText cipher);

        protected abstract TText Create(string text);

        protected abstract TText Create(int length);

        #region Shortcut functions

        protected ICipherResult<TKey, TText> GetResult(double score, TKey key, TText decoded)
        {
            return new CipherResult<TKey, TText>(Decode(key, decoded), key, score, keyFormatter);
        }

        protected ICipherResult<TKey, TText> GetResult(double score, TKey key)
        {
            return new CipherResult<TKey, TText>(Decode(key), key, score, keyFormatter);
        }

        protected ICipherResult<TKey, TText> GetResult(TKey key)
        {
            TText decoded = Decode(key);
            return new CipherResult<TKey, TText>(decoded, key, scorer, keyFormatter);
        }

        public virtual TText Decode(TText cipher, TKey key)
        {
            return Decode(cipher, key, Create(cipher.Count));
        }

        #endregion
    }
}
