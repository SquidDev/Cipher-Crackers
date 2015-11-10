using Cipher.Ciphers;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cipher.Prompt.Commands
{
    public class CipherCommand : ICommand
    {
        public Func<string, GenericCipherResult> Crack;
        public Func<string, string, string> Decode;

        public override void Run(IEnumerable<string> Args, TextReader Reader, TextWriter Writer)
        {
            bool ShowHelp = false;
            string Key = null;

            OptionSet Options = new OptionSet()
            {
                { "h|?|help", "Show this message and exit", V => ShowHelp = (V != null) },
                { "k|key=", "{KEY} of the cipher", V => Key = V },
            };

            IList<string> Extra = Options.Parse(Args);

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

            if (Extra.Count > 0)
            {
                Console.WriteLine("Unregognised options: {0}", String.Join(", ", Extra));
                Console.WriteLine("Run `CipherPrompt help` and CipherPrompt help {0}` for more info", Name);
            }

            string Cipher = Reader.ReadToEnd();
            string Plaintext;
            if (String.IsNullOrWhiteSpace(Key))
            {
                GenericCipherResult Result = Crack(Cipher);
                Console.WriteLine("Key:   {0}", Result.Key);
                Console.WriteLine("Score: {0}", Result.Score);
                Plaintext = Result.Text;
            }
            else
            {
                Plaintext = Decode(Cipher, Key);
            }

            Writer.WriteLine(Plaintext);
        }
    }
}
