using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.domain
{
    /// <summary>
    /// Фабрика для создания доменных объектов.
    /// Централизует логику создания объектов и выполняет валидацию входных параметров.
    /// </summary>
    public static class DomainFactory
    {
        /// <summary>
        /// Создает новый банковский счет.
        /// </summary>
        public static BankAccount CreateBankAccount(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счета не должно быть пустым.", nameof(name));

            if (initialBalance < 0)
                throw new ArgumentException("Начальный баланс не может быть отрицательным.", nameof(initialBalance));

            return new BankAccount(Guid.NewGuid(), name, initialBalance);
        }

        public static BankAccount CreateBankAccount(Guid id, string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название счета не должно быть пустым.", nameof(name));

            if (initialBalance < 0)
                throw new ArgumentException("Начальный баланс не может быть отрицательным.", nameof(initialBalance));

            return new BankAccount(id, name, initialBalance);
        }

        /// <summary>
        /// Создает новую категорию.
        /// </summary>
        public static Category CreateCategory(CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не должно быть пустым.", nameof(name));

            return new Category(Guid.NewGuid(), type, name);
        }

        public static Category CreateCategory(Guid id, CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не должно быть пустым.", nameof(name));

            return new Category(id, type, name);
        }

        /// <summary>
        /// Создает новую операцию.
        /// </summary>
        public static Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, string description, Guid categoryId)
        {
            if (amount < 0)
                throw new ArgumentException("Сумма операции не может быть отрицательной.", nameof(amount));

            return new Operation(Guid.NewGuid(), type, bankAccountId, amount, date, description, categoryId);
        }

        public static Operation CreateOperation(Guid id, OperationType type, Guid bankAccountId, decimal amount, DateTime date, string description, Guid categoryId)
        {
            if (amount < 0)
                throw new ArgumentException("Сумма операции не может быть отрицательной.", nameof(amount));

            return new Operation(id, type, bankAccountId, amount, date, description, categoryId);
        }
    }
}
