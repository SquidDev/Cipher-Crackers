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

        protected void InternalNGramArray(string A)
        {
            LetterArray Characters = new LetterArray(A);
            NgramArray Letters = new NgramArray(A, 2);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual<string>(Letters.ToString(), Characters.ToString());
            }
        }

    }
}
