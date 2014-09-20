using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing
{
    public static class AssertUtils
    {
        public static void AssertRoughly(double Expected, double Actual, int DecimalPlaces)
        {
            Assert.AreEqual<double>(Math.Round(Expected, DecimalPlaces), Math.Round(Actual, DecimalPlaces));
        }

        public static void AssertRoughly(double Expected, double Actual, int DecimalPlaces, string Message)
        {
            Assert.AreEqual<double>(Math.Round(Expected, DecimalPlaces), Math.Round(Actual, DecimalPlaces), Message);
        }

        public static void AssertRoughly(double Expected, double Actual, int DecimalPlaces, string Message, params object[] Params)
        {
            Assert.AreEqual<double>(Math.Round(Expected, DecimalPlaces), Math.Round(Actual, DecimalPlaces), Message, Params);
        }

        public static void AssertRoughly(double Expected, double Actual, MidpointRounding Mode)
        {
            Assert.AreEqual<double>(Math.Round(Expected, Mode), Math.Round(Actual, Mode));
        }

        public static void AssertRoughly(double Expected, double Actual, MidpointRounding Mode, string Message)
        {
            Assert.AreEqual<double>(Math.Round(Expected, Mode), Math.Round(Actual, Mode), Message);
        }

        public static void AssertRoughly(double Expected, double Actual, MidpointRounding Mode, string Message, params object[] Params)
        {
            Assert.AreEqual<double>(Math.Round(Expected, Mode), Math.Round(Actual, Mode), Message, Params);
        }

        public static void AssertWithDiff(string Expected, string Actual)
        {
            Assert.AreEqual<string>(Expected, Actual, "Diff: {0}", new Differ(Expected, Actual));
        }

        /// <summary>
        /// Lazy loader String differ
        /// </summary>
        public class Differ
        {
            string Original;
            string New;

            public Differ(string Original, string New)
            {
                this.Original = Original;
                this.New = New;
            }

            public override string ToString()
            {
                IEnumerable<string> Set1 = Original.Split(' ').Distinct();
                IEnumerable<string> Set2 = New.Split(' ').Distinct();

                if (Set2.Count() > Set1.Count())
                {
                    return String.Join(", ", Set2.Except(Set1));
                }
                else
                {
                    return String.Join(", ", Set1.Except(Set2));
                }
            }
        }
        
    }
}
