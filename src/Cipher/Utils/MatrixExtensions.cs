using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace Cipher.Utils
{
    public static class MatrixExtensions
    {
        public static Matrix<float> Floor(this Matrix<float> value)
        {
            return value.Map(f => (float)Math.Floor(f));
        }

        public static void Floor(this Matrix<float> value, Matrix<float> result)
        {
            value.Map(f => (float)Math.Floor(f), result);
        }

        public static Matrix<float> Adjugate(this Matrix<float> value)
        {
            return value.CofactorMatrix().Transpose();
        }
        public static Matrix<float> CofactorMatrix(this Matrix<float> value)
        {
            return Matrix<float>.Build.Dense(value.RowCount, value.ColumnCount,
                (row, column) => value.Cofactor(row, column));
        }

        public static float Cofactor(this Matrix<float> value, int row, int column)
        {
            float result = value.MinorEntry(row, column);
            return ((row + column) % 2 == 0) ? result : -result;
        }

        public static float MinorEntry(this Matrix<float> value, int row, int column)
        {
            return value.MinorMatrix(row, column).Determinant();
        }

        public static Matrix<float> MinorMatrix(this Matrix<float> value, int row, int column)
        {
            if (row < 0 || row >= value.RowCount || column < 0 || column >= value.ColumnCount)
            {
                throw new ArgumentException("row and column must statisfy 0 <= row < rows and 0 <= column < columns");
            }

            return value.RemoveRow(row).RemoveColumn(column);
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
