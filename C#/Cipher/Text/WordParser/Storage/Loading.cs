using System.IO;
using System.Xml.Serialization;

namespace Cipher.Text.WordParser.Storage
{
    /// <summary>
    /// Extension class for loading words into <see cref="FrequencyStorage"/>s.
    /// </summary>
    public static class Loading
    {
        static XmlSerializer Serializer = new XmlSerializer(typeof(DictionaryStorage));

        /// <summary>
        /// Read a XML stream as a dictionary
        /// </summary>
        /// <param name="Frequency">Frequency to load into</param>
        /// <param name="Reader">XMl stream</param>
        /// <param name="DefaultScore">Default score of added words</param>
        public static void ReadXMLDictionary(this FrequencyStorage Frequency, TextReader Reader, double DefaultScore = 0)
        {
            DictionaryStorage Storage = Serializer.Deserialize<DictionaryStorage>(Reader);
            if (Storage != null)
            {
                Storage.Process(Frequency, DefaultScore);
            }
        }

        public static void ReadPlainDictionary(this FrequencyStorage Frequency, TextReader Reader, double DefaultScore = 0)
        {
            while (Reader.Peek() >= 0)
            {
                string Word = Reader.ReadLine().Trim().ToUpper();
                
                if (!Frequency.ContainsKey(Word))
                {
                    IFrequencyDistribution Result = Frequency.GetDefault(Word);
                    Result.N = DefaultScore;
                    Frequency[Word] = Result;
                }
            }
        }

        public static T Deserialize<T>(this XmlSerializer Serializer, TextReader InputStream)
        {
            return (T)Serializer.Deserialize(InputStream);
        }
    }
}
