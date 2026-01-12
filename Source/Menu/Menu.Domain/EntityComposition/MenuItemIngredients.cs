using Menu.Domain.Entities;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    public sealed class MenuItemIngredients
    {
        private readonly HashSet<MenuItemIngredient> _ingredients;
        public IReadOnlyCollection<MenuItemIngredient> Ingredients => _ingredients.AsReadOnly();

        public MenuItemIngredients(IEnumerable<MenuItemIngredient> ingredients)
        {
            MenuException.ThrowIfNull(ingredients, nameof(ingredients));

            _ingredients = [.. new HashSet<MenuItemIngredient>(ingredients)];

            ValidateMenuIngredients(_ingredients);
        }

        private void ValidateMenuIngredients(IEnumerable<MenuItemIngredient> items)
        {
            MenuException.ThrowIfNull(items, nameof(items));
            EnsureMinimumSizeQuantityOfIngredients(items);
        }

        private static void EnsureMinimumSizeQuantityOfIngredients(IEnumerable<MenuItemIngredient> items)
        {
            MenuException.ThrowIf(
                items.Any(),
                "Item do menu deve possuir ao menos um ingrediente."
            );
        }

        private void FailIfMenuItemIngredientAlreadyExists(MenuItemIngredient item)
        {
            MenuException.ThrowIfNull(item, nameof(item));

            MenuException.ThrowIf(
                _ingredients.Contains(item),
                "Categoria já existente no item do menu."
            );
        }

        public void Add(MenuItemIngredient ingredient)
        {
            ArgumentNullException.ThrowIfNull(ingredient, nameof(ingredient));
            FailIfMenuItemIngredientAlreadyExists(ingredient);

            var projected = _ingredients.Concat([ingredient]).ToList();

            ValidateMenuIngredients(projected);
            _ingredients.Add(ingredient);
        }

        public void Remove(Guid menuItemIngredientId)
        {
            MenuException.ThrowIf(
                !ContainsByIngredientId(menuItemIngredientId),
                "O ingrediente do item do menu que se deseja remover não existe."
            );

            var projected = new List<MenuItemIngredient>(_ingredients);
            projected.RemoveAll(i => i.Id == menuItemIngredientId);

            ValidateMenuIngredients(projected);
            _ingredients.RemoveWhere(i => i.Id == menuItemIngredientId);
        }

        public bool ContainsByIngredientId(Guid ingredientId) =>
            _ingredients.Any(i => i.IngredientId == ingredientId);
    }
}