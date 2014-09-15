using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cipher.Text.WordParser.Storage
{
    public class GuessLoader
    {
        public List<string> Sources = new List<string>();
        public List<string> RawDictionaries = new List<string>();
        public List<string> XmlDictionaries = new List<string>();

        public FrequencyStorage Frequencies = new FrequencyStorage();

        public GuessLoader() { }
        public GuessLoader(IEnumerable<string> Paths)
        {
            foreach (string FilePath in Paths)
            {
                if (Directory.Exists(FilePath))
                {
                    QueueDirectory(FilePath);
                }
                else if (File.Exists(FilePath))
                {
                    QueueFile(FilePath);
                }
            }
        }

        #region Gueueing
        public void QueueDirectory(string DirPath)
        {
            Queue<string> FileQueue = new Queue<string>();
            FileQueue.Enqueue(DirPath);
            while (FileQueue.Count > 0)
            {
                DirPath = FileQueue.Dequeue();
                foreach (string SubDir in Directory.EnumerateDirectories(DirPath))
                {
                    FileQueue.Enqueue(SubDir);
                }
                  
                foreach (string FilePath in Directory.EnumerateFiles(DirPath))
                {
                    QueueFile(FilePath);
                }
            }
        }

        public void QueueFile(string FilePath)
        {
            switch (Path.GetExtension(FilePath))
            {
                case ".xml":
                    XmlDictionaries.Add(FilePath);
                    break;
                case ".dict":
                    RawDictionaries.Add(FilePath);
                    break;
                default:
                    Sources.Add(FilePath);
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Load in files
        /// </summary>
        public void Load()
        {
            foreach (string Source in Sources)
            {
                using (StreamReader Reader = new StreamReader(Source))
                {
                    Frequencies.ReadText(Reader.ReadToEnd());
                }
            }

            if (Frequencies.Count == 0)
            {
                throw new InvalidOperationException("You must load some sample texts before loading dictionaries");
            }
            double AverageScore = Frequencies.Average(KV => KV.Value.N);
            // Normalise
            Frequencies.Normalise();

            // Load dictionaries
            foreach (string Dictionary in RawDictionaries)
            {
                using (StreamReader Reader = new StreamReader(Dictionary))
                {
                    Frequencies.ReadPlainDictionary(Reader, AverageScore);
                }
            }

            foreach (string Dictionary in XmlDictionaries)
            {
                using (StreamReader Reader = new StreamReader(Dictionary))
                {
                    Frequencies.ReadXMLDictionary(Reader, AverageScore);
                }
            }
        }

        public int Count
        {
            get
            {
                return Sources.Count + RawDictionaries.Count + XmlDictionaries.Count;
            }
        }
    }
}
