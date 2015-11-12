using Cipher.Ciphers;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cipher.Prompt.Commands
{
    public class CipherCommand<TKey, TText> : ICommand
    	where TText : ITextArray
    {
    	public BaseCipher<TKey, TText> Cipher;
        public IKeyConverter<TKey> Converter;

        public override void Run(IEnumerable<string> args, TextReader reader, TextWriter writer)
        {
            bool showHelp = false;
            string key = null;

            OptionSet Options = new OptionSet()
            {
                { "h|?|help", "Show this message and exit", V => showHelp = (V != null) },
                { "k|key=", "{KEY} of the cipher", V => key = V },
            };

            IList<string> extra = Options.Parse(args);

            if (showHelp)
            {
                Console.WriteLine("Usage: CipherPrompt {0} [OPTIONS]", Name);
                Console.WriteLine();
                Console.WriteLine(Description);
                Console.WriteLine();
                Console.WriteLine("Available Options:");
                Options.WriteOptionDescriptions(Console.Out);

                return;
            }

            if (extra.Count > 0)
            {
                Console.WriteLine("Unregognised options: {0}", String.Join(", ", extra));
                Console.WriteLine("Run `CipherPrompt help` and CipherPrompt help {0}` for more info", Name);
            }

            string cipher = reader.ReadToEnd();
            string plaintext;
            if (String.IsNullOrWhiteSpace(key))
            {
                ICipherResult<TKey, TText> result = Cipher.Crack(cipher);
                Console.WriteLine("Key:   {0}", Converter.ToString(result.Key));
                Console.WriteLine("Score: {0}", result.Score);
                plaintext = result.Contents.ToString();
            }
            else
            {
            	plaintext = Cipher.Decode(cipher, Converter.FromString(key)).ToString();
            }

            writer.WriteLine(plaintext);
        }
    }
}
