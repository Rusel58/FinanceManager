using FinancialModule.Application;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Реализация импорта данных из YAML.
    /// Ожидается, что YAML-файл содержит массив объектов с ключами:
    /// "EntityType", "id", "name", "balance", "type", "bank_account_id", "amount", "date", "description", "category_id"
    /// </summary>
    public class YamlImport : ImportTemplate
    {
        public YamlImport(BankAccountFacade accountFacade, CategoryFacade categoryFacade, OperationFacade operationFacade)
            : base(accountFacade, categoryFacade, operationFacade)
        {
        }

        protected override List<ImportedItem> ParseData(string content)
        {
            try
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(NullNamingConvention.Instance)
                    .WithTypeConverter(new EmptyStringToGuidConverter())
                    .WithTypeConverter(new EmptyStringToDateTimeConverter())
                    .WithTypeConverter(new EmptyStringToDecimalConverter())
                    .Build();

                var data = deserializer.Deserialize<List<ImportedItem>>(content);
                return data ?? new List<ImportedItem>();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка парсинга YAML: " + ex.Message);
            }
        }
    }
}