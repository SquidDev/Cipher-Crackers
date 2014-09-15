using Cipher.Ciphers;
using Cipher.Prompt.Commands;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LArray = Cipher.Text.LetterArray;
using QSLArray = Cipher.Text.QuadgramScoredLetterArray;

namespace Cipher.Prompt
{
    public class CipherPrompt
    {
        #region Static Code

        public static void Main(string[] Args)
        {
            CipherPrompt Prompt = new CipherPrompt();
#if DEBUG
            Prompt.Run(Args);
#else
            try
            {
                Prompt.Run(Args);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("CipherPrompt: {0}", e.Message);
                Console.Error.WriteLine("See `CipherPrompt help` for more information");
                Environment.ExitCode = 1;
            }
#endif
        }

        #endregion

        #region Commands
        public Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

        public CipherPrompt()
        {
            AddCommand(new CipherCommand()
            {
                Name = "caeser",
                Description = "Decode/crack the caeser shift cipher",
                Decode = (Enc, Key) => new CaeserShift<QSLArray>(Enc).Decode(Convert.ToByte(Key)).ToString(),
                Crack = (Enc) => (GenericCipherResult)new CaeserShift<QSLArray>(Enc).Crack(),
            });

            AddCommand(new CipherCommand()
            {
                Name = "substitution",
                Description = "Crack the substitution cipher",
                Decode = (Enc, Key) => new Substitution<QSLArray>(Enc).Decode(new LArray(Key)).ToString(),
                Crack = (Enc) => (GenericCipherResult)new Substitution<QSLArray>(Enc).Crack(),
            });

            AddCommand(new CipherCommand()
            {
                Name = "vigenere",
                Description = "Crack the vigenere cipher",
                Decode = (Enc, Key) => new MonogramVigenere(Enc).Decode(new LArray(Key)).ToString(),
                Crack = (Enc) => (GenericCipherResult)new MonogramVigenere(Enc).Crack(),
            });

            AddCommand(new AutoSpaceCommand());

            AddCommand(new HelpCommand(this));
        }

        public void AddCommand(ICommand Command)
        {
            Commands.Add(Command.Name.ToLower(), Command);
        }

        public ICommand GetCommand(string Command)
        {
            ICommand Result;
            Commands.TryGetValue(Command.ToLower(), out Result);
            return Result;
        }
        #endregion
        public OptionSet GlobalOptions;

        public void Run(string[] Args)
        {
            if (Args.Length == 0)
            {
                Console.WriteLine("Run `CipherPrompt help` for usage");
                return;
            }

            string InputFile = null;
            string OutputFile = null;
            
            GlobalOptions = new OptionSet()
            {
                {"i|input-file=", "The {PATH} of the cipher text to decode", F => InputFile = F},
                {"o|output-file=", "The {PATH} of where to save the plaintext to", F => OutputFile = F},
            };

            List<string> Extra = GlobalOptions.Parse(Args);

            if (Extra.Count == 0)
            {
                Console.WriteLine("Run `CipherPrompt help` for usage");
                return;
            }

            TextReader Input;
            if (String.IsNullOrWhiteSpace(InputFile))
            {
                Input = new StdInWrapper(Console.In);
            }
            else
            {
                Input = new StreamReader(InputFile);
            }

            TextWriter Output;
            if (String.IsNullOrWhiteSpace(OutputFile))
            {
                Output = Console.Out;
            }
            else
            {
                Output = new StreamWriter(OutputFile);
            }

            ICommand Command = GetCommand(Extra[0]);
            if (Command != null)
            {
                Command.Run(Extra.Skip(1), Input, Output);
            }
            else
            {
                Console.WriteLine("Unknown command {0}", Extra[0]);
                Console.WriteLine("Run `CipherPrompt help` for usage");
            }

            // Close streams
            Input.Close();
            Output.Close();
        }  
    }
}
