using Menu.Domain.Exceptions;
using Menu.Domain.ValueObjects;

namespace Menu.Domain.EntityComposition
{
    // Berlim => Continuação
    // Definir campos e do VO Ingredient.
    // Definir validações para esta classe.
    public sealed class MenuItemIngredients
    {
        private readonly List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();

        public MenuItemIngredients(IEnumerable<Ingredient> ingredients)
        {
            if (ingredients is null)
                throw new MenuException("A lista de ingredientes do item do menu não pode ser nula.", string.Empty);

            _ingredients = new List<Ingredient>(ingredients);
        }
    }
}