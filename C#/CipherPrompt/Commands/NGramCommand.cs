using Cipher.Analysis;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cipher.Prompt.Commands
{
    public class NGramCommand : ICommand
    {
        public NGramCommand()
        {
            Name = "ngrams";
            Description = "Calculate the frequencies of N-Grams";
        }

        public override void Run(IEnumerable<string> Args, TextReader Reader, TextWriter Writer)
        {
            bool ShowHelp = false;
            int Length = 2;
            int Limit = 20;

            OptionSet Options = new OptionSet()
            {
                { "h|?|help", "Show this message and exit", V => ShowHelp = (V != null) },
                { "l|length=", "{LENGTH} of the bigrams", V => Length = Convert.ToInt32(V) },
                { "m|limit=", "{MAXIMUM} numer to show (use -1 for no limit)", V => Limit = Convert.ToInt32(V) },
            };

            Options.Parse(Args);

            if (ShowHelp)
            {
                Console.WriteLine("Usage: CipherPrompt {0} [OPTIONS] (SAMPLE TEXT)+", Name);
                Console.WriteLine();
                Console.WriteLine(Description);
                Console.WriteLine();
                Console.WriteLine("Available Options:");
                Options.WriteOptionDescriptions(Console.Out);

                return;
            }

            IEnumerable<KeyValuePair<string, int>> Values = NGrams.GatherNGrams(Reader.ReadToEnd(), Length)
                .OrderByDescending(A => A.Value);

            if (Limit > 0)
            {
                Values = Values.Take(Limit);
            }

            foreach (KeyValuePair<string, int> Value in Values)
            {
                Writer.WriteLine("{0}\t{1}", Value.Key, Value.Value);
            }
        }
    }
}
