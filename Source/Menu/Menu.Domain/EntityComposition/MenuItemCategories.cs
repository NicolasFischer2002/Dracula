using Menu.Domain.Enums;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    public sealed class MenuItemCategories
    {
        private readonly HashSet<Category> _categories;
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

        public MenuItemCategories(IEnumerable<Category> categories)
        {
            ArgumentNullException.ThrowIfNull(categories, nameof(categories));

            _categories = [.. new HashSet<Category>(categories)];
            EnsureValidState(categories);
        }

        public void Add(Category category)
        {
            FailIfCategoryAlreadyExists(category);

            var projected = new HashSet<Category>(_categories) { category };
            EnsureValidState(projected);
            _categories.Add(category);
        }

        public void Remove(Category category)
        {
            FailIfCategoryNotFound(category);

            var projected = new HashSet<Category>(_categories);
            projected.Remove(category);
            
            EnsureValidState(projected);
            _categories.Remove(category);
        }

        private static void EnsureValidState(IEnumerable<Category> categories)
        {
            EnsureAtLeastOne(categories);
        }

        private static void EnsureAtLeastOne(IEnumerable<Category> categories)
        {
            ArgumentNullException.ThrowIfNull(categories);

            MenuException.ThrowIf(
                !categories.Any(),
                "O item do menu precisa possuir ao menos uma categoria."
            );
        }

        private void FailIfCategoryAlreadyExists(Category category)
        {
            MenuException.ThrowIf(
                _categories.Contains(category),
                "Categoria já existente no item do menu."
            );
        }

        private void FailIfCategoryNotFound(Category category)
        {
            MenuException.ThrowIf(
                !_categories.Contains(category),
                "Categoria não encontrada no item do menu."
            );
        }

        public bool Contains(Category category) => _categories.Contains(category);
        public int Count => _categories.Count;
    }
}