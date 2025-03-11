using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для создания банковского счета.
    /// </summary>
    public class CreateAccountCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly string _name;
        private readonly decimal _balance;

        public BankAccount CreatedAccount { get; private set; }

        public CreateAccountCommand(BankAccountFacade bankAccountFacade, string name, decimal balance)
        {
            _bankAccountFacade = bankAccountFacade;
            _name = name;
            _balance = balance;
        }

        public void Execute()
        {
            CreatedAccount = _bankAccountFacade.CreateBankAccount(_name, _balance);
        }
    }
}
