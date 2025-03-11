using Accounting_for_finance.application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для пересчёта баланса банковского счета.
    /// </summary>
    public class RecalculateBalanceCommand : ICommand
    {
        private readonly BalanceRecalculator _recalculator;
        private readonly Guid _accountId;

        public RecalculateBalanceCommand(BalanceRecalculator recalculator, Guid accountId)
        {
            _recalculator = recalculator;
            _accountId = accountId;
        }

        public void Execute()
        {
            _recalculator.RecalculateBalance(_accountId);
        }
    }
}
