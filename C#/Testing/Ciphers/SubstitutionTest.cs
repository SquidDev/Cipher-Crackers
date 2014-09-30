using Cipher.Ciphers;
using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Ciphers
{
    [TestClass]
    public class SubstitutionTest : CipherTest<string>
    {

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Crack")]
        [DeploymentItem(@"TestData\Cipher-Substitution.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-Substitution.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void SubstitutionCrack()
        {
            InternalCrack(DataRead("Ciphertext"), DataRead("Plaintext"), DataRead("Key"));
            
        }

        /// <summary>
        /// Tests the decode method
        /// </summary>
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Decode")]
        [DeploymentItem(@"TestData\Cipher-Substitution.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-Substitution.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void SubstitutionDecode()
        {
            InternalDecode(DataRead("Ciphertext"), DataRead("Plaintext"), DataRead("Key"));
        }

        #region 
        protected override void InternalDecode(string Ciphertext, string Plaintext, string Key)
        {
            Substitution<LetterArray> Shift = new Substitution<LetterArray>(Ciphertext);
            LetterArray Result = Shift.Decode(new QuadgramScoredLetterArray(Key));

            Assert.AreEqual(Plaintext, Result.ToString());
        }

        protected override void InternalCrack(string Ciphertext, string Plaintext, string Key)
        {
            Substitution<QuadgramScoredLetterArray> Cipher = new Substitution<QuadgramScoredLetterArray>(Ciphertext);
            Substitution<QuadgramScoredLetterArray>.CipherResult Result = Cipher.Crack();

            Assert.AreEqual<string>(Plaintext, Result.Text.ToString());
            Assert.AreEqual<string>(Key, Result.Key.ToString());
        }
        #endregion
    }
}
