using System;
using System.IO;

namespace MatrixCalculator
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        static void Main()
        {
            do
            {
                Console.Clear();
                PrintRulesAndOperations();
                int operationMode, inputMode;
                CalculatorMode(out operationMode);
                GetInputMode(out inputMode);
                Start(inputMode, operationMode);
                Console.WriteLine("\nДля выхода из калькулятора нажмите ESC, для повторного использования нажмите любую клавишу.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            Console.WriteLine("Goodbye))");
        }

        /// <summary>
        /// Вывод правил в консоль.
        /// </summary>
        static void PrintRulesAndOperations()
        {
            Console.WriteLine("Перед началом работы с калькулятором прочитайте файл readme.txt для корректного пользования калькулятором. Файл находится рядом с исходником.");
            Console.WriteLine("В начале выбирите операцию, которую вы хотите выполнить: \n" +
                "1. Нахождение следа матрицы.\n" +
                "2. Транспонирование матрицы.\n" +
                "3. Сумма двух матриц.\n" +
                "4. Разность двух матриц.\n" +
                "5. Произведение двух матриц.\n" +
                "6. Умножение матрицы на число.\n" +
                "7. Нахождение определителя матрицы.\n" +
                "8. Решение системы уравнений.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Выбор операции калькулятора.
        /// </summary>
        /// <param name="n">Переменная, в которую запишется номер операции калькулятора.</param>
        static void CalculatorMode(out int mode)
        {
            string inputLine, error = "";
            // Проверка на корректный ввод данных, в данном случае корректность на ввод номера операции.
            do
            {
                if (error == "")
                {
                    Console.Write("Введите номер операции (от 1 до 8): ");
                    inputLine = Console.ReadLine();
                    error = "Ошибка: Неправильный ввод (Нужно ввести целое число от 1 до 8)";
                }
                // Вывод ошибки при некорректном вводе.
                else
                {
                    Console.WriteLine(error);
                    Console.Write("Повторите ввод: ");
                    inputLine = Console.ReadLine();
                }
            } while (!int.TryParse(inputLine, out mode) || mode < 1 || mode > 8);
        }

        /// <summary>
        /// Получение режима ввода матрицы в калькулятор.
        /// </summary>
        /// <param name="inputMode">Переменная, в которую запишется номер режима ввода матриц.</param>
        static void GetInputMode(out int inputMode)
        {
            Console.WriteLine("1. Файл (полный путь до файла).\n" +
                "2. Рандомизировать матрицы.\n" +
                "3. Ввести через клавиатуру матрицы.");
            string inputLine, error = "";
            do
            {
                if (error == "")
                {
                    Console.Write("Введите номер input (от 1 до 3): ");
                    inputLine = Console.ReadLine();
                    error = "Ошибка: Неправильный ввод (Нужно ввести целое число от 1 до 3)";
                }
                // Вывод ошибки при некорректном вводе.
                else
                {
                    Console.WriteLine(error);
                    Console.Write("Повторите ввод: ");
                    inputLine = Console.ReadLine();
                }
            } while (!int.TryParse(inputLine, out inputMode) || inputMode < 1 || inputMode > 3);
            Console.Clear();
        }

        /// <summary>
        /// Запуск калькулятора.
        /// </summary>
        /// <param name="inputMode">Режим ввода матриц.</param>
        /// <param name="operationMode">Номер операции.</param>
        static void Start(int inputMode, int operationMode)
        {
            string path;
            double[][] matrixFirst = null, matrixSecond = null;
            bool isOk = true;
            switch (inputMode)
            {
                // Ввод через файл.
                case 1:
                    string error = "";
                    // Проверка на существования пути к файлу
                    do
                    {
                        if (error == "")
                            Console.Write("Введите полный путь к файлу с матрицами формата txt: ");
                        else
                            Console.Write(error + "Такого файла не существует, проверьте правильность пути и введите заного путь: ");
                        path = Console.ReadLine();
                        error = "Ошибка. ";
                    } while (!File.Exists(path));
                    Console.Clear();
                    ReadMatricesFromFile(path, ref isOk, ref matrixFirst, ref matrixSecond);
                    Console.WriteLine();
                    if (isOk && CheckCorrectFileMatrices(operationMode, matrixFirst: matrixFirst, matrixSecond: matrixSecond))
                        Calculate(operationMode, matrixFirst: matrixFirst, matrixSecond: matrixSecond);
                    else
                        Console.WriteLine("Я не могу произвести эту операцию над этой(ими) матрицой(ами)((\n" +
                            "Проверьте файл на корректность введенных в нем матриц.");
                    break;
                // Рандомизация матриц.
                case 2:
                    Calculate(operationMode, random: true);
                    break;
                // Ввод матриц с клавиатуры.
                case 3:
                    Calculate(operationMode);
                    break;
            }
        }


        /// <summary>
        /// Чтение матрицы из файла по полному пути. Матрицы передаются по ссылке. Если в файле только одна матрица,
        /// то matrixSecond ничего не присвоится. Если же в файле ничего нет, то выведет в консоль сообщение об этом.
        /// </summary>
        /// <param name="path">Полный путь до файла строкой.</param>
        /// <param name="matrixFirst">Ссылка на первую матрицу.</param>
        /// <param name="matrixSecond">Ссылка на вторую матрицу.</param>
        static void ReadMatricesFromFile(string path, ref bool isOk, ref double[][] matrixFirst, ref double[][] matrixSecond)
        {
            int k = 0;
            foreach (var matrix in File.ReadAllText(path).TrimEnd('\r', '\n').Split("\n\n"))
            {
                if (matrix != null && k == 0)
                {
                    matrixFirst = new double[matrix.Split('\n').Length][];
                    for (int i = 0; i < matrix.Split('\n').Length; i++)
                    {
                        string line = matrix.Split('\n')[i].TrimEnd(' ');
                        matrixFirst[i] = new double[line.Split(' ').Length];
                        for (int j = 0; j < line.Split(' ').Length; j++)
                        {
                            if (!double.TryParse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[j], out matrixFirst[i][j]))
                            {
                                isOk = false;
                                goto Failed;
                            }
                        }
                    }
                    k++;
                }
                else if (matrix != null && k == 1)
                {
                    matrixSecond = new double[matrix.Split('\n').Length][];
                    for (int i = 0; i < matrix.Split('\n').Length; i++)
                    {
                        string line = matrix.Split('\n')[i].TrimEnd(' ');
                        matrixSecond[i] = new double[line.Split(' ').Length];
                        for (int j = 0; j < line.Split(' ').Length; j++)
                        {
                            if (!double.TryParse(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[j], out matrixSecond[i][j]))
                            {
                                isOk = false;
                                goto Failed;
                            }
                        }
                    }
                    k++;
                }
                else
                    Console.WriteLine("Пустой файл");
            }
        Failed:
            return;
        }

        /// <summary>
        /// Проверка на правильность измерений матриц при различных операциях.
        /// </summary>
        /// <param name="operationMode">Номер операции калькулятора.</param>
        /// <param name="matrixFirst">Первая матрица в операции.</param>
        /// <param name="matrixSecond">Вторая матрица в операции.</param>
        /// <returns>True если все правила с измерениями при счете с матрицами соблюдены, иначе False.</returns>
        static bool CheckCorrectFileMatrices(int operationMode, double[][] matrixFirst, double[][] matrixSecond = null)
        {
            if (operationMode == 1 || operationMode == 7)
                return matrixFirst.GetLength(0) == matrixFirst[0].Length;
            else if (matrixSecond == null && (operationMode == 3 || operationMode == 4 || operationMode == 5))
                return false;
            else if ((operationMode == 3 || operationMode == 4))
                return (matrixFirst.GetLength(0) == matrixSecond.GetLength(0)) && (matrixFirst[0].Length == matrixSecond[0].Length);
            else if (operationMode == 5)
                return matrixFirst[0].Length == matrixSecond.GetLength(0);
            else
                return true;
        }

        /// <summary>
        /// Рандомизатор матриц.
        /// </summary>
        /// <param name="minValue">Минимальное значение, которое будет присутствовать в матрице.</param>
        /// <param name="maxValue">Максимальное значение, которое будет присутствовать в матрице.</param>
        /// <param name="demensions"></param>
        /// <returns>Матрицу типа double[][], но, так как удобней работать с целыми числами, по факту int[][]</returns>
        public static double[][] RandomizeMatrix(int minValue, int maxValue, int[] demensions)
        {
            var rnd = new Random();
            double[][] result = new double[demensions[0]][];
            for (int i = 0; i < demensions[0]; i++)
            {
                result[i] = new double[demensions[1]];
                for (int j = 0; j < demensions[1]; j++)
                {
                    result[i][j] = rnd.Next(minValue, maxValue);
                }
            }
            return result;
        }

        /// <summary>
        /// Получение от пользователя минимального и максимального значение для рандомизации матрицы.
        /// </summary>
        /// <param name="minValue">Переменная, в которую вернется минимальное значение.</param>
        /// <param name="maxValue">Переменная, в которую вернется максимальное значение.</param>
        public static void GetMinMaxValues(out int minValue, out int maxValue)
        {
            string inputLine, error = "";
            do
            {
                if (error == "")
                {
                    Console.WriteLine($"Введите сначала минимальное значение, потом максимальное значение через пробел" +
                        " (каждое из значений может принимать от -999999 до 999999): ");
                    inputLine = Console.ReadLine();
                    error = "Ошибка: Неправильный ввод (нужно ввести два целых числа сначала минимальное потом максимальное). ";
                }
                // Вывод ошибка при некорректном вводе.
                else
                {
                    Console.Write(error);
                    Console.Write("Повторите ввод: ");
                    inputLine = Console.ReadLine();
                }
            } while (inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length != 2
                     || !int.TryParse(inputLine.Split()[0], out minValue)
                     || !int.TryParse(inputLine.Split()[1], out maxValue)
                     || minValue > maxValue);
        }

        /// <summary>
        /// Получение ответа введенной операции от введенных матриц.
        /// </summary>
        /// <param name="operationMode">Номер операции калькулятора.</param>
        /// <param name="matrixFirst">Первая матрица.</param>
        /// <param name="matrixSecond">Вторая матрица.</param>
        /// <param name="random">Флаг, указывающий на нужность рандомизации матриц.</param>
        static void Calculate(int operationMode, double[][] matrixFirst = null, double[][] matrixSecond = null, bool random = false)
        {
            switch (operationMode)
            {
                case 1:
                    Answers.ForFirstOperation(matrixFirst, random: random);
                    break;
                case 2:
                    Answers.ForSecnodOperation(matrixFirst, random: random);
                    break;
                case 3:
                    Answers.ForthirdOperation(matrixFirst, matrixSecond, random: random);
                    break;
                case 4:
                    Answers.ForFourthOperation(matrixFirst, matrixSecond, random: random);
                    break;
                case 5:
                    Answers.ForFifthOperation(matrixFirst, matrixSecond, random: random);
                    break;
                case 6:
                    Answers.ForSixthOperation(matrixFirst, random: random);
                    break;
                case 7:
                    Answers.ForSeventhOperation(matrixFirst, random: random);
                    break;
                case 8:
                    Answers.ForEigthOperation(matrixFirst, random: random);
                    break;
            }
        }


        /// <summary>
        /// Запрос на порядок квадратной матрицы и ввод этой матрицы с клавиатуры.
        /// <br>Возвращает введенную матрицу с клавиатуры</br>
        /// </summary>
        /// <param name="matrix">Возвращаемая матрица введенная с клавитуры.</param>
        public static void EnterSquareMatrix(out double[][] matrix)
        {
            int demension = GetOrderOfSquareMatrix();
            matrix = GetUserMatrix(demension, demension);
        }

        /// <summary>
        /// Запрос на измерения матрицы и ввод этой матрицы с клавиатуры.
        /// <br>Возвращает введенную матрицу с клавиатуры</br>
        /// </summary>
        /// <param name="matrix">Возвращаемая матрица введенная с клавитуры.</param>
        public static void EnterOneMatrix(out double[][] matrix)
        {
            int[] demensions = GetDemansionOfTheMatrix();
            matrix = GetUserMatrix(demensions[0], demensions[1]);
        }

        /// <summary>
        /// Запрос на измерения матриц и ввод двух матриц этих матриц с клавитуры.
        /// </summary>
        /// <param name="matrixFirst">Возвращаемая перая матрица введенная с клавитуры.</param>
        /// <param name="matrixSecond">Возвращаемая вторая матрица введенная с клавитуры.</param>
        public static void EnterTwoMatrixSameDemensions(out double[][] matrixFirst, out double[][] matrixSecond)
        {
            int[] demensions = GetDemansionOfTheMatrix();
            matrixFirst = GetUserMatrix(demensions[0], demensions[1]);
            matrixSecond = GetUserMatrix(demensions[0], demensions[1]);
        }

        /// <summary>
        /// Запрос на измерение первой матрицы для умножения и ввод этой матрицы с клавиатуры.
        /// <br>Запрос на измерение второй матрицы с проверкой на корректность измерений для умножения и ввод этой матрицы с клавиатуры</br>
        /// </summary>
        /// <param name="matrixFirst">Возвращаемая перая матрица введенная с клавитуры.</param>
        /// <param name="matrixSecond">Возвращаемая вторая матрица введенная с клавитуры.</param>
        public static void EnterTwoMatrixForMultiply(out double[][] matrixFirst, out double[][] matrixSecond)
        {
            int[] demensions1 = GetDemansionOfTheMatrix();
            int[] demensions2;
            string error = "";
            matrixFirst = GetUserMatrix(demensions1[0], demensions1[1]);
            do
            {
                if (error == "")
                {
                    demensions2 = GetDemansionOfTheMatrix();
                    error = "Ошибка: Неверные измерения для перемножения матриц. ";
                }
                else
                {
                    Console.Write(error + $"Для перемножения кол-во строк второй матрицы должно быть равным {demensions1[1]}. ");
                    Console.WriteLine("Повторите ввод: ");
                    demensions2 = GetDemansionOfTheMatrix();
                }
            } while (demensions1[1] != demensions2[0]);
            matrixSecond = GetUserMatrix(demensions2[0], demensions2[1]);
        }

        /// <summary>
        /// Запрос на порядок квадратной матрицы и ввод с клавиатуры самой матрицы.
        /// <br>Возвращает введенную матрицу с клавиатуры</br>
        /// </summary>
        /// <param name="matrix">Возвращаема матрица введенная с клавитуры.</param>
        public static void EnterMatrixForMultiplyByNumber(out double[][] matrix)
        {
            int[] demensions = GetDemansionOfTheMatrix();
            matrix = GetUserMatrix(demensions[0], demensions[1]);
        }


        /// <summary>
        /// Получение порядка квадратной матрицы от пользователя.
        /// </summary>
        /// <returns>int, порядок квадратной матрицы введеной с клавиатуры пользователем.</returns>
        public static int GetOrderOfSquareMatrix()
        {
            string inputLine, error = "";
            int demension;
            do
            {
                if (error == "")
                {
                    Console.Write("Введите порядок квадратной матрицы: ");
                    inputLine = Console.ReadLine();
                    error = "Ошибка: Неправильный ввод (Нужно ввести натуральное число до 101)";
                }
                // Вывод ошибки при некорректном вводе.
                else
                {
                    Console.WriteLine(error);
                    Console.Write("Повторите ввод: ");
                    inputLine = Console.ReadLine();
                }
            } while (!int.TryParse(inputLine, out demension) || (demension <= 0 && demension >= 100));
            return demension;
        }



        /// <summary>
        /// Получение измерений матрицы от пользователя.
        /// </summary>
        /// <returns>int[], массив с двумя элементами.</returns>
        public static int[] GetDemansionOfTheMatrix()
        {
            string inputLine, error = "";
            int demension1, demension2;
            // Ввод размерности матрицы
            do
            {
                if (error == "")
                {
                    Console.WriteLine("Введите рамерность матрицы (n x m) через пробел: ");
                    inputLine = Console.ReadLine();
                    error = "Ошибка: Неправильный ввод (нужно ввести два натуральных числа до 10)";
                }
                // Вывод ошибки при некорректном вводе.
                else
                {
                    Console.WriteLine(error);
                    Console.Write("Повторите ввод: ");
                    inputLine = Console.ReadLine();
                }
            } while (inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length != 2
                     || !int.TryParse(inputLine.Split()[0], out demension1) || (demension1 <= 0 && demension1 > 10)
                     || !int.TryParse(inputLine.Split()[1], out demension2) || (demension2 <= 0 && demension2 > 10));

            return new int[] { demension1, demension2 };
        }



        /// <summary>
        /// Ввод пользователем матрицы размера nxm.
        /// </summary>
        /// <param name="n">Кол-во строк в матрице.</param>
        /// <param name="m">Кол-во столбцов в матрице.</param>
        /// <returns>Матрицу типа double[][], введеннуюю с клавиатуры пользователем.</returns>
        public static double[][] GetUserMatrix(int n, int m)
        {
            double[][] userMatrix = new double[n][];
            string inputLine;
            double[] numbersInLine = new double[m];
            Console.WriteLine($"Введите по строчно матрицу {n}x{m} (Все элементы через пробел): ");
            // Ввод матрицы
            for (int i = 0; i < n; i++)
            {
                userMatrix[i] = new double[m];
            // Проверка на корректность введенной матрицы
            EnterAgain:
                string error = "";
                do
                {
                    if (error == "")
                    {

                        Console.Write($"Введите строку номер {i + 1}: ");
                        inputLine = Console.ReadLine();
                        error = "Ошибка: Количество элементов в строке не соответствует размерности. ";
                    }
                    else
                    {
                        Console.Write(error);
                        Console.Write("Повторите ввод: ");
                        inputLine = Console.ReadLine();
                    }
                } while (inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != m);

                for (int j = 0; j < m; j++)
                {
                    if (!double.TryParse(inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries)[j], out numbersInLine[j]))
                    {
                        Console.WriteLine("Ошибка: один из элементов не число. Повторите ввод.");
                        goto EnterAgain;
                    }
                    userMatrix[i][j] = numbersInLine[j];
                }
            }
            return userMatrix;
        }


        /// <summary>
        /// Вывод матрицы в консоль.
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно вывести в консоль.</param>
        public static void PrintMatrix(double[][] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != 0)
                        Console.Write("{0, -9}" + " ", Math.Round(matrix[i][j], 3));
                    else
                        Console.Write("{0, -9}" + " ", Math.Abs(matrix[i][j]));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
