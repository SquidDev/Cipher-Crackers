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
        protected readonly TextScorer scorer;

        public BaseCipher(TextScorer scorer)
        {
            this.scorer = scorer;
        }

        public abstract TText Decode(TText cipher, TKey key, TText decoded);

        public abstract ICipherResult<TKey, TText> Crack(TText cipher);

        protected abstract TText Create(string text);

        protected abstract TText Create(int length);

        #region Shortcut functions

        protected ICipherResult<TKey, TText> GetResult(TText cipher, double score, TKey key, TText decoded)
        {
            return new CipherResult<TKey, TText>(key, Decode(cipher, key, decoded), score);
        }

        protected ICipherResult<TKey, TText> GetResult(TText cipher, double score, TKey key)
        {
            return new CipherResult<TKey, TText>(key, Decode(cipher, key), scorer);
        }

        protected ICipherResult<TKey, TText> GetResult(TText cipher, TKey key)
        {
            return new CipherResult<TKey, TText>(key, Decode(cipher, key), scorer);
        }

        public TText Decode(TText cipher, TKey key)
        {
            return Decode(cipher, key, Create(cipher.Count));
        }
        
        public TText Decode(string cipher, TKey key)
        {
        	return Decode(Create(cipher), key);
        }
        
        public ICipherResult<TKey, TText> Crack(string cipher)
        {
        	return Crack(Create(cipher));
        }

        #endregion
    }
    
    public abstract class DefaultCipher<TKey, TText> : BaseCipher<TKey, TText>
    	where TText : ITextArray, new()
    {
    	public DefaultCipher(TextScorer scorer)
            : base(scorer)
        {
        }
    	

		protected override TText Create(int length)
		{
			TText text = new TText();
			text.Initalise(length);
			return text;
		}
    	
		protected override TText Create(string text)
		{
			TText array = new TText();
			array.Initalise(text);
			return array;
		}
    }
}
