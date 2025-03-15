using Accounting_for_finance.application;
using Accounting_for_finance.application.Analytics;
using Accounting_for_finance.infrastructure;
using Accounting_for_finance.presentation.IOController;
using FinancialModule.Application;
using System;

namespace Accounting_for_finance.presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var categoryFacade = new CategoryFacade();
            var bankAccountFacade = new BankAccountFacade();
            var operationFacade = new OperationFacade(bankAccountFacade);
            var analyticsFacade = new AnalyticsFacade(operationFacade, categoryFacade);

            // Регистрация зависимостей.
            DIContainer.RegisterInstance(categoryFacade);
            DIContainer.RegisterInstance(bankAccountFacade);
            DIContainer.RegisterInstance(operationFacade);
            DIContainer.RegisterInstance(analyticsFacade);

            // Разрешение и запуск UI.
            var ui = DIContainer.Resolve<ConsoleUI>();
            ui.Run();

            ConsoleController.WriteLine("Приложение завершило работу.", ConsoleColor.Green);
        }
    }
}
