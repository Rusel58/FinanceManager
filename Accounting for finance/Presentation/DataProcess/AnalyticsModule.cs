using Accounting_for_finance.application.Analytics;
using Accounting_for_finance.application.commands;
using Accounting_for_finance.application.Decorators;
using Accounting_for_finance.presentation.IOController;

namespace Accounting_for_finance.presentation.DataProcess
{
    public class AnalyticsModule
    {
        public static void ShowIncomeExpenseDifference(AnalyticsFacade analyticsFacade)
        {
            ConsoleController.Write("Введите начальную дату (yyyy-MM-dd): ", ConsoleColor.Cyan);
            var startStr = ConsoleController.ReadLine();
            DateTime startDate;
            while (true)
            {
                if (!DateTime.TryParse(startStr, out startDate))
                {
                    ConsoleController.WriteLine("Некорректная дата.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            DateTime endDate;
            while (true)
            {
                ConsoleController.Write("Введите конечную дату (yyyy-MM-dd): ", ConsoleColor.Cyan);
                var endStr = ConsoleController.ReadLine();
                if (!DateTime.TryParse(endStr, out endDate))
                {
                    ConsoleController.WriteLine("Некорректная дата.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            ICommand diffCommand = new CalculateIncomeExpenseDifferenceCommand(analyticsFacade, startDate, endDate);
            ICommand timedCommand = new TimedCommand(diffCommand, "Расчет разницы доходов и расходов");
            timedCommand.Execute();

            decimal difference = ((CalculateIncomeExpenseDifferenceCommand)diffCommand).Difference;
            ConsoleController.WriteLine($"Разница доходов и расходов за период: {difference}", ConsoleColor.Green);
        }

        public static void ShowSumByCategory(AnalyticsFacade analyticsFacade)
        {
            ConsoleController.Write("Введите начальную дату (yyyy-MM-dd): ", ConsoleColor.Cyan);
            var startStr = ConsoleController.ReadLine();
            DateTime startDate;
            while (true)
            {
                if (!DateTime.TryParse(startStr, out startDate))
                {
                    ConsoleController.WriteLine("Некорректная дата.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            DateTime endDate;
            while (true)
            {
                ConsoleController.Write("Введите конечную дату (yyyy-MM-dd): ", ConsoleColor.Cyan);
                var endStr = ConsoleController.ReadLine();
                if (!DateTime.TryParse(endStr, out endDate))
                {
                    ConsoleController.WriteLine("Некорректная дата.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            ICommand sumCommand = new CalculateSumByCategoryCommand(analyticsFacade, startDate, endDate);
            ICommand timedCommand = new TimedCommand(sumCommand, "Группировка по категориям");
            timedCommand.Execute();

            var sums = ((CalculateSumByCategoryCommand)sumCommand).Sums;

            ConsoleController.WriteLine("Суммы по категориям:", ConsoleColor.Yellow);
            foreach (var (catId, catName, total) in sums)
            {
                ConsoleController.WriteLine(
                    $"Категория: {catName} (ID: {catId}), сумма: {total}",
                    ConsoleColor.Green
                );
            }
        }
    }
}
