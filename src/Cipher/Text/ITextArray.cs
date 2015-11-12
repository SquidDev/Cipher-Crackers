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
    }

    public delegate double TextScorer(ITextArray array);
    public delegate double TextScorer<T>(T array) where T : ITextArray;
}

