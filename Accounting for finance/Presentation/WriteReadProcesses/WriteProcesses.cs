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

                // Экспорт счетов
                foreach (var account in bankAccountFacade.GetAllBankAccounts())
                {
                    // Если у объектов реализован метод Accept, можно вызвать account.Accept(csvVisitor)
                    csvVisitor.Visit(account);
                }

                // Экспорт категорий
                foreach (var category in categoryFacade.GetAllCategories())
                {
                    csvVisitor.Visit(category);
                }

                // Экспорт операций
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

                // Если у ваших доменных объектов реализован метод Accept:
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
