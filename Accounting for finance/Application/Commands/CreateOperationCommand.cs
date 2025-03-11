using System;
using Accounting_for_finance.domain;
using FinancialModule.Application;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для создания операции (доход/расход).
    /// </summary>
    public class CreateOperationCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;
        private readonly OperationType _type;
        private readonly Guid _bankAccountId;
        private readonly decimal _amount;
        private readonly DateTime _date;
        private readonly string _description;
        private readonly Guid _categoryId;

        public Operation CreatedOperation { get; private set; }

        public CreateOperationCommand(
            OperationFacade operationFacade,
            OperationType type,
            Guid bankAccountId,
            decimal amount,
            DateTime date,
            string description,
            Guid categoryId)
        {
            _operationFacade = operationFacade;
            _type = type;
            _bankAccountId = bankAccountId;
            _amount = amount;
            _date = date;
            _description = description;
            _categoryId = categoryId;
        }

        public void Execute()
        {
            CreatedOperation = _operationFacade.CreateOperation(
                _type,
                _bankAccountId,
                _amount,
                _date,
                _description,
                _categoryId
            );
        }
    }
}
