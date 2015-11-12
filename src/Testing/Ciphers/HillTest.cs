using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Ciphers;
using Cipher.Text;
using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;
using Testing.Ciphers;
using Testing.Experimental;
using Testing.Tools;

namespace Testing.Ciphers
{
    [TestFixture]
    public class HillTest
    {
        /// <summary>
        /// Tests the decode method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("Items")]
        public void HillDecode(string ciphertext, string plaintext, Matrix<float> key)
        {
            Console.WriteLine(key.Transpose());
            Hill cipher = new Hill(TextScorers.ScoreQuadgrams);
            NGramArray result = cipher.Decode(ciphertext, key);

            Assert.AreEqual(plaintext, result.ToString());
        }

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [Test]
        [Category("Cipher"), Category("Decode"), Category("Experimental")]
        [TestCaseSource("BruteItems")]
        public void HillBruteCrack(string ciphertext, string plaintext, Matrix<float> key)
        {
            HillBrute hill = new HillBrute(TextScorers.ScoreMonograms);
            var result = hill.Crack(ciphertext);

            Assert.AreEqual(plaintext, result.Contents.ToString());
            Assert.AreEqual(key, result.Key);
        }

        [Test]
        [Category("Cipher"), Category("Decode")]
        [TestCaseSource("CribItems")]
        public void HillCribCrack(string ciphertext, string plaintext, Matrix<float> key, string plainCrib, string cipherCrib)
        {
            HillCribbed hill = new HillCribbed(TextScorers.ScoreQuadgrams);
            CribSpace cribs = new CribSpace(2);
            cribs.Add(cipherCrib, plainCrib);
            var result = hill.Crack(ciphertext, cribs);

            Assert.AreEqual(key, result.Key);
            Assert.AreEqual(plaintext, result.Contents.ToString());
        }
        
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Hill.xml");
                return document.Descendants("Cipher").Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        MatrixExtensions.ReadMatrix(item.Element("Key").Value),
                    });
            }
        }
        
        public IEnumerable<Object[]> BruteItems
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Hill.xml");
                return document.Descendants("Cipher")
                	.Where(item => Boolean.Parse(item.Element("Brute").Value))
                	.Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        MatrixExtensions.ReadMatrix(item.Element("Key").Value),
                    });
            }
        }
        
        public IEnumerable<Object[]> CribItems
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Cipher-Hill.xml");
                return document.Descendants("Cipher").Select(item => new Object[]
                    {
                        item.Element("Ciphertext").Value,
                        item.Element("Plaintext").Value,
                        MatrixExtensions.ReadMatrix(item.Element("Key").Value),
                        item.Element("PlainCrib").Value,
                        item.Element("CipherCrib").Value,
                    });
            }
        }
    }
}
