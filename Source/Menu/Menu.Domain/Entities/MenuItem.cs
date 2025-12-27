using Menu.Domain.Enums;
using Menu.Domain.Exceptions;
using Menu.Domain.ValueObjects;

namespace Menu.Domain.Entities
{
    // Berlim => Continuar daqui:
    // Provavelmente será necessário criar um objeto para encapsular as regras de Categories e _ingredients
    public sealed class MenuItem
    {
        public Guid Id { get; init; }
        private List<Category> _categories { get; set; }
        public MenuItemImages MenuItemImages { get; private set; }
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        public ItemName Name { get; private set; }
        public Price Price { get; private set; }
        private List<Ingredient> _ingredients { get; set; }
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();
        public PreparationTimeInMinutes? PreparationTimeInMinutes { get; private set; }
        public bool OptionalItem { get; private set; }
        public bool ItemIsActive { get; private set; }

        public MenuItem(Guid id, IEnumerable<Category> categories, IEnumerable<MenuItemImage> itemImages, 
            ItemName name, Price price, IEnumerable<Ingredient>? ingredients, 
            PreparationTimeInMinutes? preparationTimeInMinutes, bool optionalItem, bool itemIsActive)
        {
            Id = id;

            _categories = categories is null ? throw new MenuException(
                "A coleção de categorias não pode ser nula.",
                string.Empty
            ) : new List<Category>(categories);

            MenuItemImages = new MenuItemImages(itemImages);
            Name = name;
            Price = price;
            _ingredients = ingredients is null ? new List<Ingredient>() : new List<Ingredient>(ingredients);
            PreparationTimeInMinutes = preparationTimeInMinutes;
            OptionalItem = optionalItem;
            ItemIsActive = itemIsActive;

            ValidateCategories();
        }

        private void ValidateCategories()
        {
            if (_categories.Count == 0)
            {
                throw new MenuException(
                    $"O item do menu '{Name}' precisa possuir ao menos uma categoria.",
                    string.Empty
                );
            }
        }

        public void AddCategory(Category category)
        {
            if (!_categories.Contains(category))
            {
                _categories.Add(category);
            }
        }

        public void RemoveCategory(Category category)
        {
            if (_categories.Count == 1)
            {
                throw new MenuException(
                    "O item do menu deve possui ao menos uma categoria",
                    string.Empty
                );
            }

            _categories.Remove(category);
        }

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