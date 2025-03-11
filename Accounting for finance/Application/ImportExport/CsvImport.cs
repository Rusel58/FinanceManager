using FinancialModule.Application;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Пример реализации импорта из CSV.
    /// Предполагается, что в CSV-файле первая строка — заголовок:
    /// EntityType;id;name;balance;type;bank_account_id;amount;date;description;category_id
    /// </summary>
    public class CsvImport : ImportTemplate
    {
        public CsvImport(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
            : base(accountFacade, categoryFacade, operationFacade)
        {
        }

        protected override List<ImportedItem> ParseData(string content)
        {
            // Разбиваем на строки
            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<ImportedItem>();

            if (lines.Length < 2)
            {
                // Нет заголовка или данных
                return result;
            }

            // Пропускаем первую строку (заголовок)
            for (int i = 1; i < lines.Length; i++)
            {
                var fields = lines[i].Split(';');
                // columns:
                // 0 - EntityType
                // 1 - id
                // 2 - name
                // 3 - balance
                // 4 - type
                // 5 - bank_account_id
                // 6 - amount
                // 7 - date
                // 8 - description
                // 9 - category_id

                if (fields.Length < 10)
                    continue; // строка некорректна или неполна

                var item = new ImportedItem();

                // 0 - EntityType
                item.EntityType = fields[0].Replace("\"", "").Trim();

                // 1 - id
                if (Guid.TryParse(fields[1].Replace("\"", ""), out Guid parsedId))
                    item.Id = parsedId;

                // 2 - name
                item.Name = fields[2].Replace("\"", "").Trim();

                // 3 - balance
                if (decimal.TryParse(fields[3].Replace("\"", ""), out decimal parsedBalance))
                    item.Balance = parsedBalance;

                // 4 - type
                item.Type = fields[4].Replace("\"", "").Trim();  // "income" / "expense" или пусто

                // 5 - bank_account_id
                if (Guid.TryParse(fields[5].Replace("\"", ""), out Guid accId))
                    item.BankAccountId = accId;

                // 6 - amount
                if (decimal.TryParse(fields[6].Replace("\"", ""), out decimal parsedAmount))
                    item.Amount = parsedAmount;

                // 7 - date
                if (DateTime.TryParse(fields[7].Replace("\"", ""), out DateTime parsedDate))
                    item.Date = parsedDate;

                // 8 - description
                item.Description = fields[8].Replace("\"", "").Trim();

                // 9 - category_id
                if (Guid.TryParse(fields[9].Replace("\"", ""), out Guid catId))
                    item.CategoryId = catId;

                result.Add(item);
            }

            return result;
        }
    }
}
