using Cipher.Text.WordParser;
using System;
using System.IO;

namespace Cipher.Prompt
{
    class TestingProg
    {
        static bool Debug = false;
        static void Main(string[] args)
        {
#if DEBUG
            Debug = true;
#endif
            DateTime Start = DateTime.Now;

            FrequencyStorage Storage = new FrequencyStorage();
            using (StreamReader Reader = new StreamReader("dickens-twocities.txt"))
            {
                Storage.ReadText(Reader.ReadToEnd());
            }

            WordGuesser Guesser = new WordGuesser("philithasbeenstaringyouinthefaceallthistime", Storage);
            Console.WriteLine(String.Join(" ", Guesser.Result));

            if(Debug)
            {
                DateTime End = DateTime.Now;
                TimeSpan Duration = End - Start;
                Console.WriteLine("Took {0} seconds to complete", Duration.TotalSeconds);
                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
            }

        }
    }
}
