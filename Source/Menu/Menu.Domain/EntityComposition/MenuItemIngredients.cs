using Menu.Domain.Entities;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    // Berlim => Continuação
    // Construir novo Bounded Context para o Controle de Estoque de Ingredientes;
    // Será deste BC que o Id "IngredientId" de MenuItemIngredient vem.
    // No BC de Ordering, nas entidades Order e OrderItem, será necessário trocar
    // os valores monetários de decimal para o tipo Money recém criado no Shared Kernel.
    public sealed class MenuItemIngredients
    {
        private readonly List<MenuItemIngredient> _ingredients;
        public IReadOnlyCollection<MenuItemIngredient> Ingredients => _ingredients.AsReadOnly();

        public MenuItemIngredients(IEnumerable<MenuItemIngredient> ingredients)
        {
            if (ingredients is null)
                throw new MenuException("A lista de ingredientes do item do menu não pode ser nula.", string.Empty);

            _ingredients = new List<MenuItemIngredient>(ingredients);
            EnsureNoDuplicates(_ingredients);
        }

        private void EnsureNoDuplicates(IEnumerable<MenuItemIngredient> items)
        {
            var seenIds = new HashSet<Guid>();
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var it in items)
            {
                if (seenIds.Contains(it.IngredientId))
                    throw new MenuException($"Ingrediente duplicado (IngredientId): {it.IngredientId}", string.Empty);

                var name = it.IngredientName?.Name ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(name) && !seenNames.Add(name))
                    throw new MenuException($"Ingrediente duplicado (nome): {name}", string.Empty);

                seenIds.Add(it.IngredientId);
            }
        }

        public void Add(MenuItemIngredient ingredient)
        {
            if (ingredient is null) throw new MenuException("Ingrediente requerido.", string.Empty);
            if (_ingredients.Any(i => i.Id == ingredient.Id)) return; // idempotente por Id

            // projeção e validação leve
            var projected = _ingredients.Concat(new[] { ingredient }).ToList();
            EnsureNoDuplicates(projected);

            _ingredients.Add(ingredient);
        }

        public void Remove(Guid menuItemIngredientId)
        {
            var found = _ingredients.FirstOrDefault(i => i.Id == menuItemIngredientId);
            if (found is null) throw new MenuException("Ingrediente não encontrado.", menuItemIngredientId.ToString());

            if (_ingredients.Count == 1)
                throw new MenuException("Item do menu deve possuir ao menos um ingrediente.", string.Empty);

            _ingredients.Remove(found);
        }

        public bool ContainsByIngredientId(Guid ingredientId) =>
            _ingredients.Any(i => i.IngredientId == ingredientId);
    }
}