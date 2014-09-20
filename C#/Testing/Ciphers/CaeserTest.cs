using Cipher.Ciphers;
using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Ciphers
{
    [TestClass]
    public class CaeserTest : CipherTest<byte>
    {
        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestData\Cipher-CaeserShift.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-CaeserShift.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void CaeserCrack()
        {
            InternalCrack(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadByte("Key"));
        }

        /// <summary>
        /// Tests the crack method for monograms
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestData\Cipher-CaeserShift.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-CaeserShift.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void CaeserMonogramCrack()
        {
            InternalMonogramCrack(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadByte("Key"));
        }

        /// <summary>
        /// Tests the decode method
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestData\Cipher-CaeserShift.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-CaeserShift.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void CaeserDecode()
        {
            InternalDecode(DataRead("Ciphertext"), DataRead("Plaintext"), DataReadByte("Key"));
        }

        #region Internal functions
        protected override void InternalCrack(string Ciphertext, string Plaintext, byte Key)
        {
            CaeserShift<QuadgramScoredLetterArray> Shift = new CaeserShift<QuadgramScoredLetterArray>(Ciphertext);
            CaeserShift<QuadgramScoredLetterArray>.CipherResult Result = Shift.Crack();


            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual<byte>(Key, Result.Key);
        }

        protected void InternalMonogramCrack(string Ciphertext, string Plaintext, byte Key)
        {
            MonogramCaeserShift Shift = new MonogramCaeserShift(Ciphertext);
            MonogramCaeserShift.CipherResult Result = Shift.Crack();

            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual<byte>(Key, Result.Key);
        }

        protected override void InternalDecode(string Ciphertext, string Plaintext, byte Key)
        {
            CaeserShift<LetterArray> Shift = new CaeserShift<LetterArray>(Ciphertext);
            LetterArray Result = Shift.Decode(Key);

            Assert.AreEqual(Plaintext, Result.ToString());
        }
        #endregion
    }
}
