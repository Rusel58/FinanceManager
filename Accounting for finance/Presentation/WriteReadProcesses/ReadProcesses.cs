using Accounting_for_finance.application;
using Accounting_for_finance.application.import_export;
using Accounting_for_finance.presentation.IOController;
using FinancialModule.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.presentation.WriteReadProcesses
{
    public class ReadProcesses
    {
        public static void ReadDataFromCsv(string filePath, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade,
            OperationFacade operationFacade)
        {
            try
            {
                var importer = new CsvImport(bankAccountFacade, categoryFacade, operationFacade);
                importer.Import(filePath);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка импорта: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void ReadDataFromJson(string filePath, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade,
            OperationFacade operationFacade)
        {
            try
            {
                var importer = new JsonImport(bankAccountFacade, categoryFacade, operationFacade);
                importer.Import(filePath);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка импорта: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void ReadDataFromYaml(string filePath, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
        {
            try
            {
                var importer = new YamlImport(bankAccountFacade, categoryFacade, operationFacade);
                importer.Import(filePath);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка импорта YAML: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
