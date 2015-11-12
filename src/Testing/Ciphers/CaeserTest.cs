using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Ciphers;
using Cipher.Text;
using NUnit.Framework;

namespace Testing.Ciphers
{
    [TestFixture]
    public class CaeserTest
    {
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void Crack(string ciphertext, string plaintext, byte key)
        {
            var shift = new CaeserShift<LetterTextArray>(TextScorers.ScoreQuadgrams);
            var result = shift.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.AreEqual(key, result.Key);
        }

        /// <summary>
        /// Tests using monograms instead
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void MonogramCrack(string ciphertext, string plaintext, byte key)
        {
            var shift = new MonogramCaeserShift<LetterTextArray>();
            var result = shift.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.AreEqual(key, result.Key);
        }

        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void Decode(string ciphertext, string plaintext, byte key)
        {
            var shift = new CaeserShift<LetterTextArray>(TextScorers.ScoreQuadgrams);
            var result = shift.Decode(ciphertext, key);

            Assert.AreEqual(plaintext, result.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-CaeserShift.xml");
                return document.Descendants("Cipher").Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        (byte)(int)item.Element("Key"),
                    });
            }
        }
    }
}
