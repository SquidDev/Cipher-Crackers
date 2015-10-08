using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Analysis.AutoSpace;
using Cipher.Analysis.CipherGuess;
using NUnit.Framework;

namespace Testing.Analysis
{
    [TestFixture]
    public class AutoSpaceTest
    {
    	[Test]
        [TestCaseSource("Items")]
        public void AutoSpace(string text, string result)
        {
            WordGuesser Spacer = new WordGuesser(text);
            AssertUtils.AssertWithDiff(result, Spacer.Result);
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Analysis-AutoSpace.xml");
        		return document.Descendants("AnalysisItem").Select(item => new Object[] {
			        	item.Element("Text").Value,
			        	item.Element("Result").Value,
        			});
        	}
        }
    }
}
