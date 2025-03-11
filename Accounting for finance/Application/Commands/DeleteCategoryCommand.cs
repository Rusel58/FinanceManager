using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application.commands
{
    /// <summary>
    /// Команда для удаления категории.
    /// </summary>
    public class DeleteCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly Guid _categoryId;

        public DeleteCategoryCommand(CategoryFacade categoryFacade, Guid categoryId)
        {
            _categoryFacade = categoryFacade;
            _categoryId = categoryId;
        }

        public void Execute()
        {
            _categoryFacade.DeleteCategory(_categoryId);
        }
    }
}
