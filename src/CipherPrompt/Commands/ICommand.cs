using System.Collections.Generic;
using System.IO;

namespace Cipher.Prompt.Commands
{
    public abstract class ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract void Run(IEnumerable<string> args, TextReader reader, TextWriter writer);
    }
}
