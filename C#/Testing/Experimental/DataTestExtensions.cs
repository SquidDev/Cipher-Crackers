using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Testing.Experimental
{
    public static class DataTestExtensions
    {
        /// <summary>
        /// Reads a matrix in the form 1,2;3,4
        /// </summary>
        /// <param name="test"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Matrix<float> DataReadMatrix(this DataTest test, string name)
        {
            string text = test.DataRead(name);
            int rows = text.Count(c => c == ';') + 1;
            int columns = (text.Count(c => c == ',') / rows) + 1;

            float[] data = text.Split(',', ';')
                .Select(s => Single.Parse(s))
                .ToArray();

            return Matrix<float>.Build.Dense(rows, columns, data);
        }
    }
}
