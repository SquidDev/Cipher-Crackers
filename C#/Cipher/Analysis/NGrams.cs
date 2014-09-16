using Cipher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Analysis
{
    public class NGrams
    {
        public static Dictionary<string, int> GatherNGrams(string Value, int NGramLength = 2)
        {
            BasicDefaultDict<string, int> Positions = new BasicDefaultDict<string, int>();
            int End = Value.Length - NGramLength;
            for (int Position = 0; Position < End; Position++)
            {
                string NGram = Value.Substring(Position, NGramLength);
                Positions[NGram] = Positions.GetOrDefault(NGram) + 1;
            }

            return Positions;
        }
    }
}
