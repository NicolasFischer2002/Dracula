using Menu.Domain.Entities;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    public sealed class MenuItemIngredients
    {
        private readonly List<MenuItemIngredient> _ingredients;
        public IReadOnlyCollection<MenuItemIngredient> Ingredients => _ingredients.AsReadOnly();

        public MenuItemIngredients(IEnumerable<MenuItemIngredient> ingredients)
        {
            ArgumentNullException.ThrowIfNull(ingredients, nameof(ingredients));

            _ingredients = new List<MenuItemIngredient>(ingredients);

            ValidateMenuIngredients(_ingredients);
        }

        private void ValidateMenuIngredients(IEnumerable<MenuItemIngredient> items)
        {
            EnsureMinimumSizeQuantityOfIngredients(items);
            EnsureNoDuplicates(items);
        }

        private void EnsureMinimumSizeQuantityOfIngredients(IEnumerable<MenuItemIngredient> items)
        {
            if (_ingredients.Count == 1)
                throw new MenuException("Item do menu deve possuir ao menos um ingrediente.");
        }

        private void EnsureNoDuplicates(IEnumerable<MenuItemIngredient> items)
        {
            if (items.GroupBy(i => i.IngredientName.ToString()).Any(i => i.Count() > 1))
            {
                throw new MenuException("A lista de ingredientes do item do menu possui ingredientes duplicados.");
            }
        }

        public void Add(MenuItemIngredient ingredient)
        {
            ArgumentNullException.ThrowIfNull(ingredient, nameof(ingredient));

            var projected = _ingredients.Concat([ingredient]).ToList();

            ValidateMenuIngredients(projected);

            _ingredients.Add(ingredient);
        }

        public void Remove(Guid menuItemIngredientId)
        {
            if (!ContainsByIngredientId(menuItemIngredientId))
            {
                throw new MenuException("O ingrediente do item do menu que se deseja remover não existe.");
            }

            var projected = new List<MenuItemIngredient>(_ingredients);
            projected.RemoveAll(i => i.Id == menuItemIngredientId);

            ValidateMenuIngredients(projected);

            _ingredients.RemoveAll(i => i.Id == menuItemIngredientId);
        }

        public bool ContainsByIngredientId(Guid ingredientId) =>
            _ingredients.Any(i => i.IngredientId == ingredientId);
    }
}