using Accounting_for_finance.application.import_export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.domain
{
    /// <summary>
    /// Перечисление типов операций: доход или расход.
    /// </summary>
    public enum OperationType
    {
        Income,
        Expense
    }

    /// <summary>
    /// Класс, представляющий операцию (доход или расход).
    /// </summary>
    public class Operation
    {
        public Guid Id { get; }
        public OperationType Type { get; }
        public Guid BankAccountId { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Description { get; }
        public Guid CategoryId { get; }

        public Operation(Guid guid, OperationType type, Guid bankAccountId, decimal amount, DateTime date, string description, Guid categoryId)
        {
            if (amount < 0)
                throw new ArgumentException("Сумма операции не может быть отрицательной.", nameof(amount));

            Id = guid;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
