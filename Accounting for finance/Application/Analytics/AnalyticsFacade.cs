using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting_for_finance.domain;
using FinancialModule.Application;

namespace Accounting_for_finance.application.Analytics
{
        /// <summary>
        /// Фасад (или сервис) для аналитики операций:
        /// подсчёт разницы доход/расход за период,
        /// группировка по категориям и т. д.
        /// </summary>
        public class AnalyticsFacade
        {
            private readonly OperationFacade _operationFacade;
            private readonly CategoryFacade _categoryFacade;

            public AnalyticsFacade(OperationFacade operationFacade, CategoryFacade categoryFacade)
            {
                _operationFacade = operationFacade;
                _categoryFacade = categoryFacade;
            }

            /// <summary>
            /// Подсчёт разницы доходов и расходов за указанный период (startDate включительно, endDate включительно).
            /// Возвращает (сумма доходов - сумма расходов).
            /// </summary>
            public decimal GetIncomeExpenseDifference(DateTime startDate, DateTime endDate)
            {
                var operations = _operationFacade.GetAllOperations()
                    .Where(op => op.Date >= startDate && op.Date <= endDate);

                decimal totalIncome = 0;
                decimal totalExpense = 0;

                foreach (var op in operations)
                {
                    if (op.Type == OperationType.Income)
                        totalIncome += op.Amount;
                    else
                        totalExpense += op.Amount;
                }

                return totalIncome - totalExpense;
            }

            /// <summary>
            /// Группировка доходов/расходов по категориям за период.
            /// Возвращает словарь: 
            ///   ключ   = Guid категории, 
            ///   значение = сумма по этой категории.
            /// Можно дополнительно получить название категории через CategoryFacade.
            /// </summary>
            public Dictionary<Guid, decimal> GetSumByCategory(DateTime startDate, DateTime endDate)
            {
                var operations = _operationFacade.GetAllOperations()
                    .Where(op => op.Date >= startDate && op.Date <= endDate);

                // Для каждой категории аккумулируем сумму
                var result = new Dictionary<Guid, decimal>();

                foreach (var op in operations)
                {
                    if (!result.ContainsKey(op.CategoryId))
                    {
                        result[op.CategoryId] = 0;
                    }
                    result[op.CategoryId] += op.Amount;
                }

                return result;
            }

            /// <summary>
            /// Пример метода, который возвращает удобную структуру для вывода 
            /// (ID категории + название + сумма).
            /// </summary>
            public List<(Guid categoryId, string categoryName, decimal totalAmount)>
                GetSumByCategoryDetailed(DateTime startDate, DateTime endDate)
            {
                var grouped = GetSumByCategory(startDate, endDate);
                var result = new List<(Guid, string, decimal)>();

                foreach (var kvp in grouped)
                {
                    var categoryId = kvp.Key;
                    decimal sum = kvp.Value;

                    // Получаем название категории
                    var category = _categoryFacade.GetCategory(categoryId);
                    result.Add((categoryId, category.Name, sum));
                }

                return result;
            }
        }
    
}
