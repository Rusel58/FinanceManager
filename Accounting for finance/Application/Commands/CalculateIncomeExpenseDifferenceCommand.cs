using Accounting_for_finance.application.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для расчёта разницы между доходами и расходами за указанный период.
    /// </summary>
    public class CalculateIncomeExpenseDifferenceCommand : ICommand
    {
        private readonly AnalyticsFacade _analyticsFacade;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        public decimal Difference { get; private set; }

        public CalculateIncomeExpenseDifferenceCommand(AnalyticsFacade analyticsFacade, DateTime startDate, DateTime endDate)
        {
            _analyticsFacade = analyticsFacade;
            _startDate = startDate;
            _endDate = endDate;
        }

        public void Execute()
        {
            Difference = _analyticsFacade.GetIncomeExpenseDifference(_startDate, _endDate);
        }
    }
}
