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
						MatrixExtensions.ReadMatrix(item.Element("Matrix").Value),
						MatrixExtensions.ReadMatrix(item.Element("Adjugate").Value),
        			});
        	}
        }
    }
}
