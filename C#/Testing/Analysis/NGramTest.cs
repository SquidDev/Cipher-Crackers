using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cipher.Analysis;
using System.Collections.Generic;
using System.Data;

namespace Testing.Analysis
{
    /// <summary>
    /// Test class for NGrams
    /// </summary>
    [TestClass]
    public class NGramTest : DataTest
    {
        /// <summary>
        /// Prefix of NGram results in the XML file
        /// </summary>
        public const string RESULT_PREFIX = "N_";

        /// <summary>
        /// Test method for ngrams
        /// </summary>
        [TestMethod]
        [TestCategory("Analysis")]
        [DeploymentItem(@"TestData\Analysis-NGram.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Analysis-NGram.xml", "AnalysisItem",
            DataAccessMethod.Sequential
        )]
        public void NGram()
        {
            Dictionary<string, int> NGs = NGrams.GatherNGrams(DataRead("Text"), DataReadByte("Length"));
            foreach(KeyValuePair<string, int> NGram in NGs)
            {
                Assert.AreEqual<int>(DataReadInt(RESULT_PREFIX + NGram.Key), NGram.Value);
            }

            // Check for items not in the NGram results
            // (Issue #15 where some letters are ignored)
            foreach(DataColumn DColumn in TestContext.DataRow.Table.Columns)
            {
                string Column = DColumn.ColumnName;
                if(Column.StartsWith(RESULT_PREFIX))
                {
                    Assert.AreEqual<int>(DataReadInt(Column), NGs[Column.Substring(RESULT_PREFIX.Length)]);
                }
            }
        }
    }
}
