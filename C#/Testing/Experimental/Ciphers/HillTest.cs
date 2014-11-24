using Cipher.Ciphers;
using Testing.Experimental;
using Cipher.Text;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.Ciphers;
using System;

namespace Testing.Experimental.Ciphers
{
    [TestClass]
    public class HillTest : CipherTest<Matrix<float>>
    {
        /// <summary>
        /// Tests the decode method
        /// </summary>
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Decode"), TestCategory("Experimental")]
        [DeploymentItem(@"TestData\Experimental-Cipher-Hill.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Experimental-Cipher-Hill.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void HillDecode()
        {
            InternalDecode(DataRead("Ciphertext"), DataRead("Plaintext"), this.DataReadMatrix("Key"));
        }

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Decode"), TestCategory("Experimental")]
        [DeploymentItem(@"TestData\Experimental-Cipher-Hill.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Experimental-Cipher-Hill.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        [Ignore]
        public void HillBruteCrack()
        {
            InternalBruteCrack(DataRead("Ciphertext"), DataRead("Plaintext"), this.DataReadMatrix("Key"));
        }

        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Decode"), TestCategory("Experimental")]
        [DeploymentItem(@"TestData\Experimental-Cipher-Hill.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Experimental-Cipher-Hill.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void HillCribCrack()
        {
            InternalCribCrack(DataRead("Ciphertext"), DataRead("Plaintext"), this.DataReadMatrix("Key"), DataRead("PlainCrib"), DataRead("CipherCrib"));
        }

        #region Internal functions
        protected override void InternalDecode(string Ciphertext, string Plaintext, Matrix<float> Key)
        {
            Console.WriteLine(Key.Transpose());
            Hill<NGramArray> shift = new Hill<NGramArray>(Ciphertext);
            NGramArray result = shift.Decode(Key);

            Assert.AreEqual(Plaintext, result.ToString());
        }

        protected void InternalBruteCrack(string Ciphertext, string Plaintext, Matrix<float> Key)
        {
            HillBrute<QuadgramScoredNGramArray> hill = new HillBrute<QuadgramScoredNGramArray>(Ciphertext);
            HillBrute<QuadgramScoredNGramArray>.CipherResult result = hill.Crack();

            Assert.AreEqual(Plaintext, result.Text.ToString());
            Assert.AreEqual(Key, result.Key);
        }

        protected void InternalCribCrack(string Ciphertext, string Plaintext, Matrix<float> Key, string plainCrib, string cipherCrib)
        {
            HillCribbed<NGramArray> hill = new HillCribbed<NGramArray>(Ciphertext);
            hill.Add(cipherCrib, plainCrib);
            HillCribbed<NGramArray>.CipherResult result = hill.Crack();

            Assert.AreEqual(Plaintext, result.Text.ToString());
            Assert.AreEqual(Key, result.Key);
        }

        protected override void InternalCrack(string Ciphertext, string Plaintext, Matrix<float> Key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
