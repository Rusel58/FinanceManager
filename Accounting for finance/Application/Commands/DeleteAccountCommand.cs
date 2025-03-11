using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для удаления банковского счета.
    /// </summary>
    public class DeleteAccountCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly Guid _accountId;

        public DeleteAccountCommand(BankAccountFacade bankAccountFacade, Guid accountId)
        {
            _bankAccountFacade = bankAccountFacade;
            _accountId = accountId;
        }

        public void Execute()
        {
            _bankAccountFacade.DeleteBankAccount(_accountId);
        }
    }
}
