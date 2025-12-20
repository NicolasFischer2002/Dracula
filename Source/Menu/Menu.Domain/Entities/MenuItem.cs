using Menu.Domain.Enums;
using Menu.Domain.ValueObjects;

namespace Menu.Domain.Entities
{
    public sealed class MenuItem
    {
        public Guid Id { get; init; }
        public Category Category { get; private set; }
        public ItemName Name { get; private set; }
        public Price Price { get; private set; }
        private List<Ingredient> _ingredients { get; set; }
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();
        public PreparationTimeInMinutes? PreparationTimeInMinutes { get; private set; }
        public bool OptionalItem { get; private set; }
        public bool ItemIsActive { get; private set; }

        public MenuItem(Guid id, Category category, ItemName name, Price price, 
            List<Ingredient> ingredients, PreparationTimeInMinutes? preparationTimeInMinutes,
            bool optionalItem, bool itemIsActive)
        {
            Id = id;
            Category = category;
            Name = name; 
            Price = price;
            _ingredients = ingredients;
            PreparationTimeInMinutes = preparationTimeInMinutes;
            OptionalItem = optionalItem;
            ItemIsActive = itemIsActive;
        }

        public void UpdateItemCategory(Category newCategory) => Category = newCategory;
        public void UpdateItemName(string newName) => Name = new ItemName(newName);
        public void UpdatePrice(decimal newPrice) => Price = new Price(newPrice);
        public void UpdatePreparationTimeInMinutes(int preparationTimeInMinutes)
        {
            PreparationTimeInMinutes = new PreparationTimeInMinutes(preparationTimeInMinutes);
        }
        public void MarkAsOptional() => OptionalItem = true;
        public void UnmarkAsOptional() => OptionalItem = false;
        public void ActivateItem() => ItemIsActive = true;
        public void DeactivateItem() => ItemIsActive = false;
    }
}