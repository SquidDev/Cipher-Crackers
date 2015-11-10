using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Analysis.CipherGuess
{
    public class StatsType
    {
        /// <summary>
        /// Index of Coincidence times 1000
        /// </summary>
        public double IC;

        /// <summary>
        /// Max IC for periods 1-15, times 1000
        /// </summary>
        public double MIC;

        /// <summary>
        /// Max kappa for periods 1-15, times 1000
        /// </summary>
        public double MKA;

        /// <summary>
        /// Digraphic Index of Coincidence, times 10000. 
        /// </summary>
        public double DIC;

        /// <summary>
        /// DIC for even numbered pairs, times 10000.
        /// </summary>
        public double EDI;

        /// <summary>
        /// Long Repeat (percentage of 3 symbol repeats)
        /// </summary>
        public double LR;

        /// <summary>
        /// Percentage of odd-spaced repeats to all repeats.
        /// </summary>
        public double ROD;

        /// <summary>
        /// average English log digraph score
        /// </summary>
        public double LDI;

        /// <summary>
        /// Average English single letter - digraph discrepancy score
        /// </summary>
        public double SDD;
    }

    public class CipherType
    {
        public string Name;
        public StatsType Standard;
        public StatsType Average;

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj is CipherType)
            {
                return ((CipherType)obj).Name == Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        #endregion
    }
}
