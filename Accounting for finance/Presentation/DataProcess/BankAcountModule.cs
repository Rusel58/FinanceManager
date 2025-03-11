using Accounting_for_finance.application;
using Accounting_for_finance.application.commands;
using Accounting_for_finance.application.Decorators;
using Accounting_for_finance.presentation.InOut;
using Accounting_for_finance.presentation.IOController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.presentation.DataProcess
{
    public class BankAcountModule
    {
        public static void CreateBankAccount(BankAccountFacade bankAccountFacade)
        {
            string name = Input.GetString("Введите название счета: ", "Название счета не должно быть пустым.");
            decimal balance = Input.GetAmountOfMoney("Введите начальный баланс: ", "Некорректное значение баланса.");

            ICommand createCommand = new CreateAccountCommand(bankAccountFacade, name, balance);
            ICommand timedCreate = new TimedCommand(createCommand, "Создание банковского счета");
            timedCreate.Execute();

            var createdAccount = ((CreateAccountCommand)createCommand).CreatedAccount;
            ConsoleController.WriteLine($"Банковский счет создан. ID: {createdAccount.Id}", ConsoleColor.Green);
        }

        public static void ChangeBankAccount(BankAccountFacade bankAccountFacade)
        {
            Guid accountId;
            while (true)
            {
                ConsoleController.Write("Введите ID счета для удаления: ", ConsoleColor.Cyan);
                string idStr = ConsoleController.ReadLine();
                if (!Guid.TryParse(idStr, out accountId))
                {
                    ConsoleController.WriteLine("Некорректный ID.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            string newName = Input.GetString("Введите новое название счета: ", "Название не должно быть пустым.");

            ICommand updateCommand = new UpdateAccountCommand(bankAccountFacade, accountId, newName);

            ICommand timedUpdate = new TimedCommand(updateCommand, "Изменение банковского счета");
            try
            {
                timedUpdate.Execute();
                ConsoleController.WriteLine($"Счет обновлен. Новый ID: {accountId}, новое название: {newName}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка обновления счета: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void DeleteBankAccount(BankAccountFacade bankAccountFacade)
        {
            Guid accountId;
            while (true)
            {
                ConsoleController.Write("Введите ID счета для удаления: ", ConsoleColor.Cyan);
                string idStr = ConsoleController.ReadLine();
                if (!Guid.TryParse(idStr, out accountId))
                {
                    ConsoleController.WriteLine("Некорректный ID.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            ICommand deleteCommand = new DeleteAccountCommand(bankAccountFacade, accountId);
            ICommand timedDelete = new TimedCommand(deleteCommand, "Удаление банковского счета");
            try
            {
                timedDelete.Execute();
                ConsoleController.WriteLine("Счет успешно удален.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка удаления счета: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
