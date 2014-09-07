using Cipher.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing.Misc
{
    [TestClass]
    public class TextArrayTest
    {
        [TestMethod]
        public void TestSwapping()
        {
            QuadgramScoredLetterArray P = new QuadgramScoredLetterArray("BCD");
            QuadgramScoredLetterArray C = new QuadgramScoredLetterArray(P.Length);

            P.CopyTo(C);

            // START ITERATION

            // Swap
            C.Swap(0, 1);

            // Score ++
            if (true)
            {
                C.CopyTo(P);
            }
            else
            {
                P.CopyTo(C);
            }

            // ITERATION 2

            // Swap
            C.Swap(1, 2);

            // Score --
            if (false)
            {
                C.CopyTo(P);
            }
            else
            {
                P.CopyTo(C);
            }

            Assert.AreEqual<string>("CBD", P.ToString());
        }
    }
}
