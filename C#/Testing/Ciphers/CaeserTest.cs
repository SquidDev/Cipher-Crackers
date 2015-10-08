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
        public void Crack(string Ciphertext, string Plaintext, byte Key)
        {
            CaeserShift<QuadgramScoredLetterArray> Shift = new CaeserShift<QuadgramScoredLetterArray>(Ciphertext);
            CaeserShift<QuadgramScoredLetterArray>.CipherResult Result = Shift.Crack();


            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual(Key, Result.Key);
        }

        /// <summary>
        /// Tests using monograms instead
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Crack")]
        [TestCaseSource("Items")]
        public void MonogramCrack(string Ciphertext, string Plaintext, byte Key)
        {
            MonogramCaeserShift Shift = new MonogramCaeserShift(Ciphertext);
            MonogramCaeserShift.CipherResult Result = Shift.Crack();

            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual(Key, Result.Key);
        }

        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void Decode(string Ciphertext, string Plaintext, byte Key)
        {
            CaeserShift<LetterArray> Shift = new CaeserShift<LetterArray>(Ciphertext);
            LetterArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Cipher-CaeserShift.xml");
        		return document.Descendants("Cipher").Select(item => new Object[] {
			        	item.Element("Ciphertext").Value,
			        	item.Element("Plaintext").Value,
			        	(byte)(int)item.Element("Key"),
        			});
        	}
        }
    }
}
