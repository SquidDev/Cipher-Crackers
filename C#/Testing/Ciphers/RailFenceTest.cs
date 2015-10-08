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
        public void RailFenceCrack(string Ciphertext, string Plaintext, int Key)
        {
            RailFence<QuadgramScoredLetterArray, byte> Shift = new RailFence<QuadgramScoredLetterArray, byte>(Ciphertext);
            RailFence<QuadgramScoredLetterArray, byte>.CipherResult Result = Shift.Crack();


            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual(Key, Result.Key);
        }


        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void RailFenceDecode(string Ciphertext, string Plaintext, int Key)
        {
            RailFence<LetterArray, byte> Shift = new RailFence<LetterArray, byte>(Ciphertext);
            LetterArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Cipher-RailFence.xml");
        		return document.Descendants("Cipher").Select(item => new Object[] {
			        	item.Element("Ciphertext").Value,
			        	item.Element("Plaintext").Value,
			        	(int)item.Element("Key"),
        			});
        	}
        }
    }
}
