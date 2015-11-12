using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Cipher.Analysis.AutoSpace;
using Cipher.Analysis.CipherGuess;
using Cipher.Ciphers;
using Cipher.Prompt.Commands;
using Cipher.Text;
using NDesk.Options;
using LArray = Cipher.Text.LetterTextArray;

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
            AddCommand(new CipherCommand<byte, LetterTextArray>()
                {
                    Name = "caeser",
                    Description = "Decode/crack the caeser shift cipher",
                    Converter = KeyConverters.Byte,
                    Cipher = new CaeserShift<LetterTextArray>(TextScorers.ScoreQuadgrams),
                });

        	AddCommand(new CipherCommand<byte[], LetterTextArray>()
                {
                    Name = "substitution",
                    Description = "Crack the substitution cipher",
                    Converter = KeyConverters.String,
                    Cipher = new Substitution<LetterTextArray>(TextScorers.ScoreQuadgrams),
                });

        	AddCommand(new CipherCommand<byte[], LetterTextArray>()
                {
                    Name = "vigenere",
                    Description = "Crack the vigenere cipher",
                    Converter = KeyConverters.String,
                    Cipher = new Vigenere<LetterTextArray>(),
                });

            AddCommand(new CipherCommand<int, CharacterTextArray>()
                {
                    Name = "railfence",
                    Description = "Decode/crack the railfence cipher",
                    Converter = KeyConverters.Integer,
                    Cipher = new RailFence<CharacterTextArray, char>(TextScorers.ScoreQuadgrams),
                });

        	AddCommand(new CipherCommand<byte[], CharacterTextArray>()
                {
                    Name = "transposition",
                    Description = "Decode/crack the columnar transposition cipher (separate key with ';')",
                    Converter = KeyConverters.ByteList,
                    Cipher = new ColumnarTransposition<CharacterTextArray, char>(TextScorers.ScoreQuadgrams),
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

        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Run `CipherPrompt help` for usage");
                return;
            }

            string inputFile = null;
            string outputFile = null;
            
            GlobalOptions = new OptionSet()
            {
                { "i|input-file=", "The {PATH} of the cipher text to decode", F => inputFile = F },
                { "o|output-file=", "The {PATH} of where to save the plaintext to", F => outputFile = F },
            };

            List<string> extra = GlobalOptions.Parse(args);

            if (extra.Count == 0)
            {
                Console.WriteLine("Run `CipherPrompt help` for usage");
                return;
            }

            using(TextReader input = String.IsNullOrWhiteSpace(inputFile) ? (TextReader)new StdInWrapper(Console.In) : new StreamReader(inputFile))
            {
            	using(TextWriter output = String.IsNullOrWhiteSpace(outputFile) ? Console.Out : new StreamWriter(outputFile))
            	{
            		ICommand command = GetCommand(extra[0]);
		            if (command != null)
		            {
		                command.Run(extra.Skip(1), input, output);
		            }
		            else
		            {
		                Console.WriteLine("Unknown command {0}", extra[0]);
		                Console.WriteLine("Run `CipherPrompt help` for usage");
		            }
		
		            // Close streams
		            input.Close();
		            output.Close();
            	}
            }
        }
    }
}
