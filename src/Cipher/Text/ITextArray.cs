using System;
using System.Collections.Generic;

namespace Cipher
{
    public interface ITextArray : IReadOnlyList<byte>
    {
        void Initalise(string text);

        void Initalise(int length);
    }

    public interface ITextArray<T> : ITextArray
    {
        new T this [int offset] { get; set; }
        
        void Initalise(IReadOnlyList<T> text);
    }

    public delegate double TextScorer(ITextArray array);
}

