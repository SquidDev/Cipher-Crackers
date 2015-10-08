﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Ciphers;
using Cipher.Text;
using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;
using Testing.Ciphers;
using Testing.Experimental;

namespace Testing.Experimental.Ciphers
{
    [TestFixture]
    public class HillTest
    {
        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode"), Category("Experimental")]
        [TestCaseSource("Items")]
        public void HillDecode(string Ciphertext, string Plaintext, Matrix<float> Key)
        {
            Console.WriteLine(Key.Transpose());
            Hill<NGramArray> shift = new Hill<NGramArray>(Ciphertext);
            NGramArray result = shift.Decode(Key);

            Assert.AreEqual(Plaintext, result.ToString());
        }

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode"), Category("Experimental")]
        [TestCaseSource("Items")]
        public void HillBruteCrack(string Ciphertext, string Plaintext, Matrix<float> Key)
        {
            HillBrute<QuadgramScoredNGramArray> hill = new HillBrute<QuadgramScoredNGramArray>(Ciphertext);
            HillBrute<QuadgramScoredNGramArray>.CipherResult result = hill.Crack();

            Assert.AreEqual(Plaintext, result.Text.ToString());
            Assert.AreEqual(Key, result.Key);
        }

        [Test]
        [Category("Cipher"), Category("Decode"), Category("Experimental")]
      	[TestCaseSource("Items")]
        public void HillCribCrack(string Ciphertext, string Plaintext, Matrix<float> Key, string plainCrib, string cipherCrib)
        {
            HillCribbed<NGramArray> hill = new HillCribbed<NGramArray>(Ciphertext);
            hill.Add(cipherCrib, plainCrib);
            HillCribbed<NGramArray>.CipherResult result = hill.Crack();

            Assert.AreEqual(Plaintext, result.Text.ToString());
            Assert.AreEqual(Key, result.Key);
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Experimental-Tools-NgramArray.xml");
        		return document.Descendants("Cipher").Select(item => new Object[] {
						item.Element("Ciphertext").Value,
						item.Element("Plaintext").Value,
						DataTestExtensions.DataReadMatrix(item.Element("Key").Value),
        			});
        	}
        }
        
        public IEnumerable<Object[]> CribKey
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Experimental-Tools-NgramArray.xml");
        		return document.Descendants("Cipher").Select(item => new Object[] {
						item.Element("Ciphertext").Value,
						item.Element("Plaintext").Value,
						DataTestExtensions.DataReadMatrix(item.Element("Key").Value),
						item.Element("PlainCrib").Value,
						item.Element("CipherCrib").Value,
        			});
        	}
        }
    }
}
