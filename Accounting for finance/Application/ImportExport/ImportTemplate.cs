using Accounting_for_finance.domain;
using Accounting_for_finance.presentation.IOController;
using FinancialModule.Application;
using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Абстрактный класс, реализующий шаблонный метод для импорта данных.
    /// </summary>
    public abstract class ImportTemplate
    {
        protected readonly BankAccountFacade _accountFacade;
        protected readonly CategoryFacade _categoryFacade;
        protected readonly OperationFacade _operationFacade;

        // Конструктор, принимающий уже существующие экземпляры фасадов.
        public ImportTemplate(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
        {
            _accountFacade = accountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;
        }

        /// <summary>
        /// Шаблонный метод импорта, определяющий общий алгоритм:
        /// 1) Читаем содержимое файла
        /// 2) Парсим данные (конкретная реализация зависит от формата)
        /// 3) Валидируем данные
        /// 4) Сохраняем данные (через фасады или репозитории)
        /// </summary>
        public void Import(string filePath)
        {
            // 1. Чтение содержимого файла.
            string content = ReadFile(filePath);

            // 2. Парсинг данных.
            var data = ParseData(content);

            // 3. Валидация данных.
            ValidateData(data);

            // 4. Сохранение данных.
            SaveData(data);

            ConsoleController.WriteLine($"Импорт данных из файла '{filePath}' завершён.", ConsoleColor.Green);
        }

        /// <summary>
        /// Считываем содержимое файла в виде строки.
        /// </summary>
        protected virtual string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл не найден: {filePath}");

            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Абстрактный метод парсинга данных. 
        /// Его должны переопределять конкретные импортеры (CSV, JSON, YAML и т.д.).
        /// </summary>
        protected abstract List<ImportedItem> ParseData(string content);

        /// <summary>
        /// Проверяем, что данные не пустые и в целом корректны.
        /// </summary>
        protected virtual void ValidateData(List<ImportedItem> data)
        {
            if (data == null || data.Count == 0)
                throw new Exception("Нет данных для импорта или они некорректны.");
        }

        /// <summary>
        /// Сохраняем данные (используем фасады BankAccountFacade, CategoryFacade и OperationFacade).
        /// Здесь вы решаете, как именно маппить данные на доменные объекты.
        /// </summary>
        protected virtual void SaveData(List<ImportedItem> data)
        {

            foreach (var item in data)
            {
                switch (item.EntityType)
                {
                    case "BankAccount":
                        // Создаём счёт (в примере игнорируем item.Id, чтобы не ломать существующие конструкторы)
                        _accountFacade.CreateBankAccount(item.Id, item.Name, item.Balance ?? 0m);
                        break;

                    case "Category":
                        // Преобразуем строку type => Income/Expense
                        var catType = item.Type.Equals("income", StringComparison.OrdinalIgnoreCase)
                            ? CategoryType.Income
                            : CategoryType.Expense;

                        _categoryFacade.CreateCategory(item.Id, catType, item.Name);
                        break;

                    case "Operation":
                        // Преобразуем строку type => Income/Expense
                        var opType = item.Type.Equals("income", StringComparison.OrdinalIgnoreCase)
                            ? OperationType.Income
                            : OperationType.Expense;

                        _operationFacade.CreateOperation(
                            item.Id,
                            opType,
                            item.BankAccountId ?? Guid.Empty,
                            item.Amount ?? 0m,
                            item.Date ?? DateTime.MinValue,
                            item.Description,
                            item.CategoryId ?? Guid.Empty
                        );
                        break;

                    default:
                        // Неизвестный тип
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Вспомогательный класс для хранения импортированных данных.
    /// </summary>
    public class ImportedItem
    {
        // Тот же набор колонок, что в CSV:
        // "EntityType","id","name","balance","type","bank_account_id","amount","date","description","category_id"

        [YamlMember(Alias = "EntityType")]
        [JsonPropertyName("EntityType")]
        public string EntityType { get; set; }

        [YamlMember(Alias = "id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [YamlMember(Alias = "name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [YamlMember(Alias = "balance")]
        [JsonPropertyName("balance")]
        public decimal? Balance { get; set; }

        [YamlMember(Alias = "type")]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [YamlMember(Alias = "bank_account_id")]
        [JsonPropertyName("bank_account_id")]
        public Guid? BankAccountId { get; set; }

        [YamlMember(Alias = "amount")]
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        [YamlMember(Alias = "date")]
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [YamlMember(Alias = "description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [YamlMember(Alias = "category_id")]
        [JsonPropertyName("category_id")]
        public Guid? CategoryId { get; set; }
    }
}
