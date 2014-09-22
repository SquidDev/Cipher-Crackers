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
    public abstract class BaseCipher<TKey, TArray, TArrayType>
        where TArray : TextArray<TArrayType>, new()
    {
        protected TArray Text;

        // Conversion function for custom keys
        protected Func<TKey, string> KeyStringify = K => K.ToString();

        public BaseCipher(TArray CipherText)
        {
            Text = CipherText;
        }

        public BaseCipher(string CipherText)
        {
            Text = new TArray();
            Text.Initalise(CipherText);
        }

        public abstract TArray Decode(TKey Key, TArray Decoded);
        public TArray Decode(TKey Key)
        {
            TArray Decoded = new TArray();
            Decoded.Initalise(Text.Length);

            return Decode(Key, Decoded);
        }
        public abstract CipherResult Crack();




        #region TArray shortcuts
        protected TArray Create(string Text)
        {
            TArray Arr = new TArray();
            Arr.Initalise(Text);
            return Arr;
        }

        protected TArray Create(int Length)
        {
            TArray Arr = new TArray();
            Arr.Initalise(Length);
            return Arr;
        }
        #endregion

        protected CipherResult GetResult(double Score, TKey Key, TArray Decoded)
        {
            return new CipherResult(Decode(Key, Decoded), Score, Key, KeyStringify);
        }

        protected CipherResult GetResult(double Score, TKey Key)
        {
            return new CipherResult(Decode(Key), Score, Key, KeyStringify);
        }

        /// <summary>
        /// The result of decryption
        /// </summary>
        public class CipherResult
        {
            public readonly TArray Text;
            public readonly double Score;
            public readonly TKey Key;

            protected Func<TKey, string> KeyStringify;

            public CipherResult(TArray Text, double Score, TKey Key)
                : this(Text, Score, Key, K => K.ToString())
            { }

            public CipherResult(TArray Text, double Score, TKey Key, Func<TKey, string> KeyStringify)
            {
                this.Text = Text;
                this.Score = Score;
                this.Key = Key;

                this.KeyStringify = KeyStringify;
            }

            public override string ToString()
            {
                return Text.ToString();
            }

            public string KeyString()
            {
                return KeyStringify(Key);
            }

            public static explicit operator GenericCipherResult(CipherResult Result)
            {
                return new GenericCipherResult(Result.KeyString(), Result.Text.ToString(), Result.Score);
            }
        }
    }

    public class GenericCipherResult
    {
        public readonly string Key;
        public readonly string Text;
        public readonly double Score;

        public GenericCipherResult(string Key, string Text, double Score)
        {
            this.Key = Key;
            this.Score = Score;
            this.Text = Text;
        }
    }
}