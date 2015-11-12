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
    public class RailFenceTest
    {
        /// <summary>
        /// Tests the crack method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void RailFenceCrack(string ciphertext, string plaintext, int key)
        {
            var cipher = new RailFence<CharacterTextArray, char>(TextScorers.ScoreQuadgrams);
            var result = cipher.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.AreEqual(key, result.Key);
        }


        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void RailFenceDecode(string ciphertext, string plaintext, int key)
        {
            var cipher = new RailFence<LetterTextArray, byte>(TextScorers.ScoreQuadgrams);
            LetterTextArray result = cipher.Decode(ciphertext, key);

            Assert.AreEqual(plaintext, result.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-RailFence.xml");
                return document.Descendants("Cipher").Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        (int)item.Element("Key"),
                    });
            }
        }
    }
}
