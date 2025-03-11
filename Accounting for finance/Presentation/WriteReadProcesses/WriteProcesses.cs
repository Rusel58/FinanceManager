using Accounting_for_finance.application.import_export;
using Accounting_for_finance.application;
using Accounting_for_finance.presentation.IOController;
using FinancialModule.Application;

namespace Accounting_for_finance.presentation.WriteReadProcesses
{
    public class WriteProcesses
    {
        public static void WriteDataToCsv(string filePath, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade,
            OperationFacade operationFacade)
        {
            try
            {
                var csvVisitor = new CsvExportVisitor();

                foreach (var account in bankAccountFacade.GetAllBankAccounts())
                {
                    csvVisitor.Visit(account);
                }

                foreach (var category in categoryFacade.GetAllCategories())
                {
                    csvVisitor.Visit(category);
                }

                foreach (var operation in operationFacade.GetAllOperations())
                {
                    csvVisitor.Visit(operation);
                }

                string csvResult = csvVisitor.GetExportResult();
                File.WriteAllText(filePath, csvResult);
                ConsoleController.WriteLine($"Данные успешно сохранены в файл '{filePath}'.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка экспорта: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void WriteDataToJson(string filePath, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade,
            OperationFacade operationFacade)
        {
            try
            {
                var jsonExportVisitor = new JsonExportVisitor();

                foreach (var account in bankAccountFacade.GetAllBankAccounts())
                {
                    account.Accept(jsonExportVisitor);
                }

                foreach (var category in categoryFacade.GetAllCategories())
                {
                    category.Accept(jsonExportVisitor);
                }

                foreach (var operation in operationFacade.GetAllOperations())
                {
                    operation.Accept(jsonExportVisitor);
                }

                string jsonResult = jsonExportVisitor.GetExportResult();
                File.WriteAllText(filePath, jsonResult);
                ConsoleController.WriteLine($"Данные успешно сохранены в файл '{filePath}'.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка экспорта: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void WriteDataToYaml(string filePath, BankAccountFacade bankAccountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
        {
            try
            {
                var yamlExportVisitor = new YamlExportVisitor();

                foreach (var account in bankAccountFacade.GetAllBankAccounts())
                {
                    account.Accept(yamlExportVisitor);
                }
                foreach (var category in categoryFacade.GetAllCategories())
                {
                    category.Accept(yamlExportVisitor);
                }
                foreach (var operation in operationFacade.GetAllOperations())
                {
                    operation.Accept(yamlExportVisitor);
                }

                string yamlResult = yamlExportVisitor.GetExportResult();
                File.WriteAllText(filePath, yamlResult);
                ConsoleController.WriteLine($"Данные успешно сохранены в YAML-файл '{filePath}'.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка экспорта YAML: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
