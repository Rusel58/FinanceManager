using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для создания категории (доход/расход).
    /// </summary>
    public class CreateCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly CategoryType _type;
        private readonly string _name;

        public Category CreatedCategory { get; private set; }

        public CreateCategoryCommand(CategoryFacade categoryFacade, CategoryType type, string name)
        {
            _categoryFacade = categoryFacade;
            _type = type;
            _name = name;
        }

        public void Execute()
        {
            CreatedCategory = _categoryFacade.CreateCategory(_type, _name);
        }
    }
}
