using System;
using System.IO;

namespace Cipher.Prompt
{
    /// <summary>
    /// Wrapper classs for TextReaders
    /// Need Ctrl+Z to end file in stdin so we just read a line like normal
    /// people do.
    /// </summary>
    public class StdInWrapper : TextReader
    {
        public TextReader Reader;
        public StdInWrapper()
            : this(Console.In)
        {
        }
        public StdInWrapper(TextReader Reader)
        {
            this.Reader = Reader;
        }

        public override string ReadToEnd()
        {
            return ReadLine();
        }

        public override string ReadLine()
        {
            return Reader.ReadLine();
        }
    }
}
