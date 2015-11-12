using Cipher.Analysis.AutoSpace;
using Cipher.Analysis.CipherGuess;
using Cipher.Ciphers;
using Cipher.Prompt.Commands;
using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LArray = Cipher.Text.LetterTextArray;
using QSLArray = Cipher.Text.QuadgramScoredLetterArray;
using QSCArray = Cipher.Text.QuadgramScoredCharacterArray;

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
            catch (Exception e)
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
                    Decode = (Enc, Key) => new Substitution<QSLArray>(Enc).Decode(new ByteTextArray(Key)).ToString(),
                    Crack = (Enc) => (GenericCipherResult)new Substitution<QSLArray>(Enc).Crack(),
                });

            AddCommand(new CipherCommand()
                {
                    Name = "vigenere",
                    Description = "Crack the vigenere cipher",
                    Decode = (Enc, Key) => new MonogramVigenere(Enc).Decode(new ByteTextArray(Key)).ToString(),
                    Crack = (Enc) => (GenericCipherResult)new MonogramVigenere(Enc).Crack(),
                });

            AddCommand(new CipherCommand()
                {
                    Name = "railfence",
                    Description = "Decode/crack the railfence cipher",
                    Decode = (Enc, Key) => new RailFence<QSCArray, char>(Enc).Decode(Convert.ToInt32(Key)).ToString(),
                    Crack = (Enc) => (GenericCipherResult)new RailFence<QSCArray, char>(Enc).Crack(),
                });

            AddCommand(new CipherCommand()
                {
                    Name = "transposition",
                    Description = "Decode/crack the columnar transposition cipher (separate key with ';')",
                    Decode = (Enc, Key) => new ColumnarTransposition<QSCArray, char>(Enc).Decode(Key.Split(';').Select(C => Convert.ToByte(C)).ToArray()).ToString(),
                    Crack = (Enc) => (GenericCipherResult)new ColumnarTransposition<QSCArray, char>(Enc).Crack(),
                });

            AddCommand(new NGramCommand());
            AddCommand(new InlineCommand()
                {
                    Name = "space",
                    Description = "Add spaces automagically",
                    Command = delegate(List<string> Extra, TextReader Input, TextWriter Output)
                    {
                        WordGuesser Guesser = new WordGuesser(Input.ReadToEnd());
                        Console.WriteLine("Score {0}", Guesser.Score);
                        Output.WriteLine(String.Join(" ", Guesser.Result));
                    }
                });

            AddCommand(new InlineCommand()
                {
                    Name = "guess",
                    Description = "Show the deviations from each type of cipher, and so deduce the most likely cipher",
                    Command = delegate(List<string> Extra, TextReader Input, TextWriter Output)
                    {
                        CipherAnalysis Analysis = new CipherAnalysis(Input.ReadToEnd());
                        foreach (KeyValuePair<CipherType, double> Cipher in Analysis.Deviations.OrderBy(KV => KV.Value))
                        {
                            Output.WriteLine("{0,-20} {1}", Cipher.Key.Name, Cipher.Value);
                        }
                    }
                });

            AddCommand(new InlineCommand()
                {
                    Name = "text-data",
                    Description = "Calculate some common properties about the text",
                    Command = delegate(List<string> Extra, TextReader Input, TextWriter Output)
                    {
                        CipherAnalysis Analysis = new CipherAnalysis(Input.ReadToEnd());
                        Output.WriteLine("{0,-4} {1}", "IC", Analysis.TextData.IC);
                        Output.WriteLine("{0,-4} {1}", "MIC", Analysis.TextData.MIC);
                        Output.WriteLine("{0,-4} {1}", "MKA", Analysis.TextData.MKA);
                        Output.WriteLine("{0,-4} {1}", "DIC", Analysis.TextData.DIC);
                        Output.WriteLine("{0,-4} {1}", "EDI", Analysis.TextData.EDI);
                        Output.WriteLine("{0,-4} {1}", "LR", Analysis.TextData.LR);
                        Output.WriteLine("{0,-4} {1}", "ROD", Analysis.TextData.ROD);
                        Output.WriteLine("{0,-4} {1}", "LDI", Analysis.TextData.LDI);
                        Output.WriteLine("{0,-4} {1}", "SDD", Analysis.TextData.SDD);
                    }
                });

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
                { "i|input-file=", "The {PATH} of the cipher text to decode", F => InputFile = F },
                { "o|output-file=", "The {PATH} of where to save the plaintext to", F => OutputFile = F },
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
