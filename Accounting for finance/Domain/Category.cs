using Accounting_for_finance.application.import_export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.domain
{
    /// <summary>
    /// Перечисление типов категорий: доход или расход.
    /// </summary>
    public enum CategoryType
    {
        Income,
        Expense
    }

    /// <summary>
    /// Класс, представляющий категорию (например, "Кафе" для расхода или "Зарплата" для дохода).
    /// </summary>
    public class Category
    {
        public Guid Id { get; }
        public CategoryType Type { get; }
        public string Name { get; }

        public Category(Guid id, CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не должно быть пустым.", nameof(name));

            Id = id;
            Type = type;
            Name = name;
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
