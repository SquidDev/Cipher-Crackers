using System;
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
                Console.WriteLine("{0}, {1}", row, column);
                throw new ArgumentException("row and column must statisfy 0 <= row < rows and 0 <= column < columns");
            }

            return value.RemoveRow(row).RemoveColumn(column);
        }
    }
}
