using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

using Cipher.Analysis;
using NUnit.Framework;

namespace Testing.Analysis
{
    /// <summary>
    /// Test class for NGrams
    /// </summary>
    [TestFixture]
    public class NGramTest
    {
        /// <summary>
        /// Prefix of NGram results in the XML file
        /// </summary>
        public const string RESULT_PREFIX = "N_";

        /// <summary>
        /// Test method for ngrams
        /// </summary>
        [Test]
        [TestCaseSource("Items")]
        [Ignore("Dictionary parsing doesn't exist yet")]
        public void NGram(String text, int length, Dictionary<String, int> counts)
        {
            Dictionary<string, int> NGs = NGrams.GatherNGrams(text, length);
            foreach(KeyValuePair<string, int> NGram in NGs)
            {
            	Assert.AreEqual(counts[NGram.Key], NGram.Value);
            }

            // Check for items not in the NGram results
            // (Issue #15 where some letters are ignored)
            foreach(KeyValuePair<String, int> count in counts) {
            	Assert.AreEqual(count.Value, NGs[count.Key]);
            }
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Analysis-NGram.xml");
        		return document.Descendants("AnalysisItem").Select(item => new Object[] {
			        	item.Element("Text").Value,
			        	(int)item.Element("Length"),
			        	null
        			});
        	}
        }
    }
}
