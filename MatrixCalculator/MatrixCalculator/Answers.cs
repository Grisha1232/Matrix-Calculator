using System;
namespace MatrixCalculator
{
    /// <summary>
    /// Класс обработки ответов и структурирования данных, полученных после произведенных операций.
    /// </summary>
    public class Answers
    {
            
        /// <summary>
        /// Ответ на первую операцию калькулятора (Нахождение следа матрицы).<br></br>
        /// Выводит матрицу, над которй производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demension">Порядок квадратной матрицы.</param>
        public static void ForFirstOperation(double[][] matrixFirst, bool random = false, int minValue = default,
                            int maxValue = default, int demension = default)
        {
            if (matrixFirst == null && !random)
                Program.EnterSquareMatrix(out matrixFirst);
            if (matrixFirst == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demension = Program.GetOrderOfSquareMatrix();
                int[] demensions = new int[] { demension, demension };
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            Console.Clear();
            Console.WriteLine("Входная матрица: ");
            Program.PrintMatrix(matrixFirst);
            Console.WriteLine("След матрицы: " + Calculator.TraceMatrix(matrixFirst));
        }

        /// <summary>
        /// Ответ на вторую операцию калькулятора (Транспонирование матрицы).<br></br>
        /// Выводит матрицу, над которй производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demensions">Измерения матрицы.</param>
        public static void ForSecnodOperation(double[][] matrixFirst, bool random = false, int minValue = default,
                            int maxValue = default, int[] demensions = default)
        {
            if (matrixFirst == null && !random)
                Program.EnterOneMatrix(out matrixFirst);
            if (matrixFirst == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demensions = Program.GetDemansionOfTheMatrix();
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            Console.Clear();
            Console.WriteLine("Входная матрица: ");
            Program.PrintMatrix(matrixFirst);
            Console.WriteLine("Транспонированная матрица: ");
            Program.PrintMatrix(Calculator.TransposeMatrix(matrixFirst));
        }

        /// <summary>
        /// Ответ на третью операцию калькулятора (Суммирование двух матриц).<br></br>
        /// Выводит матрицы, над которыми производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="matrixSecond">Матрица, над которой нужно провести операцию.</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demensions">Измерения матриц.</param>
        public static void ForthirdOperation(double[][] matrixFirst, double[][] matrixSecond, bool random = false, int minValue = default,
                            int maxValue = default, int[] demensions = null)
        {
            if (matrixFirst == null && matrixSecond == null && !random)
                Program.EnterTwoMatrixSameDemensions(out matrixFirst, out matrixSecond);
            if (matrixFirst == null && matrixSecond == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demensions = Program.GetDemansionOfTheMatrix();
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
                matrixSecond = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            Console.Clear();
            Console.WriteLine("Входные матрицы: ");
            Program.PrintMatrix(matrixFirst);
            Program.PrintMatrix(matrixSecond);
            Console.WriteLine("Сумма этих матриц: ");
            Program.PrintMatrix(Calculator.SumMatrices(matrixFirst, matrixSecond));
        }

        /// <summary>
        /// Ответ на четвертую операцию калькулятора (Разность двух матриц).<br></br>
        /// Выводит матрицы, над которыми производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="matrixSecond">Матрица, над которой нужно провести операцию.</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demensions">Измерения матриц.</param>
        public static void ForFourthOperation(double[][] matrixFirst, double[][] matrixSecond, bool random = false, int minValue = default,
                            int maxValue = default, int[] demensions = default)
        {
            if (matrixFirst == null && matrixSecond == null && !random)
                Program.EnterTwoMatrixSameDemensions(out matrixFirst, out matrixSecond);
            if (matrixFirst == null && matrixSecond == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demensions = Program.GetDemansionOfTheMatrix();
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
                matrixSecond = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            Console.Clear();
            Console.WriteLine("Входные матрицы: ");
            Program.PrintMatrix(matrixFirst);
            Program.PrintMatrix(matrixSecond);
            Console.WriteLine("Разность этих матриц: ");
            Program.PrintMatrix(Calculator.SubtractMatrices(matrixFirst, matrixSecond));
        }

        /// <summary>
        /// Ответ на пятую операцию калькулятора (Умножение двух матриц).<br></br>
        /// Выводит матрицы, над которыми производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="matrixSecond">Матрица, над которой нужно провести операцию.</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        public static void ForFifthOperation(double[][] matrixFirst, double[][] matrixSecond, bool random = false, int minValue = default,
                            int maxValue = default)
        {
            if (matrixFirst == null && matrixSecond == null && !random)
                Program.EnterTwoMatrixForMultiply(out matrixFirst, out matrixSecond);
            if (matrixFirst == null && matrixSecond == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                int[] demensions1 = Program.GetDemansionOfTheMatrix();
                int[] demensions2;
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions1);
                do
                {
                    demensions2 = Program.GetDemansionOfTheMatrix();
                } while (demensions1[1] != demensions2[0]);
                matrixSecond = Program.RandomizeMatrix(minValue, maxValue, demensions2);
            }
            Console.Clear();
            Console.WriteLine("Входные матрицы: ");
            Program.PrintMatrix(matrixFirst);
            Program.PrintMatrix(matrixSecond);
            Console.WriteLine("Произведение этих матриц: ");
            Program.PrintMatrix(Calculator.MultiplyMatrices(matrixFirst, matrixSecond));
        }


        /// <summary>
        /// Ответ на шустую операцию калькулятора (Умножение матрицы на число).<br></br>
        /// Выводит матрицу, над которй производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demensions">Измерения мартицы.</param>
        public static void ForSixthOperation(double[][] matrixFirst, bool random = false, int minValue = default,
                            int maxValue = default, int[] demensions = default)
        {
            double number;
            string error = "", inputLine;
            if (matrixFirst == null && !random)
                Program.EnterMatrixForMultiplyByNumber(out matrixFirst);
            if (matrixFirst == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demensions = Program.GetDemansionOfTheMatrix();
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            do
            {
                if (error == "")
                {
                    Console.Write("Введите число, на которое нужно домножить матрицу: ");
                    inputLine = Console.ReadLine();
                    error = "Ошибка: Неправильный ввод (Нужно ввести число)";
                }
                else
                {
                    Console.WriteLine(error);
                    Console.Write("Повторите ввод: ");
                    inputLine = Console.ReadLine();
                }
            } while (!double.TryParse(inputLine, out number));
            Console.Clear();
            Console.WriteLine("Ваша матрица: ");
            Program.PrintMatrix(matrixFirst);
            Console.WriteLine($"Матрица умноженная на {number}: ");
            Program.PrintMatrix(Calculator.MultiplyMatrixByNumber(matrixFirst, number));
        }


        /// <summary>
        /// Ответ на седьмую операцию калькулятора (Нахождение определителя матрицы).<br></br>
        /// Выводит матрицу, над которй производится операция, и результат операции.
        /// </summary>
        /// <param name="matrixFirst">Матрица, над которой нужно провести операцию</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demension">Порядок квадратной матрицы.</param>
        public static void ForSeventhOperation(double[][] matrixFirst, bool random = false, int minValue = default,
                            int maxValue = default, int demension = default)
        {
            double det;
            if (matrixFirst == null && !random)
                Program.EnterSquareMatrix(out matrixFirst);
            if (matrixFirst == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demension = Program.GetOrderOfSquareMatrix();
                int[] demensions = new int[] { demension, demension };
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            double[][] steppedView = new double[matrixFirst.Length][];
            Console.Clear();
            Console.WriteLine("Входная матрица: ");
            Program.PrintMatrix(matrixFirst);
            Calculator.GaussMethod(matrixFirst, ref steppedView, out det);
            Console.WriteLine("Определитель этой матрицы: " + Math.Round(det, 5));
        }

        /// <summary>
        /// Ответ на восьмую операцию калькулятора (Решение ситемы).<br></br>
        /// Выводит матрицу ступенчатого вида. Улучешенного ступенчатого вида выводит только при условии, если возможно решить систему.
        /// </summary>
        /// <param name="matrixFirst">Матрица системы уравнений.</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        /// <param name="minValue">Минимальное значение при рандомизации.</param>
        /// <param name="maxValue">Максимальное значение при рандомизации.</param>
        /// <param name="demension">Порядок квадратной матрицы.</param>
        public static void ForEigthOperation(double[][] matrixFirst, bool random = false, int minValue = default,
                            int maxValue = default, int[] demensions = default)
        {
            double det;
            if (matrixFirst == null && !random)
                Program.EnterOneMatrix(out matrixFirst);
            if (matrixFirst == null && random)
            {
                Program.GetMinMaxValues(out minValue, out maxValue);
                demensions = Program.GetDemansionOfTheMatrix();
                matrixFirst = Program.RandomizeMatrix(minValue, maxValue, demensions);
            }
            Console.Clear();
            Console.WriteLine("Входная матрица: ");
            Program.PrintMatrix(matrixFirst);
            double[][] steppedView = new double[matrixFirst.Length][];
            matrixFirst = Calculator.GaussMethod(matrixFirst, ref steppedView, out det);
            Console.WriteLine("Ступенчатый вид этой матрицы: ");
            Program.PrintMatrix(steppedView);
            if (matrixFirst != null)
            {
                Console.WriteLine("Канонический вид матрицы: ");
                Program.PrintMatrix(matrixFirst);
                for (int i = 0; i < matrixFirst.Length; i++)
                {
                    string answer = "";
                    for (int j = 0; j < matrixFirst[0].Length; j++)
                    {
                        if (j == matrixFirst[0].Length - 1)
                            answer += $"= {Math.Round(matrixFirst[i][j], 3)}";
                        else if (j == matrixFirst[0].Length - 2)
                            answer += $"{Math.Abs(Math.Round(matrixFirst[i][j], 3))}*x{j} ";
                        else if (j != matrixFirst[0].Length - 1)
                        {
                            char sign = matrixFirst[i][j + 1] >= 0 ? '+' : '-';
                            answer += $"{Math.Round(matrixFirst[i][j], 3)}*x{j} {sign} ";
                        }
                    }
                    Console.WriteLine(answer);
                }
            }
            else
                Console.WriteLine("Решений нет");
        }
    }
}
