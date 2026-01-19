using IngredientInventory.Domain.ValueObjects;

namespace IngredientInventory.Domain.Entities
{
    // Berlim => Continuar
    public sealed class Ingredient
    {
        public Guid Id { get; }
        public IngredientName Name { get; }

        public Ingredient(Guid id, IngredientName name)
        {
            Id = id;
            Name = name;
        }
    }
}