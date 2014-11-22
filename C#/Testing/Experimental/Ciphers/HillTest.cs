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

        #region Internal functions
        protected override void InternalDecode(string Ciphertext, string Plaintext, Matrix<float> Key)
        {
            Hill<NGramArray> Shift = new Hill<NGramArray>(Ciphertext);
            NGramArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
        }

        protected override void InternalCrack(string Ciphertext, string Plaintext, Matrix<float> Key)
        {

        }
        #endregion
    }
}
