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
    public class SubstitutionTest
    {

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void SubstitutionCrack(string Ciphertext, string Plaintext, string Key)
        {
            Substitution<QuadgramScoredLetterArray> Cipher = new Substitution<QuadgramScoredLetterArray>(Ciphertext);
            Substitution<QuadgramScoredLetterArray>.CipherResult Result = Cipher.Crack();

            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual(Key, Result.Key.ToString());
        }

        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void SubstitutionDecode(string Ciphertext, string Plaintext, string Key)
        {
            Substitution<LetterArray> Shift = new Substitution<LetterArray>(Ciphertext);
            LetterArray Result = Shift.Decode(new QuadgramScoredLetterArray(Key));

            Assert.AreEqual(Plaintext, Result.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Substitution.xml");
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
