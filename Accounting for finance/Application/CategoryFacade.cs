using Accounting_for_finance.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.application
{
    /// <summary>
    /// Фасад для работы с категориями. Предоставляет методы для создания, получения, обновления и удаления категорий.
    /// </summary>
    public class CategoryFacade
    {
        // Временное хранилище категорий в памяти.
        private readonly Dictionary<Guid, Category> _categories = new Dictionary<Guid, Category>();

        /// <summary>
        /// Создание новой категории с помощью фабрики.
        /// </summary>
        public Category CreateCategory(CategoryType type, string name)
        {
            Category category = DomainFactory.CreateCategory(type, name);
            _categories[category.Id] = category;
            return category;
        }

        public Category CreateCategory(Guid id, CategoryType type, string name)
        {
            Category category = DomainFactory.CreateCategory(id, type, name);
            _categories[category.Id] = category;
            return category;
        }

        /// <summary>
        /// Получение категории по идентификатору.
        /// </summary>
        public Category GetCategory(Guid id)
        {
            if (_categories.TryGetValue(id, out var category))
                return category;
            throw new Exception("Категория не найдена.");
        }

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        public IEnumerable<Category> GetAllCategories()
        {
            return _categories.Values;
        }

        /// <summary>
        /// Удаление категории по идентификатору.
        /// </summary>
        public void DeleteCategory(Guid id)
        {
            if (!_categories.ContainsKey(id))
                throw new Exception("Категория не найдена.");
            _categories.Remove(id);
        }

        /// <summary>
        /// Обновление названия категории.
        /// Поскольку класс Category является иммутабельным, создаётся новый объект с сохранением идентификатора.
        /// </summary>
        public Category UpdateCategory(Guid id, string newName)
        {
            if (!_categories.ContainsKey(id))
                throw new Exception("Категория не найдена.");

            Category oldCategory = _categories[id];

            Category updatedCategory = new Category(oldCategory.Id, oldCategory.Type, newName);
            _categories[id] = updatedCategory;
            return updatedCategory;
        }
    }
}
