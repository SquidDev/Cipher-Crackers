using Cipher.Text;
using Cipher.Utils;
using System.Collections.Generic;

namespace Cipher.Analysis
{
    public class NGrams
    {
        public static Dictionary<string, int> GatherNGrams(string value, int nGramLength = 2, bool stickBoundaries = false, int offset = 0)
        {
            value = value.UpperNoSpace();
            Dictionary<string, int> frequencies = new Dictionary<string, int>();
            int end = value.Length - nGramLength + 1;
            int delta = stickBoundaries ? nGramLength : 1;
            for (int position = offset; position < end; position += delta)
            {
                string nGram = value.Substring(position, nGramLength);
                frequencies[nGram] = frequencies.GetOrCreate(nGram) + 1;
            }

            return frequencies;
        }
    }
}
