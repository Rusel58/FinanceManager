using Accounting_for_finance.application.commands;
using Accounting_for_finance.presentation.IOController;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.Decorators
{
    /// <summary>
    /// Декоратор для измерения времени выполнения команды.
    /// </summary>
    public class TimedCommand : ICommand
    {
        private readonly ICommand _innerCommand;
        private readonly string _commandName;

        public TimedCommand(ICommand innerCommand, string commandName)
        {
            _innerCommand = innerCommand;
            _commandName = commandName;
        }

        public void Execute()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _innerCommand.Execute();
            stopwatch.Stop();
            ConsoleController.WriteLine($"{_commandName} выполнено за {stopwatch.ElapsedMilliseconds} мс", ConsoleColor.Gray);
        }
    }
}
