using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Конкретная реализация экспортера в JSON.
    /// Собирает данные в виде списка ImportedItem и предоставляет итоговый JSON.
    /// </summary>
    public class JsonExportVisitor : IExportVisitor
    {
        private readonly List<ImportedItem> _exportList = new List<ImportedItem>();

        public void Visit(BankAccount bankAccount)
        {
            _exportList.Add(new ImportedItem
            {
                EntityType = "BankAccount",
                Id = bankAccount.Id,
                Name = bankAccount.Name,
                Balance = bankAccount.Balance,
                Type = "", // для счета тип не используется
                BankAccountId = Guid.Empty,
                Amount = 0,
                Date = default,
                Description = "",
                CategoryId = Guid.Empty
            });
        }

        public void Visit(Category category)
        {
            _exportList.Add(new ImportedItem
            {
                EntityType = "Category",
                Id = category.Id,
                Name = category.Name,
                Balance = 0,
                Type = category.Type == CategoryType.Income ? "income" : "expense",
                BankAccountId = Guid.Empty,
                Amount = 0,
                Date = default,
                Description = "",
                CategoryId = Guid.Empty
            });
        }

        public void Visit(Operation operation)
        {
            _exportList.Add(new ImportedItem
            {
                EntityType = "Operation",
                Id = operation.Id,
                Name = "",
                Balance = 0,
                Type = operation.Type == OperationType.Income ? "income" : "expense",
                BankAccountId = operation.BankAccountId,
                Amount = operation.Amount,
                Date = operation.Date,
                Description = operation.Description,
                CategoryId = operation.CategoryId
            });
        }

        /// <summary>
        /// Возвращает итоговый результат экспорта в виде форматированного JSON.
        /// </summary>
        public string GetExportResult()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(_exportList, options);
        }
    }
}
