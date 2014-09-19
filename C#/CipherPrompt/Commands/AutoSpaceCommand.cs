using Cipher.Analysis.AutoSpace;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cipher.Prompt.Commands
{
    public class AutoSpaceCommand : ICommand
    {
        public AutoSpaceCommand()
        {
            Name = "space";
            Description = "Add spaces automagically";
        }
        public override void Run(IEnumerable<string> Args, TextReader Input, TextWriter Output)
        {
            bool ShowHelp = false;

            OptionSet Options = new OptionSet()
            {
                {"h|?|help", "Show this message and exit", V => ShowHelp = (V != null)},
            };

            IEnumerable<string> Extra = Options.Parse(Args);

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

            
            WordGuesser Guesser = new WordGuesser(Input.ReadToEnd());
            Console.WriteLine("Score {0}", Guesser.Score);
            Output.WriteLine(String.Join(" ", Guesser.Result));
        }
    }
}
