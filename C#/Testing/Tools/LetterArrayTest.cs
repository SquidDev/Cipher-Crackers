using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using NUnit.Framework;
using QSCArray = Cipher.Text.QuadgramScoredCharacterArray;
using QSLArray = Cipher.Text.QuadgramScoredLetterArray;

namespace Testing.Tools
{
    [TestFixture]
    public class LetterArrayTest
    {
        [Test]
        [Category("Tools")]
        [TestCaseSource("Items")]
        public void QuadgramArrays(string A)
        {
            QSCArray Characters = new QSCArray(A);
            QSLArray Letters = new QSLArray(A);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual(Letters.ToString(), Characters.ToString());
            }
            Assert.AreEqual(Letters.ScoreText(), Characters.ScoreText());
        }
        
        public IEnumerable<Object[]> Items
        {
            get
            {
                XDocument document = XDocument.Load(@"TestData\Tools-LetterArrays.xml");
                return document.Descendants("AnalysisItem").Select(item => new Object[]
                    {
                        item.Element("Text").Value,
                    });
            }
        }

    }
}
