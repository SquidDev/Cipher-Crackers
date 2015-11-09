using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Cipher.Utils;
using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;

namespace Testing.Tools
{
    [TestFixture]
    public class MatrixUtils
    {
        [Test]
        [Category("Tools")]
        [TestCaseSource("Items")]
        public void AdjugateMatrix(Matrix<float> matrix, Matrix<float> result)
        {
            Assert.AreEqual(result, matrix.Adjugate().Modulus(26));
        }
        
        public IEnumerable<Object[]> Items
        {
        	get 
        	{
        		XDocument document = XDocument.Load(@"TestData\Tools-MatrixUtils.xml");
        		return document.Descendants("Item").Select(item => new Object[] {
						ReadMatrix(item.Element("Matrix").Value),
						ReadMatrix(item.Element("Adjugate").Value),
        			});
        	}
        }
        
        /// <summary>
        /// Reads a matrix in the form 1,3;2,4 which produces the matrix:
        /// / 1 2 \
        /// \ 3 4 /
        /// </summary>
        /// <param name="ntext"></param>
        /// <returns></returns>
        public static Matrix<float> ReadMatrix(String text)
        {
            int rows = text.Count(c => c == ';') + 1;
            int columns = (text.Count(c => c == ',') / rows) + 1;

            float[] data = text.Split(',', ';')
                .Select(s => Single.Parse(s))
                .ToArray();

            return Matrix<float>.Build.Dense(rows, columns, data);
        }

    }
}
