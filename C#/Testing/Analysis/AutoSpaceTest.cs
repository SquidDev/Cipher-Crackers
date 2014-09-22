using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cipher.Analysis.CipherGuess;
using System.Collections.Generic;
using Cipher.Analysis.AutoSpace;

namespace Testing.Analysis
{
    [TestClass]
    public class AutoSpaceTest : DataTest
    {
        [TestMethod]
        [DeploymentItem(@"TestData\Analysis-AutoSpace.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Analysis-AutoSpace.xml", "AnalysisItem",
            DataAccessMethod.Sequential
        )]
        public void AutoSpace()
        {
            WordGuesser Spacer = new WordGuesser(DataRead("Text"));
            AssertUtils.AssertWithDiff(DataRead("Result"), Spacer.Result);
        }
    }
}
