using Accounting_for_finance.domain;
using Accounting_for_finance.presentation.IOController;
using FinancialModule.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.Services
{
    /// <summary>
    /// Сервис для пересчёта баланса банковского счёта на основе операций.
    /// Он использует фасады BankAccountFacade и OperationFacade для получения данных и обновления баланса.
    /// </summary>
    public class BalanceRecalculator
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly OperationFacade _operationFacade;

        public BalanceRecalculator(BankAccountFacade bankAccountFacade, OperationFacade operationFacade)
        {
            _bankAccountFacade = bankAccountFacade;
            _operationFacade = operationFacade;
        }

        /// <summary>
        /// Пересчитывает баланс банковского счёта по всем операциям.
        /// </summary>
        /// <param name="bankAccountId">Идентификатор банковского счёта.</param>
        public void RecalculateBalance(Guid bankAccountId)
        {
            var account = _bankAccountFacade.GetBankAccount(bankAccountId);

            var operations = _operationFacade.GetAllOperations()
                .Where(op => op.BankAccountId == bankAccountId);

            decimal calculatedOperations = operations.Sum(op => op.Type == OperationType.Income ? 0 : -op.Amount);

            if (account.Balance - calculatedOperations < 0)
            {
                decimal difference = calculatedOperations - account.Balance;
                    account.Deposit(difference);
                    ConsoleController.WriteLine(
                        $"Баланс увеличен на {difference}. Новый баланс: {account.Balance}",
                        ConsoleColor.Green);
            }
            else
            {
                ConsoleController.WriteLine("Баланс корректен, пересчет не требуется.", ConsoleColor.Green);
            }
        }
    }
}
