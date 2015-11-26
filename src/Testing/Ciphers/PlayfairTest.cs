using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Ciphers;
using Cipher.Ciphers.Keys;
using Cipher.Text;
using NUnit.Framework;

namespace Testing.Ciphers
{
    [TestFixture]
    public class PlayfairTest
    {
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void PlayfairDecode(string ciphertext, string plaintext, string key)
        {
            var cipher = new Playfair<LetterTextArray>(TextScorers.ScoreQuadgrams);

            CachingGridArray gArray = new CachingGridArray(5, 5, key.ToLetterArray());
            Assert.AreEqual(plaintext, cipher.Decode(ciphertext, gArray).ToString());
        }

        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        [Ignore]
        public void PlayfairCrack(string ciphertext, string plaintext, string key)
        {
            var cipher = new Playfair<LetterTextArray>(TextScorers.ScoreQuadgrams);
            var result = cipher.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.AreEqual(key, result.Key.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Playfair.xml");
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
