using System;

namespace MatrixCalculator
{
    /// <summary>
    /// Класс обработки операций.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Нахождение следа в матрице.
        /// </summary>
        /// <param name="matrix">Квадратная матрица</param>
        /// <returns>Число типа double.</returns>
        public static double TraceMatrix(double[][] matrix)
        {
            double result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result += matrix[i][i];
            }
            return result;
        }

        /// <summary>
        /// Транспонирует матрицу.
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно транспонировать.</param>
        /// <returns>Транспонированую матрицу типа double[][].</returns>
        public static double[][] TransposeMatrix(double[][] matrix)
        {
            double[][] result = new double[matrix[0].Length][];
            for (int i = 0; i < matrix[0].Length; i++)
                result[i] = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    result[j][i] = matrix[i][j];
                }
            }
            return result;

        }

        /// <summary>
        /// Суммирует две матрицы.
        /// </summary>
        /// <param name="matrixFirst">Первая матрица.</param>
        /// <param name="matrixSecond">Вторая матрица</param>
        /// <returns>Матрицу типа double[][].</returns>
        public static double[][] SumMatrices(double[][] matrixFirst, double[][] matrixSecond)
        {
            for (int i = 0; i < matrixFirst.GetLength(0); i++)
            {
                for (int j = 0; j < matrixFirst[0].Length; j++)
                {
                    matrixFirst[i][j] += matrixSecond[i][j];
                }
            }
            return matrixFirst;
        }

        /// <summary>
        /// Вычитает две матрицы.
        /// </summary>
        /// <param name="matrixFirst">Матрица, из которой вычитают.</param>
        /// <param name="matrixSecond">Матрица, которой вычитают.</param>
        /// <returns>Матрицу типа double[][].</returns>
        public static double[][] SubtractMatrices(double[][] matrixFirst, double[][] matrixSecond)
        {
            for (int i = 0; i < matrixFirst.GetLength(0); i++)
            {
                for (int j = 0; j < matrixFirst[0].Length; j++)
                {
                    matrixFirst[i][j] -= matrixSecond[i][j];
                }
            }
            return matrixFirst;
        }

        /// <summary>
        /// Умножает две матрицы.
        /// </summary>
        /// <param name="matrixFirst">Первая матрица в умножении.</param>
        /// <param name="matrixSecond">Вторая матрица в умножении.</param>
        /// <returns>Матрица типа double[][].</returns>
        public static double[][] MultiplyMatrices(double[][] matrixFirst, double[][] matrixSecond)
        {
            double[][] result = new double[matrixFirst.GetLength(0)][];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = new double[matrixSecond[0].Length];
                for (int j = 0; j < matrixSecond[0].Length; j++)
                {
                    result[i][j] = SumOfMultiplies(matrixFirst, matrixSecond, i, j);
                }
            }
            return result;
        }

        /// <summary>
        /// Вспомогательная функция для подсчета умножения матриц.
        /// <br>Считает сумму перемноженых попарно элементы строки n первой матрицы на элементы столбца m второй матрицы</br>
        /// </summary>
        /// <param name="matrixFirst">Первая матрица умножения.</param>
        /// <param name="matrixSecond">Вторая матрица умножения.</param>
        /// <param name="n">Строка первой матрицы, по которой идем умножение.</param>
        /// <param name="m">Столбец второй матрицы, по которому идет умножение.</param>
        /// <returns>Ячейку [n][m] конечной матрицы типа double.</returns>
        static double SumOfMultiplies(double[][] matrixFirst, double[][] matrixSecond, int n, int m)
        {
            double result = 0;
            for (int i = 0; i < matrixFirst[0].Length; i++)
            {
                result += matrixFirst[n][i] * matrixSecond[i][m];
            }
            return result;
        }

        /// <summary>
        /// Умножает матрицу на число.
        /// </summary>
        /// <param name="matrix">Матрица, которая умножается на число.</param>
        /// <param name="number">Число, на которе нужно умножить матрицу.</param>
        /// <returns>Матрицу типа double[][].</returns>
        public static double[][] MultiplyMatrixByNumber(double[][] matrix, double number)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    matrix[i][j] = matrix[i][j] * number;
                }
            }
            return matrix;
        }
        

        /// <summary>
        /// Метод Гаусса.
        /// Требуется для нахождения определителя и для решения системы уравнений.
        /// </summary>
        /// <param name="matrix">Матрица, для которой будет применен метод Гуасса.</param>
        /// <param name="steppedView">Ступенчатый вид матрицы</param>
        /// <param name="multiplyForDetermenant">Значение определителя.</param>
        /// <returns>Матрицу улучешенного ступенчатого вида.</returns>
        public static double[][] GaussMethod(double[][] matrix, ref double[][] steppedView, out double multiplyForDetermenant)
        {
            multiplyForDetermenant = 1;
            int row1 = 0, row2 = 1;
            while (matrix[0][0] == 0 || row2 == matrix.GetLength(0))
            {
                matrix = SwapRows(matrix, row1, row2);
                multiplyForDetermenant *= -1;
                row2++;
            }
            steppedView = SteppedView(matrix, ref multiplyForDetermenant);
            matrix = BetterSteppedView(matrix);
            return matrix;
        }

        /// <summary>
        /// Преобразовывает матрицу к ступенчатому виду.
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно преобразовать.</param>
        /// <param name="multiplyForDetermenant">Определитель полученный после преобразований.</param>
        /// <returns>Матрицу ступенчатого вида.</returns>
        static double[][] SteppedView(double[][] matrix, ref double multiplyForDetermenant)
        {
            double temp;
            double[][] steppedView = new double[matrix.GetLength(0)][];
            int n = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                n = 0;
                double[] elements = matrix[i];
                int zero = 0;
                for (int k = 0; k < elements.Length; k++)
                    if (elements[k] == 0)
                        zero++;
                if (zero == elements.Length && i != matrix.Length - 1)
                {
                    SwapRows(matrix, i, i + 1);
                    multiplyForDetermenant *= -1;
                    elements = matrix[i];
                }
                while (matrix[i][n] == 0)
                {
                    for (int k = 0; k < matrix[0].Length - 1; k++)
                    {
                        if (matrix[i][k] != 0)
                        {
                            n = k;
                            break;
                        }
                    }
                    if (matrix[i][n] != 0) break;
                    if (n >= matrix[0].Length - 1 && i != matrix.Length - 1)
                    {
                        n = 0;
                        SwapRows(matrix, i, i + 1);
                        multiplyForDetermenant *= -1;
                    }
                    else break;
                }
                steppedView[i] = new double[matrix[0].Length];
                multiplyForDetermenant *= matrix[i][i];
                temp = matrix[i][n];
                for (int j = matrix[0].GetLength(0) - 1; j >= i; j--)
                    if (temp != 0)
                        matrix[i][j] = (double)matrix[i][j] / temp;
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    temp = matrix[j][n];
                    for (int k = matrix[0].GetLength(0) - 1; k >= n; k--)
                        matrix[j][k] -= (double)temp * matrix[i][k];
                }
                matrix[i].CopyTo(steppedView[i], 0);
            }
            return steppedView;
        }

        /// <summary>
        /// Преобразовывает ступенчатую матрицу в улучшенную ступенчатого вида матрицу.
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно преобразовать.</param>
        /// <returns>Матрицу улучшенного ступенчатого вида.</returns>
        static double[][] BetterSteppedView(double[][] matrix)
        {
            double temp;
            int n = matrix[0].Length - 2;
            for (int i = 0; i < matrix[0].Length; i++)
            {
                if (matrix[matrix.Length - 1][i] == 1)
                    n = i;
            }
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            {
                double[] elements = matrix[i];
                int zero = 0;
                for (int k = 0; k < elements.Length; k++)
                    if (elements[k] == 0)
                        zero++;
                if (zero == elements.Length)
                    continue;
                if (zero == elements.Length - 1)
                    return null;
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 1)
                        n = j;
                }
                for (int k = i - 1; k >= 0; k--)
                {
                    temp = matrix[k][n];
                    for (int j = matrix[0].GetLength(0) - 1; j >= 0; j--)
                    {
                        matrix[k][j] -= (double)elements[j] * temp;
                    }
                }
            }
            return matrix;
        }

        /// <summary>
        /// Меняет местами указанные строки в матрице.
        /// </summary>
        /// <param name="matrix">Матрица, в которой нужно поменять строки.</param>
        /// <param name="row1">Номер первой строки.</param>
        /// <param name="row2">Номер второй строки.</param>
        /// <returns>Матрицу со свапнутами строками.</returns>
        static double[][] SwapRows(double[][] matrix, int row1, int row2)
        {
            double[] temp = matrix[row1];
            matrix[row1] = matrix[row2];
            matrix[row2] = temp;
            return matrix;
        }
    }
}
