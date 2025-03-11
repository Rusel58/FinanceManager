using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для обновления названия категории.
    /// </summary>
    public class UpdateCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly Guid _categoryId;
        private readonly string _newName;

        public UpdateCategoryCommand(CategoryFacade categoryFacade, Guid categoryId, string newName)
        {
            _categoryFacade = categoryFacade;
            _categoryId = categoryId;
            _newName = newName;
        }

        public void Execute()
        {
            _categoryFacade.UpdateCategory(_categoryId, _newName);
        }
    }
}
