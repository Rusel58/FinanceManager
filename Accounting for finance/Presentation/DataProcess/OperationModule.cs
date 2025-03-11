using Accounting_for_finance.application;
using Accounting_for_finance.application.commands;
using Accounting_for_finance.application.Decorators;
using Accounting_for_finance.domain;
using Accounting_for_finance.presentation.InOut;
using Accounting_for_finance.presentation.IOController;
using FinancialModule.Application;
using System;

namespace Accounting_for_finance.presentation.DataProcess
{
    public class OperationModule
    {
        public static void CreateOperation(
            CategoryFacade categoryFacade,
            OperationFacade operationFacade,
            BankAccountFacade bankAccountFacade)
        {
            // Ввод ID банковского счета
            Guid accountId;
            while (true)
            {
                ConsoleController.Write("Введите ID банковского счета: ", ConsoleColor.Cyan);
                if (!Guid.TryParse(ConsoleController.ReadLine(), out accountId))
                {
                    ConsoleController.WriteLine("Некорректный ID счета.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            // Ввод ID категории
            Guid categoryId;
            while (true)
            {
                ConsoleController.Write("Введите ID категории: ", ConsoleColor.Cyan);
                if (!Guid.TryParse(ConsoleController.ReadLine(), out categoryId))
                {
                    ConsoleController.WriteLine("Некорректный ID категории.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            // Получаем саму категорию (чтобы понять, доход или расход)
            Category category;
            try
            {
                category = categoryFacade.GetCategory(categoryId);
            }
            catch (Exception)
            {
                ConsoleController.WriteLine("Категория не найдена.", ConsoleColor.Red);
                return;
            }

            // Определяем тип операции на основании типа категории
            OperationType opType = (category.Type == CategoryType.Income)
                ? OperationType.Income
                : OperationType.Expense;

            // Ввод суммы операции, с проверкой баланса, если это расход
            decimal amount;
            while (true)
            {
                amount = Input.GetAmountOfMoney("Введите сумму операции: ", "Некорректное значение суммы.");

                // Если это расход, проверяем, хватает ли денег на счёте
                if (opType == OperationType.Expense)
                {
                    // Получаем счёт, чтобы проверить его баланс
                    var account = bankAccountFacade.GetBankAccount(accountId);
                    if (account.Balance < amount)
                    {
                        ConsoleController.WriteLine(
                            $"Недостаточно средств на счёте. Текущий баланс: {account.Balance}. Повторите ввод суммы.",
                            ConsoleColor.Red
                        );
                        continue; // запрашиваем сумму заново
                    }
                }

                // Если доход или денег достаточно, выходим из цикла
                break;
            }

            // Ввод описания
            string description = Input.GetString("Введите описание операции: ", "Описание не должно быть пустым.");

            // Создаём команду для создания операции
            var createCommand = new CreateOperationCommand(
                operationFacade,
                opType,
                accountId,
                amount,
                DateTime.Now,
                description,
                categoryId
            );

            ICommand timedCreate = new TimedCommand(createCommand, "Создание операции");
            timedCreate.Execute();

            var operation = ((CreateOperationCommand)createCommand).CreatedOperation;
            ConsoleController.WriteLine($"Операция создана. ID: {operation.Id}", ConsoleColor.Green);
        }

        public static void ChangeOperation(OperationFacade operationFacade)
        {
            Guid operationId;
            while (true)
            {
                ConsoleController.Write("Введите ID операции для изменения описания: ", ConsoleColor.Cyan);
                if (!Guid.TryParse(ConsoleController.ReadLine(), out operationId))
                {
                    ConsoleController.WriteLine("Некорректный ID операции.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            string newDescription = Input.GetString("Введите новое описание операции: ", "Описание не должно быть пустым.");

            ICommand updateCommand = new UpdateOperationDescriptionCommand(operationFacade, operationId, newDescription);
            ICommand timedUpdate = new TimedCommand(updateCommand, "Изменение описания операции");
            try
            {
                timedUpdate.Execute();
                var updated = ((UpdateOperationDescriptionCommand)updateCommand).UpdatedOperation;
                ConsoleController.WriteLine($"Описание операции обновлено. ID: {updated.Id}, новое описание: {updated.Description}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка при изменении описания операции: {ex.Message}", ConsoleColor.Red);
            }
        }


        public static void DeleteOperation(OperationFacade operationFacade)
        {
            Guid operationId;
            while (true)
            {
                ConsoleController.Write("Введите ID операции для удаления: ", ConsoleColor.Cyan);
                string idStr = ConsoleController.ReadLine();
                if (!Guid.TryParse(idStr, out operationId))
                {
                    ConsoleController.WriteLine("Некорректный ID операции.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            ICommand deleteCommand = new DeleteOperationCommand(operationFacade, operationId);
            ICommand timedDelete = new TimedCommand(deleteCommand, "Удаление операции");
            try
            {
                timedDelete.Execute();
                ConsoleController.WriteLine("Операция успешно удалена.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка удаления операции: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
