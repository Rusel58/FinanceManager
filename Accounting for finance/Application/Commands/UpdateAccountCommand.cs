using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для обновления имени банковского счета.
    /// </summary>
    public class UpdateAccountCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly Guid _accountId;
        private readonly string _newName;

        public UpdateAccountCommand(BankAccountFacade bankAccountFacade, Guid accountId, string newName)
        {
            _bankAccountFacade = bankAccountFacade;
            _accountId = accountId;
            _newName = newName;
        }

        public void Execute()
        {
            _bankAccountFacade.UpdateBankAccountName(_accountId, _newName);
        }
    }
}
