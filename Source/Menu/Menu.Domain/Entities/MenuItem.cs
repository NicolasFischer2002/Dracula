using Menu.Domain.EntityComposition;
using Menu.Domain.Enums;
using Menu.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Menu.Domain.Entities
{
    public sealed class MenuItem
    {
        public Guid Id { get; }
        public MenuItemCategories Categories { get; }
        public MenuItemImages MenuItemImages { get; private set; }
        public ItemName Name { get; private set; }
        public Money Price { get; private set; }
        public MenuItemIngredients Ingredients { get; private set; }
        public PreparationTimeInMinutes? PreparationTimeInMinutes { get; private set; }
        public bool OptionalItem { get; private set; }
        public bool ItemIsActive { get; private set; }

        public MenuItem(Guid id, IEnumerable<Category> categories, IEnumerable<MenuItemImage> itemImages, 
            ItemName name, Money price, IEnumerable<MenuItemIngredient> ingredients, 
            PreparationTimeInMinutes? preparationTimeInMinutes, bool optionalItem, bool itemIsActive)
        {
            Id = id;
            Categories = new MenuItemCategories(categories);
            MenuItemImages = new MenuItemImages(itemImages);
            Name = name;
            Price = price;
            Ingredients = new MenuItemIngredients(ingredients);
            PreparationTimeInMinutes = preparationTimeInMinutes;
            OptionalItem = optionalItem;
            ItemIsActive = itemIsActive;
        }

        public void UpdateItemName(string newName) => Name = new ItemName(newName);
        public void UpdatePrice(Money newPrice) => Price = newPrice;
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