using Accounting_for_finance.domain;
using FinancialModule.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для обновления описания операции.
    /// Поскольку класс Operation иммутабельный, создаётся новый объект с обновленным описанием.
    /// </summary>
    public class UpdateOperationDescriptionCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;
        private readonly Guid _operationId;
        private readonly string _newDescription;

        public Operation UpdatedOperation { get; private set; }

        public UpdateOperationDescriptionCommand(OperationFacade operationFacade, Guid operationId, string newDescription)
        {
            _operationFacade = operationFacade;
            _operationId = operationId;
            _newDescription = newDescription;
        }

        public void Execute()
        {
            UpdatedOperation = _operationFacade.UpdateOperationDescription(_operationId, _newDescription);
        }
    }
}
