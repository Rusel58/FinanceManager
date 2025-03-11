using Accounting_for_finance.application.commands;
using Accounting_for_finance.application.Decorators;
using Accounting_for_finance.application.Services;
using Accounting_for_finance.presentation.InOut;
using Accounting_for_finance.presentation.IOController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.presentation.DataProcess
{
    public class BalanceModule
    {
        public static void RecalculateAccountBalance(BalanceRecalculator recalculator)
        {
            Guid accountId;
            while (true)
            {
                ConsoleController.Write("Введите ID счета для пересчета баланса: ", ConsoleColor.Cyan);
                string idStr = ConsoleController.ReadLine();
                if (!Guid.TryParse(idStr, out accountId))
                {
                    ConsoleController.WriteLine("Некорректный ID.", ConsoleColor.Red);
                    continue;
                }
                break;
            }
            ICommand recalcCommand = new RecalculateBalanceCommand(recalculator, accountId);
            ICommand timedCommand = new TimedCommand(recalcCommand, "Пересчет баланса");

            timedCommand.Execute();
        }
    }
}
