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
        public void SubstitutionCrack(string ciphertext, string plaintext, string key)
        {
            var cipher = new Substitution<LetterTextArray>(TextScorers.ScoreQuadgrams);
            var result = cipher.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
			Assert.AreEqual(key, KeyConverters.String.ToString(result.Key));            
        }

        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void SubstitutionDecode(string ciphertext, string plaintext, string key)
        {
            var cipher = new Substitution<LetterTextArray>(TextScorers.ScoreQuadgrams);
            LetterTextArray result = cipher.Decode(ciphertext, KeyConverters.String.FromString(key));

            Assert.AreEqual(plaintext, result.ToString());
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
