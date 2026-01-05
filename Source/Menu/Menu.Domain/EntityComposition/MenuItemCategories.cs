using Menu.Domain.Enums;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    public sealed class MenuItemCategories
    {
        private readonly List<Category> _categories;
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

        public MenuItemCategories(IEnumerable<Category> categories)
        {
            ArgumentNullException.ThrowIfNull(categories, nameof(categories));

            _categories = new List<Category>(categories);

            ValidateCategories(_categories);
        }

        private void ValidateCategories(IReadOnlyCollection<Category> categories)
        {
            EnsureAtLeastOne(categories);
            EnsureNoDuplicates(categories);
        }

        private void EnsureAtLeastOne(IReadOnlyCollection<Category> categories)
        {
            if (categories.Count == 0)
                throw new MenuException("O item do menu precisa possuir ao menos uma categoria.");
        }

        private void EnsureNoDuplicates(IReadOnlyCollection<Category> categories)
        {
            var duplicate = categories
                .GroupBy(c => c)
                .FirstOrDefault(g => g.Count() > 1);

            if (duplicate != null)
                throw new MenuException($"Categoria repetida: {duplicate.Key}");
        }

        public void Add(Category category)
        {
            var projected = new List<Category>(_categories) { category };
            ValidateCategories(projected);

            _categories.Add(category);
        }

        public void Remove(Category category)
        {
            var projected = new List<Category>(_categories);
            projected.Remove(category);
            
            ValidateCategories(projected);
            
            _categories.Remove(category);
        }

        public bool Contains(Category category) => _categories.Contains(category);
        public int Count => _categories.Count;
    }
}