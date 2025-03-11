using FinancialModule.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для удаления операции.
    /// </summary>
    public class DeleteOperationCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;
        private readonly Guid _operationId;

        public DeleteOperationCommand(OperationFacade operationFacade, Guid operationId)
        {
            _operationFacade = operationFacade;
            _operationId = operationId;
        }

        public void Execute()
        {
            _operationFacade.DeleteOperation(_operationId);
        }
    }
}
