using Accounting_for_finance.presentation.IOController;

namespace Accounting_for_finance.presentation.InOut
{
    public class Output
    {
        public static void ShowFormatFileMenu()
        {
            ConsoleController.WriteLine("=== Выбор формат ===", ConsoleColor.Cyan);
            ConsoleController.WriteLine("1. Csv формат", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("2. Json формат", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("3. Yaml формат", ConsoleColor.DarkGreen);
            ConsoleController.Write("Выберите опцию: ", ConsoleColor.Cyan);
        }

        public static void ShowAnalitycsMenu()
        {
            ConsoleController.WriteLine("=== Выбор формат ===", ConsoleColor.Cyan);
            ConsoleController.WriteLine("1. Подсчет разницы доходов и расходов за выбранный период", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("2. Группировка доходов и расходов по категориям", ConsoleColor.DarkGreen);
            ConsoleController.Write("Выберите опцию: ", ConsoleColor.Cyan);
        }

        public static void ShowActionMenu(string choiceFirst, string choiceSecond, string choiceThird)
        {
            ConsoleController.WriteLine("=== Выбор формат ===", ConsoleColor.Cyan);
            ConsoleController.WriteLine(choiceFirst, ConsoleColor.DarkGreen);
            ConsoleController.WriteLine(choiceSecond, ConsoleColor.DarkGreen);
            ConsoleController.WriteLine(choiceThird, ConsoleColor.DarkGreen);
            ConsoleController.Write("Выберите опцию: ", ConsoleColor.Cyan);
        }

        /// <summary>
        /// Отображение меню.
        /// </summary>
        public static void ShowMenu()
        {
            ConsoleController.WriteLine("=== Меню ===", ConsoleColor.Cyan);
            ConsoleController.WriteLine("1. Действия с банковскими счетами", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("2. Действия с категориями", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("3. Действия с операциями", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("4. Считать данные из файлы", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("5. Записать данные в файл", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("6. Показать все банковские счета", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("7. Показать все категории", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("8. Показать все операции", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("9. Аналитика данных", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("10. Пересчета баланса", ConsoleColor.DarkGreen);
            ConsoleController.WriteLine("0. Выход", ConsoleColor.DarkGreen);
            ConsoleController.Write("Выберите опцию: ", ConsoleColor.Cyan);
        }
    }
}
