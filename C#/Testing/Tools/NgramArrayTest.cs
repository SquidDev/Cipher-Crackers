using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Text;
using NUnit.Framework;

namespace Testing.Experimental.Tools
{
    [TestFixture]
    public class NGramArrayTest
    {
        [Test]
        [Category("Tools")]
        [TestCaseSource("Items")]
        public void NGramArray(string A)
        {
            LetterArray letters = new LetterArray(A);
            NGramArray nGrams = new NGramArray(A, 2);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual(nGrams.ToString(), letters.ToString());
            }
        }

        [Test]
        [Category("Tools")]
        [TestCaseSource("Items")]
        public void NGramArrayScoring(string A)
        {
            QuadgramScoredLetterArray letters = new QuadgramScoredLetterArray(A);
            QuadgramScoredNGramArray nGrams = new QuadgramScoredNGramArray(A, 2);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual(nGrams.ToString(), letters.ToString());
            }

            Assert.AreEqual(letters.ScoreText(), nGrams.ScoreText());
        }

        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Tools-NgramArray.xml");
                return document.Descendants("AnalysisItem").Select(item => new Object[]
                    {
                        item.Element("Text").Value,
                    });
            }
        }
    }
}
