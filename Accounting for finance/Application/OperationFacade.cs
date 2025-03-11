using System;
using System.Collections.Generic;
using Accounting_for_finance.application;
using Accounting_for_finance.domain;

namespace FinancialModule.Application
{
    /// <summary>
    /// Фасад для работы с операциями.
    /// </summary>
    public class OperationFacade
    {
        // In-memory хранилище операций.
        private readonly Dictionary<Guid, Operation> _operations = new Dictionary<Guid, Operation>();
        // Добавляем ссылку на BankAccountFacade, чтобы можно было менять баланс счёта.
        private readonly BankAccountFacade _bankAccountFacade;

        public OperationFacade(BankAccountFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        /// <summary>
        /// Создание новой операции.
        /// </summary>
        public Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, string description, Guid categoryId)
        {
            Operation operation = DomainFactory.CreateOperation(type, bankAccountId, amount, date, description, categoryId);
            _operations[operation.Id] = operation;

            var account = _bankAccountFacade.GetBankAccount(bankAccountId);

            if (type == OperationType.Income)
            {
                account.Deposit(amount);
            }
            else
            {
                account.Withdraw(amount);
            }

            return operation;
        }

        public Operation CreateOperation(Guid id, OperationType type, Guid bankAccountId, decimal amount, DateTime date, string description, Guid categoryId)
        {
            Operation operation = DomainFactory.CreateOperation(id, type, bankAccountId, amount, date, description, categoryId);
            _operations[operation.Id] = operation;

            var account = _bankAccountFacade.GetBankAccount(bankAccountId);

            if (type == OperationType.Income)
            {
                account.Deposit(amount);
            }
            else
            {
                account.Withdraw(amount);
            }

            return operation;
        }

        public Operation UpdateOperationDescription(Guid operationId, string newDescription)
        {
            if (!_operations.TryGetValue(operationId, out var oldOperation))
                throw new Exception("Операция не найдена.");

            var updatedOperation = new Operation(
                oldOperation.Id,
                oldOperation.Type,
                oldOperation.BankAccountId,
                oldOperation.Amount,
                oldOperation.Date,
                newDescription,          
                oldOperation.CategoryId
            );

            _operations[operationId] = updatedOperation;

            return updatedOperation;
        }


        /// <summary>
        /// Получение операции по идентификатору.
        /// </summary>
        public Operation GetOperation(Guid id)
        {
            if (_operations.TryGetValue(id, out var operation))
                return operation;
            throw new Exception("Операция не найдена.");
        }

        /// <summary>
        /// Получение всех операций.
        /// </summary>
        public IEnumerable<Operation> GetAllOperations()
        {
            return _operations.Values;
        }

        /// <summary>
        /// Удаление операции по идентификатору.
        /// </summary>
        public void DeleteOperation(Guid id)
        {
            if (!_operations.TryGetValue(id, out var operation))
                throw new Exception("Операция не найдена.");

            var account = _bankAccountFacade.GetBankAccount(operation.BankAccountId);

            // Если удаляем доход, то баланс нужно уменьшить,
            // а если удаляем расход, то баланс нужно увеличить.
            if (operation.Type == OperationType.Income)
            {
                // Откатываем доход (как будто снимаем средства)
                account.Withdraw(operation.Amount);
            }
            else
            {
                // Откатываем расход (как будто возвращаем средства)
                account.Deposit(operation.Amount);
            }

            // Удаляем операцию из in-memory хранилища
            _operations.Remove(id);
        }
    }
}
