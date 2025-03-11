using Accounting_for_finance.application;
using Accounting_for_finance.application.commands;
using Accounting_for_finance.application.Decorators;
using Accounting_for_finance.domain;
using Accounting_for_finance.presentation.InOut;
using Accounting_for_finance.presentation.IOController;

namespace Accounting_for_finance.presentation.DataProcess
{
    public class CategoryModule
    {
        public static void CreateCategory(CategoryFacade categoryFacade)
        {
            string name = Input.GetString("Введите название категории: ", "Название категории не должно быть пустым.");
            int typeInput = Input.GetNumberFromUser("Введите тип категории (0 - Income, 1 - Expense): ", "Значение должно быть либо 0 либо 1", 0, 1);
            CategoryType type = (typeInput == 0) ? CategoryType.Income : CategoryType.Expense;

            ICommand createCommand = new CreateCategoryCommand(categoryFacade, type, name);
            ICommand timedCreate = new TimedCommand(createCommand, "Создание категории");
            timedCreate.Execute();

            var createdCategory = ((CreateCategoryCommand)createCommand).CreatedCategory;
            ConsoleController.WriteLine($"Категория создана. ID: {createdCategory.Id}", ConsoleColor.Green);
        }

        public static void ChangeCategory(CategoryFacade categoryFacade)
        {
            Guid categoryId;
            while (true)
            {
                ConsoleController.Write("Введите ID категории для удаления: ", ConsoleColor.Cyan);
                string idStr = ConsoleController.ReadLine();
                if (!Guid.TryParse(idStr, out categoryId))
                {
                    ConsoleController.WriteLine("Некорректный ID.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            string newName = Input.GetString("Введите новое название категории: ", "Название не должно быть пустым.");

            ICommand updateCommand = new UpdateCategoryCommand(categoryFacade, categoryId, newName);
            ICommand timedUpdate = new TimedCommand(updateCommand, "Изменение категории");
            try
            {
                timedUpdate.Execute();
                ConsoleController.WriteLine($"Категория обновлена. Новый ID: {categoryId}, новое название: {newName}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка обновления категории: {ex.Message}", ConsoleColor.Red);
            }
        }

        public static void DeleteCategory(CategoryFacade categoryFacade)
        {
            Guid categoryId;
            while (true)
            {
                ConsoleController.Write("Введите ID категории для удаления: ", ConsoleColor.Cyan);
                string idStr = ConsoleController.ReadLine();
                if (!Guid.TryParse(idStr, out categoryId))
                {
                    ConsoleController.WriteLine("Некорректный ID.", ConsoleColor.Red);
                    continue;
                }
                break;
            }

            ICommand deleteCommand = new DeleteCategoryCommand(categoryFacade, categoryId);
            ICommand timedDelete = new TimedCommand(deleteCommand, "Удаление категории");
            try
            {
                timedDelete.Execute();
                ConsoleController.WriteLine("Категория успешно удалена.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleController.WriteLine($"Ошибка удаления категории: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
