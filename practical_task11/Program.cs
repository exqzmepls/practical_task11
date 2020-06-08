using System;

namespace practical_task11
{
    public class Program
    {
        // Вывод меню
        static void PrintMenu(string[] menuItems, int choice, string info)
        {
            Console.Clear();
            Console.WriteLine(info);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == choice) Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{i + 1}. {menuItems[i]}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        // Выбор пункта из меню
        static int MenuChoice(string[] menuItems, string info = "")
        {
            Console.CursorVisible = false;
            int choice = 0;
            while (true)
            {
                PrintMenu(menuItems, choice, info);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (choice == 0) choice = menuItems.Length;
                        choice--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice == menuItems.Length - 1) choice = -1;
                        choice++;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return choice;
                }
            }
        }

        // Ввод целого числа
        public static int IntInput(int lBound = int.MinValue, int uBound = int.MaxValue, string info = "")
        {
            bool exit;
            int result;
            Console.Write(info);
            do
            {
                exit = int.TryParse(Console.ReadLine(), out result);
                if (!exit) Console.Write("Введено нецелое число! Повторите ввод: ");
                else if (result <= lBound || result >= uBound)
                {
                    exit = false;
                    Console.Write("Введено недопустимое значение! Повторите ввод: ");
                }
            } while (!exit);
            return result;
        }

        // Проверка ввода матрицы
        public static bool CheckMatrixInput(string[] lines, out bool[,] matrix)
        {
            int n = lines.Length;
            matrix = new bool[n, n];

            for (int i = 0; i < n; i++)
            {
                string[] coll = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (coll.Length != n) return false;
                for (int j = 0; j < n; j++)
                {
                    if (!int.TryParse(coll[j], out int result)) return false;
                    // Только 0 или 1
                    if (result < 0 || result > 1) return false;
                    matrix[i, j] = result == 1;
                }
            }
            return true;
        }

        // Является ли матрица шифром
        // В параметр должна передаваться квадратная матрица чётного порядка
        public static bool IsCipher(bool[,] matrix)
        {
            int size = matrix.GetLength(0);
            // Достаточно проверить 1/4 матрицы, тк если проверять всю матрицу, то каждая четверка элементов будет проверена 4 раза
            for (int i = 0; i < size / 2; i++)
                for (int j = 0; j < size / 2; j++)
                {
                    int n = 0;
                    if (matrix[i, j]) n++;
                    if (matrix[j, size - i - 1]) n++;
                    if (matrix[size - i - 1, size - j - 1]) n++;
                    if (matrix[size - j - 1, i]) n++;
                    if (n != 3) return false;
                }
            return true;
        }

        // Поворот матрицы на 90 градусов по часовой стрелке
        static void Turn90(bool[,] a)
        {
            int i, j, size = a.GetLength(0);
            for (i = 0; i < size / 2; i++)
                for (j = 0; j < size / 2; j++)
                {
                    bool tmp = a[i, j];
                    a[i, j] = a[size - j - 1, i];
                    a[size - j - 1, i] = a[size - i - 1, size - j - 1];
                    a[size - i - 1, size - j - 1] = a[j, size - i - 1];
                    a[j, size - i - 1] = tmp;
                }
        }

        // Декодирование
        public static char[] Decoding(bool[,] cipher, string message)
        {
            int size = cipher.GetLength(0);
            char[] result = new char[size * size];
            int index = 0;
            for (int t = 0; t < 4; t++)
            {
                for (int i = 0; i < size; i++) for (int j = 0; j < size; j++) if (!cipher[i, j]) result[index++] = message[10 * i + j];
                Turn90(cipher);
            }
            return result;
        }

        // Кодирование
        public static char[] Coding(bool[,] cipher, string message)
        {
            int size = cipher.GetLength(0);
            char[] result = new char[size * size];
            int index = 0;
            for (int t = 0; t < 4; t++)
            {
                for (int i = 0; i < size; i++) for (int j = 0; j < size; j++) if (!cipher[i, j]) result[10 * i + j] = message[index++];
                Turn90(cipher);
            }
            return result;
        }

        static void Main(string[] args)
        {
            // Пункты меню
            string[] MENU_ITEMS = { "Задать ключ для шифра и ввести текст", "Выйти из программы" };

            // Индекс пункта - выход из программы
            const int EXIT_CHOICE = 1;

            // Индекс пункта меню, который выбрал пользователь
            int userChoice;

            // Размер матрицы для ключа
            const int SIZE = 10;

            while (true)
            {
                // Пользователь выбирает действие (выйти или задать матрицу)
                userChoice = MenuChoice(MENU_ITEMS, "Программа для шифровки/расшифровки текста с помощью решетки\nВыберите действие:");
                if (userChoice == EXIT_CHOICE) break;
                Console.Clear();

                // Ввод ключа
                Console.WriteLine($"Введите матрицу {SIZE}х{SIZE} для ключа");
                Console.WriteLine("Ввод каждой строки матрицы начинайте с новой строчки,\nразделяя элементы по столбцам пробелами");
                string[] input = new string[SIZE];
                for (int i = 0; i < SIZE; i++) input[i] = Console.ReadLine();

                // Проверка правильности ввода матрицы
                if (CheckMatrixInput(input, out bool[,] cipher))
                {
                    // Проверка на то, является ли матрица ключом для шифра
                    if (IsCipher(cipher))
                    {
                        // Ввод текста
                        Console.WriteLine();
                        Console.WriteLine($"Введите текст из {SIZE * SIZE} символов, который нужно зашифровать/расшифровать:");
                        string text = Console.ReadLine();

                        // Проверка длины текста
                        if (text.Length == SIZE * SIZE)
                        {
                            // Декодирование
                            Console.WriteLine("Результат расшифровки данного текста:");
                            Console.WriteLine(Decoding(cipher, text));

                            // Кодирование
                            Console.WriteLine("Результат зашифровки данного текста:");
                            Console.WriteLine(Coding(cipher, text));
                        }
                        else Console.WriteLine("Недопустимая длина текста!");
                    }
                    else Console.WriteLine("Матрица не является ключом для шифрования.");
                }
                else Console.WriteLine("Неверный формат ввода матрицы!");

                Console.WriteLine();
                Console.WriteLine("Нажмите Enter, чтобы вернуться в меню...");
                Console.ReadLine();
            }
        }
    }
}
