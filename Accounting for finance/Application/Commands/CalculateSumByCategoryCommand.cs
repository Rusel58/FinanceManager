using Accounting_for_finance.application.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для группировки операций по категориям за указанный период.
    /// </summary>
    public class CalculateSumByCategoryCommand : ICommand
    {
        private readonly AnalyticsFacade _analyticsFacade;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        public List<(Guid categoryId, string categoryName, decimal totalAmount)> Sums { get; private set; }

        public CalculateSumByCategoryCommand(AnalyticsFacade analyticsFacade, DateTime startDate, DateTime endDate)
        {
            _analyticsFacade = analyticsFacade;
            _startDate = startDate;
            _endDate = endDate;
        }

        public void Execute()
        {
            Sums = _analyticsFacade.GetSumByCategoryDetailed(_startDate, _endDate);
        }
    }
}
