using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cipher.Prompt.Commands
{
    public class InlineCommand : ICommand
    {
        public Action<List<string>, TextReader, TextWriter> Command { get; set; }

        public override void Run(IEnumerable<string> Args, TextReader Reader, TextWriter Writer)
        {
            bool ShowHelp = false;

            OptionSet Options = new OptionSet()
            {
                {"h|?|help", "Show this message and exit", V => ShowHelp = (V != null)},
            };

            List<string> Extra = Options.Parse(Args);

            if (ShowHelp)
            {
                Console.WriteLine("Usage: CipherPrompt {0} [OPTIONS]", Name);
                Console.WriteLine();
                Console.WriteLine(Description);
                Console.WriteLine();
                Console.WriteLine("Available Options:");
                Options.WriteOptionDescriptions(Console.Out);

                return;
            }

            Command(Extra, Reader, Writer);
        }
    }
}
