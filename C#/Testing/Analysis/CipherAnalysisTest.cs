using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Analysis.CipherGuess;
using NUnit.Framework;

namespace Testing.Analysis
{
    [TestFixture]
    public class CipherAnalysisTest
    {
        const int ROUNDING_ACCURACY = 12;
        const string XML_CIPHER_PREFIX = "Cipher_";

        [Test]
        [TestCaseSource("Items")]
        public void CipherData(string text, XElement element)
        {
            CipherAnalysis Analysis = new CipherAnalysis(text);

            AssertUtils.AssertRoughly(Double.Parse(element.Element("IC").Value), Analysis.TextData.IC, ROUNDING_ACCURACY, "IC");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("MIC").Value), Analysis.TextData.MIC, ROUNDING_ACCURACY, "MIC");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("MKA").Value), Analysis.TextData.MKA, ROUNDING_ACCURACY, "MKA");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("DIC").Value), Analysis.TextData.DIC, ROUNDING_ACCURACY, "DIC");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("EDI").Value), Analysis.TextData.EDI, ROUNDING_ACCURACY, "EDI");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("LR").Value), Analysis.TextData.LR, ROUNDING_ACCURACY, "LR");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("ROD").Value), Analysis.TextData.ROD, ROUNDING_ACCURACY, "ROD");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("LDI").Value), Analysis.TextData.LDI, ROUNDING_ACCURACY, "LDI");
	AssertUtils.AssertRoughly(Double.Parse(element.Element("SDD").Value), Analysis.TextData.SDD, ROUNDING_ACCURACY, "SDD");

            foreach (KeyValuePair<CipherType, double> Difference in Analysis.Deviations)
            {
            	AssertUtils.AssertRoughly(Double.Parse(element.Element(XML_CIPHER_PREFIX + Difference.Key.Name.Replace(" ", "")).Value), Difference.Value, ROUNDING_ACCURACY, "Cipher " + Difference.Key.Name);
            }
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Analysis-CipherAnalysis.xml");
        		return document.Descendants("AnalysisItem").Select(item => new Object[] {
			        	item.Element("Text").Value,
			        	item,
        			});
        	}
        }
    }
}
