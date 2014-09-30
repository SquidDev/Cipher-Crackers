using Cipher.Ciphers;
using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Ciphers
{
    /// <summary>
    /// Summary description for VigenereTest
    /// </summary>
    [TestClass]
    public class VigenereTest : CipherTest<string>
    {
        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Crack")]
        [DeploymentItem(@"TestData\Cipher-Vigenere.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-Vigenere.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void VigenereDecode()
        {
            InternalDecode(DataRead("Ciphertext"), DataRead("Plaintext"), DataRead("Key"));
        }

        [TestMethod]
        [TestCategory("Cipher"), TestCategory("Decode")]
        [DeploymentItem(@"TestData\Cipher-Vigenere.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Cipher-Vigenere.xml", "Cipher",
            DataAccessMethod.Sequential
        )]
        public void VigenereMonogramCrack()
        {
            InternalCrack(DataRead("Ciphertext"), DataRead("Plaintext"), DataRead("Key"));
        }

        #region Internal Methods
        protected override void InternalDecode(string Ciphertext, string Plaintext, string Key)
        {
            Vigenere<QuadgramScoredLetterArray> Cipher = new Vigenere<QuadgramScoredLetterArray>(Ciphertext);
            QuadgramScoredLetterArray Result = Cipher.Decode(new QuadgramScoredLetterArray(Key));

            Assert.AreEqual(Plaintext, Result.ToString());
        }

        protected override void InternalCrack(string Ciphertext, string Plaintext, string Key)
        {
            MonogramVigenere Cipher = new MonogramVigenere(Ciphertext);
            MonogramVigenere.CipherResult Result = Cipher.Crack();

            Assert.AreEqual(Plaintext, Result.Text.ToString());
            Assert.AreEqual<string>(Key, Result.Key.ToString());
        }
        #endregion

        
    }
}
