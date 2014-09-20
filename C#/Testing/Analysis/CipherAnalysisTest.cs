using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Cipher.Analysis.CipherGuess;
using System.Collections.Generic;

namespace Testing.Analysis
{
    [TestClass]
    public class CipherAnalysisTest : DataTest
    {
        const int ROUNDING_ACCURACY = 12;

        #region Results
        Dictionary<string, double> Values = new Dictionary<string, double>()
        {
	        {"Plaintext",47.1544213391887},
	        {"Randomdigit",587.33692726119},
	        {"Randomtext",18.90660857389782},
	        {"6x6Bifid",14.825027897066795},
	        {"6x6Playfair",13.09836918601275},
	        {"Amsco",32.190670008407636},
	        {"Bazeries",14.421710187371453},
	        {"Beaufort",3.036495842608442},
	        {"Bifid6",7.385995633069358},
	        {"Bifid7",7.567801322045839},
	        {"Cadenus",25.377162304489183},
	        {"Cmbifid",7.497769191223781},
	        {"Columnar",25.82927032779866},
	        {"Digrafid",9.521142007692692},
	        {"DoubleCheckerBoard",30.5539305030012},
	        {"Four_square",12.880614609072213},
	        {"FracMorse",9.90959333093033},
	        {"Grandpre",601.2744073386241},
	        {"Grille",30.09758603698991},
	        {"Gromark",13.858476455692585},
	        {"Gronsfeld",4.774000797520688},
	        {"Homophonic",634.3947482022614},
	        {"MonomeDinome",573.6011729004227},
	        {"Morbit",597.8633227347002},
	        {"Myszkowski",24.561961733848428},
	        {"Nicodemus",8.583407917333599},
	        {"Nihilistsub",570.8589749400534},
	        {"NihilistTransp",24.786400975219063},
	        {"Patristocrat",13.359628462929482},
	        {"Phillips",6.108395462749319},
	        {"Periodic gromark",15.322232693191394},
	        {"Playfair",11.39827213644591},
	        {"Pollux",58536.50809239805},
	        {"Porta",3.4781807951353003},
	        {"Portax",8.49115692918626},
	        {"Progressivekey",16.505784174691005},
	        {"Progkey beaufort",16.01015878540875},
	        {"Quagmire2",4.088935604189102},
	        {"Quagmire3",3.5053949129567386},
	        {"Quagmire4",4.237759232622264},
	        {"Ragbaby",10.373756575987613},
	        {"Redefence",24.05456128991408},
	        {"RunningKey",5.624001823533857},
	        {"Seriatedpfair",7.107061413645236},
	        {"Swagman",23.969806606006642},
	        {"Tridigital",567.8825191467554},
	        {"Trifid",7.357202076769686},
	        {"Trisquare",12.158073380677061},
	        {"Trisquare HR",12.049440771334709},
	        {"Two square",14.343639676986458},
	        {"Twosquarespiral",12.369415175129497},
	        {"Vigautokey",15.383748908185886},
	        {"Vigenere",3.5486233402066745},
	        {"period 7 Vigenere",2.5010080071104817},
	        {"Vigslidefair",7.17675048560841},
	        {"Route Transp",17.940809462407312}
        };
        #endregion

        [TestMethod]
        [DeploymentItem(@"TestData\Analysis-CipherAnalysis.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Analysis-CipherAnalysis.xml", "AnalysisItem",
            DataAccessMethod.Sequential
        )]
        public void CipherData()
        {
            CipherAnalysis Analysis = new CipherAnalysis(DataRead("Text"));

            AssertUtils.AssertRoughly(DataReadDouble("IC"), Analysis.TextData.IC, ROUNDING_ACCURACY, "IC");
            AssertUtils.AssertRoughly(DataReadDouble("MIC"), Analysis.TextData.MIC, ROUNDING_ACCURACY, "MIC");
            AssertUtils.AssertRoughly(DataReadDouble("MKA"), Analysis.TextData.MKA, ROUNDING_ACCURACY, "MKA");
            AssertUtils.AssertRoughly(DataReadDouble("DIC"), Analysis.TextData.DIC, ROUNDING_ACCURACY, "DIC");
            AssertUtils.AssertRoughly(DataReadDouble("EDI"), Analysis.TextData.EDI, ROUNDING_ACCURACY, "EDI");
            AssertUtils.AssertRoughly(DataReadDouble("LR"), Analysis.TextData.LR, ROUNDING_ACCURACY, "LR");
            AssertUtils.AssertRoughly(DataReadDouble("ROD"), Analysis.TextData.ROD, ROUNDING_ACCURACY, "ROD");
            AssertUtils.AssertRoughly(DataReadDouble("LDI"), Analysis.TextData.LDI, ROUNDING_ACCURACY, "LDI");
            AssertUtils.AssertRoughly(DataReadDouble("SDD"), Analysis.TextData.SDD, ROUNDING_ACCURACY, "SDD");


            foreach (KeyValuePair<CipherType, double> Difference in Analysis.Deviations)
            {
                AssertUtils.AssertRoughly(Values[Difference.Key.Name], Difference.Value, ROUNDING_ACCURACY, "Cipher " + Difference.Key.Name);
            }
        }
    }
}
