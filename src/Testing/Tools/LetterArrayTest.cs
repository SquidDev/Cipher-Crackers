using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Text;
using NUnit.Framework;

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
            var Characters = new CharacterTextArray(A);
            var Letters = new LetterTextArray(A);

            if (A.Count<char>(C => !Char.IsLetter(C)) == 0)
            {
                Assert.AreEqual(Letters.ToString(), Characters.ToString());
            }
            Assert.AreEqual(TextScorers.ScoreQuadgrams(Letters), TextScorers.ScoreQuadgrams(Characters));
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
