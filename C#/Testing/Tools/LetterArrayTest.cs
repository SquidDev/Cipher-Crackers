using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using QSCArray = Cipher.Text.QuadgramScoredCharacterArray;
using QSLArray = Cipher.Text.QuadgramScoredLetterArray;

namespace Testing.Tools
{
    [TestClass]
    public class LetterArrayTest : DataTest
    {
        [TestMethod]
        [DeploymentItem(@"TestData\Tools-LetterArrays.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Tools-LetterArrays.xml", "AnalysisItem",
            DataAccessMethod.Sequential
        )]
        public void QuadgramArrays()
        {
            InternalQuadgramArrays(DataRead("Text"));
        }

        protected void InternalQuadgramArrays(string A)
        {
            QSCArray Characters = new QSCArray(A);
            QSLArray Letters = new QSLArray(A);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual<string>(Letters.ToString(), Characters.ToString());
            }
            Assert.AreEqual<double>(Letters.ScoreText(), Characters.ScoreText());
        }

    }
}
