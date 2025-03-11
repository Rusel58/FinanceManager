using Accounting_for_finance.application;
using Accounting_for_finance.application.import_export;
using Accounting_for_finance.domain;
using Accounting_for_finance.presentation.IOController;
using Accounting_for_finance.presentation.InOut;
using FinancialModule.Application;
using Accounting_for_finance.presentation.WriteReadProcesses;
using Accounting_for_finance.presentation.DataProcess;
using Accounting_for_finance.application.Analytics;
using Accounting_for_finance.application.Services;

namespace Accounting_for_finance.presentation
{
    /// <summary>
    /// Консольный интерфейс для взаимодействия с пользователем.
    /// Позволяет выполнять базовые операции: создание счетов, категорий, операций и их просмотр.
    /// </summary>
    public class ConsoleUI
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly OperationFacade _operationFacade;
        private readonly AnalyticsFacade _analyticsFacade;

        public ConsoleUI(CategoryFacade categoryFacade, BankAccountFacade bankAccountFacade, OperationFacade operationFacade,
            AnalyticsFacade analyticsFacade)
        {
            _categoryFacade = categoryFacade;
            _bankAccountFacade = bankAccountFacade;
            _operationFacade = operationFacade;
            _analyticsFacade = analyticsFacade;
        }

        /// <summary>
        /// Основной цикл работы интерфейса.
        /// </summary>
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowMenu();
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        BankAccountActions();
                        break;
                    case "2":
                        CategoryActions();
                        break;
                    case "3":
                        OperationActions();
                        break;
                    case "4":
                        ReadDataFromFile();
                        break;
                    case "5":
                        WriteDataToFile();
                        break;
                    case "6":
                        ListAccounts();
                        break;
                    case "7":
                        ListCategories();
                        break;
                    case "8":
                        ListOperations();
                        break;
                    case "9":
                        AnalitycsOperation();
                        break;
                    case "10":
                        ListAccounts();
                        BalanceOperation();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Создание банковского счета.
        /// </summary>
        private void BankAccountActions()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowActionMenu("1. Создать банковский счет", "2. Изменить банковский счет", "3. Удалить банковский счет");
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        BankAcountModule.CreateBankAccount(_bankAccountFacade);
                        exit = true;
                        break;
                    case "2":
                        ListAccounts();
                        BankAcountModule.ChangeBankAccount(_bankAccountFacade);
                        exit = true;
                        break;
                    case "3":
                        ListAccounts();
                        BankAcountModule.DeleteBankAccount(_bankAccountFacade);
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Создание категории.
        /// </summary>
        private void CategoryActions()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowActionMenu("1. Создать категорию", "2. Изменить категорию", "3. Удалить категорию");
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        CategoryModule.CreateCategory(_categoryFacade);
                        exit = true;
                        break;
                    case "2":
                        ListCategories();
                        CategoryModule.ChangeCategory(_categoryFacade);
                        exit = true;
                        break;
                    case "3":
                        ListCategories();
                        CategoryModule.DeleteCategory(_categoryFacade);
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Создание операции с использованием фасада.
        /// </summary>
        private void OperationActions()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowActionMenu("1. Создать операцию", "2. Изменить операцию", "3. Удалить операцию");
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ListAccounts();
                        ListCategories();
                        OperationModule.CreateOperation(_categoryFacade, _operationFacade, _bankAccountFacade);
                        exit = true;
                        break;
                    case "2":
                        ListCategories();
                        OperationModule.ChangeOperation(_operationFacade);
                        break;
                    case "3":
                        ListOperations();
                        OperationModule.DeleteOperation(_operationFacade);
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Вывод списка всех банковских счетов.
        /// </summary>
        private void ListAccounts()
        {
            var accounts = _bankAccountFacade.GetAllBankAccounts();
            ConsoleController.WriteLine("Список банковских счетов:", ConsoleColor.DarkGray);
            foreach (var account in accounts)
            {
                ConsoleController.WriteLine($"ID: {account.Id} | Название: {account.Name} | Баланс: {account.Balance}", ConsoleColor.Yellow);
            }
        }

        /// <summary>
        /// Вывод списка всех категорий.
        /// </summary>
        private void ListCategories()
        {
            var categories = _categoryFacade.GetAllCategories();
            ConsoleController.WriteLine("Список категорий:", ConsoleColor.DarkGray);
            foreach (var category in categories)
            {
                ConsoleController.WriteLine($"ID: {category.Id} | Название: {category.Name} | Тип: {category.Type}", ConsoleColor.Yellow);
            }
        }

        /// <summary>
        /// Вывод списка всех операций.
        /// </summary>
        private void ListOperations()
        {
            var operations = _operationFacade.GetAllOperations();
            ConsoleController.WriteLine("Список операций:", ConsoleColor.DarkGray);
            foreach (var op in operations)
            {
                ConsoleController.WriteLine($"ID: {op.Id} | Сумма: {op.Amount} | Дата: {op.Date} | Описание: {op.Description}", ConsoleColor.Yellow);
            }
        }

        /// <summary>
        /// Чтение данных из CSV-файла.
        /// Использует реализацию CsvImport, которая следует шаблонному методу импорта.
        /// </summary>
        private void ReadDataFromFile()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowFormatFileMenu();
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        ReadProcesses.ReadDataFromCsv(Input.GetPathFromUser(), _bankAccountFacade, _categoryFacade, _operationFacade);
                        exit = true;
                        break;
                    case "2":
                        ReadProcesses.ReadDataFromJson(Input.GetPathFromUser(), _bankAccountFacade, _categoryFacade, _operationFacade);
                        exit = true;
                        break;
                    case "3":
                        ReadProcesses.ReadDataFromYaml(Input.GetPathFromUser(), _bankAccountFacade, _categoryFacade, _operationFacade);
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Запись данных в CSV-файл.
        /// Собираем данные из фасадов и используем CsvExportVisitor для формирования CSV-строки.
        /// Затем сохраняем строку в указанный файл.
        /// </summary>
        private void WriteDataToFile()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowFormatFileMenu();
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        WriteProcesses.WriteDataToCsv(Input.GetFilePathToWrite() + ".csv", _bankAccountFacade, _categoryFacade, _operationFacade);
                        exit = true;
                        break;
                    case "2":
                        WriteProcesses.WriteDataToJson(Input.GetFilePathToWrite() + ".json", _bankAccountFacade, _categoryFacade, _operationFacade);
                        exit = true;
                        break;
                    case "3":
                        WriteProcesses.WriteDataToYaml(Input.GetFilePathToWrite() + ".yaml", _bankAccountFacade, _categoryFacade, _operationFacade);
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }

        private void AnalitycsOperation()
        {
            bool exit = false;
            while (!exit)
            {
                Output.ShowAnalitycsMenu();
                string choice = ConsoleController.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        AnalyticsModule.ShowIncomeExpenseDifference(_analyticsFacade);
                        exit = true;
                        break;
                    case "2":
                        AnalyticsModule.ShowSumByCategory(_analyticsFacade);
                        exit = true;
                        break;
                    default:
                        ConsoleController.WriteLine("Неверный выбор, повторите попытку.", ConsoleColor.Red);
                        break;
                }
                Console.WriteLine();
            }
        }

        private void BalanceOperation()
        {
            BalanceRecalculator balanceRecalculator = new BalanceRecalculator(_bankAccountFacade, _operationFacade);
            BalanceModule.RecalculateAccountBalance(balanceRecalculator);
        }
    }
}
