using Cipher.Ciphers;
using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testing.Ciphers
{
    [TestClass]
    public class TranspositionTest : CipherTest<byte[]>
    {

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Crack")]
        [DeploymentItem(@"TestData\Cipher-Transposition.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-Transposition.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void TranspositionCrack()
        {
            InternalCrack(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadArray("Key", V => Convert.ToByte(V)));
        }

        /// <summary>
        /// Tests the decode method
        /// </summary>
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Decode")]
        [DeploymentItem(@"TestData\Cipher-Transposition.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-Transposition.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void TranspositionDecode()
        {
            InternalDecode(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadArray("Key", V => Convert.ToByte(V)));
        }

        #region 
        protected override void InternalDecode(string Ciphertext, string Plaintext, byte[] Key)
        {
            ColumnarTransposition<CharacterArray, char> Shift = new ColumnarTransposition<CharacterArray, char>(Ciphertext);
            CharacterArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
        }

        protected override void InternalCrack(string Ciphertext, string Plaintext, byte[] Key)
        {
            ColumnarTransposition<QuadgramScoredCharacterArray, char> Cipher = new ColumnarTransposition<QuadgramScoredCharacterArray, char>(Ciphertext);
            ColumnarTransposition<QuadgramScoredCharacterArray, char>.CipherResult Result = Cipher.Crack();

            Assert.AreEqual<string>(Plaintext, Result.Text.ToString());
            AssertUtils.AssertArray(Key, Result.Key);
        }
        #endregion
    }
}
