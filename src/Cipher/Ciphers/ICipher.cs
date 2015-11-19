using System;

namespace Cipher.Ciphers
{
    public interface ICipher<TKey, TText>
        where TText : ITextArray
    {
        TText Decode(TText cipher, TKey key);

        ICipherResult<TKey, TText> Crack(TText cipher);
    }

    public interface ICipherResult<TKey, TText>
        where TText : ITextArray
    {
        double Score { get; }

        TKey Key { get; }

        TText Contents { get; }
    }
    
    public interface IInvertableCipher<TKey, TText> : ICipher<TKey, TText>
    	where TText : ITextArray
    {
    	TKey Invert(TKey key);
    }
}

