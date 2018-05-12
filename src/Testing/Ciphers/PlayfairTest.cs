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
        public void PlayfairDecode(string ciphertext, string plaintext, IEnumerable<string> keys)
        {
            var cipher = new Playfair(TextScorers.ScoreQuadgrams);

            foreach(string key in keys)
            {
	            CachingGridArray gArray = new CachingGridArray(5, 5, key.ToLetterArray(), 26);
	            Assert.AreEqual(plaintext, cipher.Decode(ciphertext, gArray).ToString());
            }
        }

        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void PlayfairCrack(string ciphertext, string plaintext, IEnumerable<string> keys)
        {
            var cipher = new Playfair(TextScorers.ScoreQuadgrams);
            var result = cipher.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.Contains(result.Key.ToString(), keys.ToList());
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
                        item.Elements("Key").Select(x => x.Value),
                    });
            }
        }
    }
}
