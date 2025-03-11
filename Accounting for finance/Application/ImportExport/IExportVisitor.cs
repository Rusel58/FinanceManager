using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.import_export
{
    /// <summary>
    /// Интерфейс посетителя для экспорта доменных объектов в разные форматы.
    /// </summary>
    public interface IExportVisitor
    {
        void Visit(BankAccount bankAccount);
        void Visit(Category category);
        void Visit(Operation operation);
    }
}
