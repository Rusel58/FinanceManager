using FinancialModule.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Реализация импорта данных из JSON.
    /// Ожидается, что JSON-файл содержит массив объектов, где каждый объект имеет следующие поля:
    /// "EntityType", "id", "name", "balance", "type", "bank_account_id", "amount", "date", "description", "category_id"
    /// </summary>
    public class JsonImport : ImportTemplate
    {
        public JsonImport(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
            : base(accountFacade, categoryFacade, operationFacade)
        {
        }

        protected override List<ImportedItem> ParseData(string content)
        {
            try
            {
                // Используем JsonSerializer для десериализации содержимого файла в список объектов ImportedItem.
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var data = JsonSerializer.Deserialize<List<ImportedItem>>(content, options);
                return data ?? new List<ImportedItem>();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка парсинга JSON: " + ex.Message);
            }
        }
    }
}
