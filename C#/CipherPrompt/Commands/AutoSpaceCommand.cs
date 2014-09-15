using Cipher.Text.WordParser;
using Cipher.Text.WordParser.Storage;
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
                Console.WriteLine("Usage: CipherPrompt {0} [OPTIONS] (SAMPLE TEXT)+", Name);
                Console.WriteLine();
                Console.WriteLine(Description);
                Console.WriteLine();
                Console.WriteLine("Available Options:");
                Options.WriteOptionDescriptions(Console.Out);
                Console.WriteLine();
                Console.WriteLine("Sample texts:");
                Console.WriteLine("\tTexts are loaded from a directory or file");
                Console.WriteLine("\tXML files will be processed as dictionaries");

                return;
            }

            // Lazy Init variables
            GuessLoader Loader = new GuessLoader(Extra);
            if (Loader.Count <= 0)
            {
                Console.WriteLine("Must include sample texts");
                Console.WriteLine("Run `CipherPrompt help` and CipherPrompt help {0}` for more info", Name);
                return;
            }


            // Load text sources
            Loader.Load();

            WordGuesser Guesser = new WordGuesser(Input.ReadToEnd(), Loader.Frequencies);
            Output.WriteLine(String.Join(" ", Guesser.Result));
        }
    }
}
