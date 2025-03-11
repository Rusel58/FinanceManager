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
            // Инициализируем фасады для работы с доменной моделью.
            var categoryFacade = new CategoryFacade();
            var bankAccountFacade = new BankAccountFacade();
            var operationFacade = new OperationFacade(bankAccountFacade);
            var analyticsFacade = new AnalyticsFacade(operationFacade, categoryFacade);

            // Регистрируем экземпляры фасадов в DI-контейнере.
            DIContainer.RegisterInstance(categoryFacade);
            DIContainer.RegisterInstance(bankAccountFacade);
            DIContainer.RegisterInstance(operationFacade);

            // Создаем экземпляр консольного интерфейса и запускаем приложение.
            var ui = new ConsoleUI(categoryFacade, bankAccountFacade, operationFacade, analyticsFacade);
            ui.Run();

            // Перед выходом можно добавить завершающее сообщение.
            ConsoleController.WriteLine("Приложение завершило работу.", ConsoleColor.Green);
        }
    }
}
