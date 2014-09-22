using Cipher.Ciphers;
using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Ciphers
{
    [TestClass]
    public class RailFenceTest : CipherTest<int>
    {
        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestData\Cipher-RailFence.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-RailFence.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void RailFenceCrack()
        {
            InternalCrack(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadByte("Key"));
        }


        /// <summary>
        /// Tests the decode method
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestData\Cipher-RailFence.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-RailFence.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void RailFenceDecode()
        {
            InternalDecode(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadByte("Key"));
        }

        #region Internal functions
        protected override void InternalCrack(string Ciphertext, string Plaintext, int Key)
        {
            RailFence<QuadgramScoredLetterArray, byte> Shift = new RailFence<QuadgramScoredLetterArray, byte>(Ciphertext);
            RailFence<QuadgramScoredLetterArray, byte>.CipherResult Result = Shift.Crack();


            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual<int>(Key, Result.Key);
        }

        protected override void InternalDecode(string Ciphertext, string Plaintext, int Key)
        {
            RailFence<LetterArray, byte> Shift = new RailFence<LetterArray, byte>(Ciphertext);
            LetterArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
        }
        #endregion
    }
}
