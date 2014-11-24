using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathNet.Numerics.LinearAlgebra;
using Cipher.Utils;

namespace Testing.Experimental.Tools
{
    [TestClass]
    public class MatrixUtils : DataTest
    {
        [TestMethod]
        [TestCategory("Tools"), TestCategory("Experimental")]
        [DeploymentItem(@"TestData\Experimental-Tools-MatrixUtils.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\TestData\Experimental-Tools-MatrixUtils.xml", "Item",
            DataAccessMethod.Sequential
        )]
        public void AdjugateMatrix()
        {
            InternalAdjugateMatrix(this.DataReadMatrix("Matrix"), this.DataReadMatrix("Adjugate"));
        }

        

        #region Internal
        protected void InternalAdjugateMatrix(Matrix<float> matrix, Matrix<float> result)
        {
            Assert.AreEqual(result, matrix.Adjugate().Modulus(26));
        }
        #endregion

    }
}
