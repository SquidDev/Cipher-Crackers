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
    public class TranspositionTest
    {

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void TranspositionCrack(string ciphertext, string plaintext, byte[] key)
        {
            var cipher = new ColumnarTransposition<CharacterTextArray, char>(TextScorers.ScoreQuadgrams);
            var result = cipher.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            AssertUtils.AssertArray(key, result.Key);
        }
        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void TranspositionDecode(string ciphertext, string plaintext, byte[] key)
        {
            var cipher = new ColumnarTransposition<CharacterTextArray, char>(TextScorers.ScoreQuadgrams);
            var result = cipher.Decode(ciphertext, key);

            Assert.AreEqual(plaintext, result.ToString());
        }
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Transposition.xml");
                return document.Descendants("Cipher").Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        item.Element("Key").Value.Split(';').Select(v => Convert.ToByte(v)).ToArray(),
                    });
            }
        }
    }
}
