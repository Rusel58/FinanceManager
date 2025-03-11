using Accounting_for_finance.application.import_export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.domain
{
    /// <summary>
    /// Класс, представляющий банковский счет.
    /// </summary>
    public class BankAccount
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Balance { get; private set; }

        public BankAccount(Guid id, string name, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Название счета не должно быть пустым.", nameof(name));

            Id = id;
            Name = name;
            Balance = balance;
        }

        /// <summary>
        /// Метод для внесения суммы на счет.
        /// </summary>
        public void Deposit(decimal amount)
        {
            if(amount < 0)
                throw new ArgumentException("Сумма для внесения не может быть меньше нуля.", nameof(amount));

            Balance += amount;
        }

        /// <summary>
        /// Метод для снятия суммы со счета.
        /// </summary>
        public void Withdraw(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Сумма для снятия не может быть меньше нуля.", nameof(amount));

            if (amount > Balance)
                throw new ArgumentException("На балансе недостаточно средств для снятия.", nameof(amount));

            Balance -= amount;
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
