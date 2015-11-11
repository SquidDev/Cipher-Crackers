using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Ciphers;
using Cipher.Text;
using NUnit.Framework;

namespace Testing.Ciphers
{
    /// <summary>
    /// Summary description for VigenereTest
    /// </summary>
    [TestFixture]
    public class VigenereTest
    {
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void VigenereDecode(string Ciphertext, string Plaintext, string Key)
        {
            Vigenere<QuadgramScoredLetterArray> Cipher = new Vigenere<QuadgramScoredLetterArray>(Ciphertext);
            QuadgramScoredLetterArray Result = Cipher.Decode(new QuadgramScoredLetterArray(Key));

            Assert.AreEqual(Plaintext, Result.ToString());
        }

        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void VigenereMonogramCrack(string Ciphertext, string Plaintext, string Key)
        {
            MonogramVigenere Cipher = new MonogramVigenere(Ciphertext);
            MonogramVigenere.CipherResult Result = Cipher.Crack();

            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual(Key, Result.Key.ToString());
        }

        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Vigenere.xml");
                return document.Descendants("Cipher").Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        item.Element("Key").Value,
                    });
            }
        }
    }
}
