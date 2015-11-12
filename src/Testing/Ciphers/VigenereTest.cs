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
        public void VigenereDecode(string ciphertext, string plaintext, string key)
        {
            var Cipher = new Vigenere<LetterTextArray>();
            var Result = Cipher.Decode(ciphertext, KeyConverters.String.FromString(key));

            Assert.AreEqual(plaintext, Result.ToString());
        }

        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void VigenereCrack(string ciphertext, string plaintext, string key)
        {
            var cipher = new Vigenere<LetterTextArray>();
            var result = cipher.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.AreEqual(key, KeyConverters.String.ToString(result.Key));
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
