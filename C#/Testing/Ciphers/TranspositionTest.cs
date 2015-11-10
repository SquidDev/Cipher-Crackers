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
        public void TranspositionCrack(string Ciphertext, string Plaintext, byte[] Key)
        {
            ColumnarTransposition<QuadgramScoredCharacterArray, char> Cipher = new ColumnarTransposition<QuadgramScoredCharacterArray, char>(Ciphertext);
            ColumnarTransposition<QuadgramScoredCharacterArray, char>.CipherResult Result = Cipher.Crack();

            Assert.AreEqual(Plaintext, Result.Text.ToString());
            AssertUtils.AssertArray(Key, Result.Key);
        }
        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void TranspositionDecode(string Ciphertext, string Plaintext, byte[] Key)
        {
            ColumnarTransposition<CharacterArray, char> Shift = new ColumnarTransposition<CharacterArray, char>(Ciphertext);
            CharacterArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
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
