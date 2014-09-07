using Cipher.Ciphers;
using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Ciphers
{
    [TestClass]
    public class CaeserTest
    {
        // Taken from 2013 National Cipher Challenge
        string Encoded = "LMIZXPQTPWEKWCTLQXIAACXBPMWXXWZBCVQBGBWEWZSWVBPQA";
        string Plain = "DEARPHILHOWCOULDIPASSUPTHEOPPORTUNITYTOWORKONTHIS";
        byte Key = 8;
        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        public void CaeserCrack()
        {
            CaeserShift<QuadgramScoredLetterArray> Shift = new CaeserShift<QuadgramScoredLetterArray>(Encoded);
            CaeserShift<QuadgramScoredLetterArray>.CipherResult Result = Shift.Crack();


            Assert.AreEqual(Plain, Result.Text.ToString());
            Assert.AreEqual<byte>(Key, Result.Key);
        }

        /// <summary>
        /// Tests the crack method
        /// </summary>
        [TestMethod]
        public void CaeserMonogramCrack()
        {
            MonogramCaeserShift Shift = new MonogramCaeserShift(Encoded);
            MonogramCaeserShift.CipherResult Result = Shift.Crack();

            Assert.AreEqual(Plain, Result.Text.ToString());
            Assert.AreEqual<byte>(Key, Result.Key);
        }

        /// <summary>
        /// Tests the decode method
        /// </summary>
        [TestMethod]
        public void CaeserDecode()
        {
            CaeserShift<LetterArray> Shift = new CaeserShift<LetterArray>(Encoded);
            LetterArray Result = Shift.Decode(8);


            Assert.AreEqual(Plain, Result.ToString());
        }
    }
}
