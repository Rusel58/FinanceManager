using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Конкретная реализация экспортера данных в YAML.
    /// Собирает данные в виде списка ImportedItem и возвращает итоговый YAML.
    /// Ключи будут записаны согласно алиасам, указанным в ImportedItem.
    /// </summary>
    public class YamlExportVisitor : IExportVisitor
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
                Type = "", // для BankAccount тип не применяется
                BankAccountId = Guid.Empty,
                Amount = 0,
                Date = null, // пустое значение для даты
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
                Balance = null, // пустое значение для категории
                Type = category.Type == CategoryType.Income ? "income" : "expense",
                BankAccountId = Guid.Empty,
                Amount = 0,
                Date = null,
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
                Balance = null,
                Type = operation.Type == OperationType.Income ? "income" : "expense",
                BankAccountId = operation.BankAccountId,
                Amount = operation.Amount,
                Date = operation.Date,
                Description = operation.Description,
                CategoryId = operation.CategoryId
            });
        }

        /// <summary>
        /// Возвращает итоговый результат экспорта в формате YAML.
        /// Используем NullNamingConvention, чтобы ключи выводились согласно алиасам.
        /// </summary>
        public string GetExportResult()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(NullNamingConvention.Instance)
                .Build();

            return serializer.Serialize(_exportList);
        }
    }
}
