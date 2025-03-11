using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Конкретная реализация посетителя для экспорта данных в CSV.
    /// </summary>
    public class CsvExportVisitor : IExportVisitor
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public CsvExportVisitor()
        {
            // Добавляем заголовок из 10 столбцов
            _sb.AppendLine("\"EntityType\";\"id\";\"name\";\"balance\";\"type\";\"bank_account_id\";\"amount\";\"date\";\"description\";\"category_id\"");
        }

        public void Visit(BankAccount bankAccount)
        {
            // "BankAccount",id,name,balance,"",... остальные пустые
            // "EntityType","id","name","balance","type","bank_account_id","amount","date","description","category_id"
            string line = string.Format("\"BankAccount\";\"{0}\";\"{1}\";\"{2}\";\"\";\"\";\"\";\"\";\"\";\"\"",
                bankAccount.Id,
                bankAccount.Name,
                bankAccount.Balance
            );
            _sb.AppendLine(line);
        }

        public void Visit(Category category)
        {
            // "Category",id,name,"",type,"","","","",""
            // type -> "income" или "expense"
            string catType = (category.Type == CategoryType.Income) ? "income" : "expense";

            string line = string.Format("\"Category\";\"{0}\";\"{1}\";\"\";\"{2}\";\"\";\"\";\"\";\"\";\"\"",
                category.Id,
                category.Name,
                catType
            );
            _sb.AppendLine(line);
        }

        public void Visit(Operation operation)
        {
            // "Operation",id,"","",type,bank_account_id,amount,date,description,category_id
            // type -> "income" или "expense"
            string opType = (operation.Type == OperationType.Income) ? "income" : "expense";

            string line = string.Format("\"Operation\";\"{0}\";\"\";\"\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\"",
                operation.Id,
                opType,
                operation.BankAccountId,
                operation.Amount,
                operation.Date.ToString("yyyy-MM-dd"),
                operation.Description ?? "",
                operation.CategoryId
            );
            _sb.AppendLine(line);
        }

        /// <summary>
        /// Возвращает итоговый результат в виде CSV-строки.
        /// </summary>
        public string GetExportResult()
        {
            return _sb.ToString();
        }
    }
}
