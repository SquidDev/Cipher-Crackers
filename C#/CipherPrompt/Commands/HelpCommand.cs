using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cipher.Prompt.Commands
{
    public class HelpCommand : ICommand
    {
        protected CipherPrompt Instance;
        protected IEnumerable<ICommand> Commands;
        public HelpCommand(CipherPrompt Instance)
        {
            this.Instance = Instance;
            Commands = Instance.Commands.Values.Distinct().OrderBy(V => V.Name);
            Name = "help";
            Description = "Show this message and exit";
        }

        public override void Run(IEnumerable<string> Args, TextReader Reader, TextWriter Writer)
        {
            if (Args != null && Args.Count() > 0)
            {
                foreach (string Arg in Args)
                {
                    ICommand Command = Instance.GetCommand(Arg);
                    if (Command != null)
                    {
                        Command.Run(new string[] { "--help" }, Reader, Writer);
                    }
                }

                return;
            }

            Console.WriteLine(Args.Count());

            Console.WriteLine("Usage: CipherPrompt [MODE] [OPTIONS]");
            Console.WriteLine("Use `CipherPrompt help COMMAND` for help on a specific command");
            Console.WriteLine();
            Console.WriteLine("Commands:");

            foreach (ICommand Command in Commands)
            {
                Console.WriteLine("\t{0,-15}{1}", Command.Name, Command.Description);
            }

            Console.WriteLine();
            Console.WriteLine("Options:");
            Instance.GlobalOptions.WriteOptionDescriptions(Console.Out);
        }
    }
}
