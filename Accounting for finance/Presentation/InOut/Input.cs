using Accounting_for_finance.presentation.IOController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.presentation.InOut
{
    public class Input
    {
        public static string GetPathFromUser()
        {
            while (true)
            {
                ConsoleController.Write("Введите абсолютный путь файла (без кавычек): ", ConsoleColor.Blue);

                string path = ConsoleController.ReadLine();
                Console.WriteLine();
                if (File.Exists(path))
                    return path;
                ConsoleController.WriteLine("Файл не найден, повторите ввод!\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Получение денежной суммы.
        /// </summary>
        /// <returns>Значение денежной суммы</returns>
        public static decimal GetAmountOfMoney(string outputArg, string outputEr)
        {
            decimal balance = 0;
            while (true)
            {
                ConsoleController.Write(outputArg, ConsoleColor.Cyan);
                if (!decimal.TryParse(ConsoleController.ReadLine(), out balance) || balance < 0)
                {
                    ConsoleController.WriteLine(outputEr, ConsoleColor.Red);
                    continue;
                }
                break;

            }
            return balance;
        }

        /// <summary>
        /// Получаем строку от пользователя.
        /// </summary>
        /// <returns>Строка не являющаяся пустотой.</returns>
        public static string GetString(string outputArg, string outputEr)
        {
            string name = string.Empty;
            while (true)
            {
                ConsoleController.Write(outputArg, ConsoleColor.Cyan);
                name = ConsoleController.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    ConsoleController.WriteLine(outputEr, ConsoleColor.Red);
                    continue;
                }
                break;
            }
            return name;

        }

        public static int GetNumberFromUser(string outputArg, string outputEr, int min, int max)
        {
            int number;
            while (true)
            {
                ConsoleController.Write("Введите тип категории (0 - Income, 1 - Expense): ", ConsoleColor.Cyan);
                if (int.TryParse(ConsoleController.ReadLine(), out number) && number >= min && number <= max)
                    break;
                ConsoleController.WriteLine("Некорректное значение, повторите ввод.", ConsoleColor.Red);
                continue;
            }
            return number;
        }

        public static string GetFilePathToWrite()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDirectory, Input.GetString("Введите имя файла для записи: ", "Имя не должно быть пустым."));
        }
    }
}
