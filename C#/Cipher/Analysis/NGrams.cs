using Cipher.Text;
using Cipher.Utils;
using System.Collections.Generic;

namespace Cipher.Analysis
{
    public class NGrams
    {
        public static Dictionary<string, int> GatherNGrams(string Value, int NGramLength = 2)
        {
            Value = Value.UpperNoSpace();
            BasicDefaultDict<string, int> Frequencies = new BasicDefaultDict<string, int>();
            int End = Value.Length - NGramLength + 1;
            for (int Position = 0; Position < End; Position++)
            {
                string NGram = Value.Substring(Position, NGramLength);
                Frequencies[NGram] = Frequencies.GetOrDefault(NGram) + 1;
            }

            return Frequencies;
        }
    }
}
