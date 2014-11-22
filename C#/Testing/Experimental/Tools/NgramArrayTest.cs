using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Cipher.Text;

namespace Testing.Experimental.Tools
{
    [TestClass]
    public class NGramArrayTest : DataTest
    {
        [TestMethod]
        [TestCategory("Tools"), TestCategory("Experimental")]
        [DeploymentItem(@"TestData\Experimental-Tools-NgramArray.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Experimental-Tools-NgramArray.xml", "AnalysisItem",
            DataAccessMethod.Sequential
        )]
        public void NGramArray()
        {
            InternalNGramArray(DataRead("Text"));
        }

        [TestMethod]
        [TestCategory("Tools"), TestCategory("Experimental")]
        [DeploymentItem(@"TestData\Experimental-Tools-NgramArray.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Experimental-Tools-NgramArray.xml", "AnalysisItem",
            DataAccessMethod.Sequential
        )]
        public void NGramArrayScoring()
        {
            InternalNGramArrayScoring("HELLOWORLD"); // DataRead("Text"));
        }

        #region Internal
        protected void InternalNGramArray(string A)
        {
            LetterArray letters = new LetterArray(A);
            NGramArray nGrams = new NGramArray(A, 2);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual<string>(nGrams.ToString(), letters.ToString());
            }
        }

        protected void InternalNGramArrayScoring(string A)
        {
            QuadgramScoredLetterArray letters = new QuadgramScoredLetterArray(A);
            QuadgramScoredNGramArray nGrams = new QuadgramScoredNGramArray(A, 2);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual<string>(nGrams.ToString(), letters.ToString());
            }

            Assert.AreEqual<double>(letters.ScoreText(), nGrams.ScoreText());
        }
        #endregion

    }
}
