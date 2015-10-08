using System;
using System.Collections.Generic;
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
        [Ignore("Need to convert items")]
        public void CipherData()
        {
//            CipherAnalysis Analysis = new CipherAnalysis(DataRead("Text"));
//
//            AssertUtils.AssertRoughly(DataReadDouble("IC"), Analysis.TextData.IC, ROUNDING_ACCURACY, "IC");
//            AssertUtils.AssertRoughly(DataReadDouble("MIC"), Analysis.TextData.MIC, ROUNDING_ACCURACY, "MIC");
//            AssertUtils.AssertRoughly(DataReadDouble("MKA"), Analysis.TextData.MKA, ROUNDING_ACCURACY, "MKA");
//            AssertUtils.AssertRoughly(DataReadDouble("DIC"), Analysis.TextData.DIC, ROUNDING_ACCURACY, "DIC");
//            AssertUtils.AssertRoughly(DataReadDouble("EDI"), Analysis.TextData.EDI, ROUNDING_ACCURACY, "EDI");
//            AssertUtils.AssertRoughly(DataReadDouble("LR"), Analysis.TextData.LR, ROUNDING_ACCURACY, "LR");
//            AssertUtils.AssertRoughly(DataReadDouble("ROD"), Analysis.TextData.ROD, ROUNDING_ACCURACY, "ROD");
//            AssertUtils.AssertRoughly(DataReadDouble("LDI"), Analysis.TextData.LDI, ROUNDING_ACCURACY, "LDI");
//            AssertUtils.AssertRoughly(DataReadDouble("SDD"), Analysis.TextData.SDD, ROUNDING_ACCURACY, "SDD");
//
//
//            foreach (KeyValuePair<CipherType, double> Difference in Analysis.Deviations)
//            {
//                AssertUtils.AssertRoughly(DataReadDouble(XML_CIPHER_PREFIX + Difference.Key.Name.Replace(" ", "")), Difference.Value, ROUNDING_ACCURACY, "Cipher " + Difference.Key.Name);
//            }
        }
    }
}
