using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cipher.Prompt.Commands
{
    public class InlineCommand : ICommand
    {
        public Action<IEnumerable<string>, TextReader, TextWriter> Command;

        public override void Run(IEnumerable<string> Args, TextReader Reader, TextWriter Writer)
        {
            Command(Args, Reader, Writer);
        }
    }
}
