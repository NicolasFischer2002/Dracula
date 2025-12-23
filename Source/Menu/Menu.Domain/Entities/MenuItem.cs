using Menu.Domain.Enums;
using Menu.Domain.Exceptions;
using Menu.Domain.ValueObjects;

namespace Menu.Domain.Entities
{
    // Berlim => Continuar daqui:
    // Adicionar os validações e comportamentos das listas _itemImages e _ingredients
    // Adicionar validações de nullable para o ctor da lista da entidade Order.
    // Avaliar se é possível melhorar o método AddImageReplacingThePrimaryOne
    public sealed class MenuItem
    {
        public Guid Id { get; init; }
        private List<Category> _categories { get; set; }
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        private List<MenuItemImage> _itemImages { get; set; }
        public IReadOnlyCollection<MenuItemImage> ItemImages => _itemImages.AsReadOnly();
        public ItemName Name { get; private set; }
        public Price Price { get; private set; }
        private List<Ingredient> _ingredients { get; set; }
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();
        public PreparationTimeInMinutes? PreparationTimeInMinutes { get; private set; }
        public bool OptionalItem { get; private set; }
        public bool ItemIsActive { get; private set; }
        private const int MaximumNumberOfImages = 3;

        public MenuItem(Guid id, List<Category> categories, List<MenuItemImage> itemImages, 
            ItemName name, Price price, List<Ingredient>? ingredients,
            PreparationTimeInMinutes? preparationTimeInMinutes, bool optionalItem, bool itemIsActive)
        {
            Id = id;

            _categories = categories ?? throw new MenuException(
                "A coleção de categorias não pode ser nula.",
                string.Empty
            );

            _itemImages = itemImages ?? throw new MenuException(
                "A coleção de imagens do item do menu não pode ser nula.",
                string.Empty
            );

            Name = name;
            Price = price;
            _ingredients = ingredients ?? new List<Ingredient>();
            PreparationTimeInMinutes = preparationTimeInMinutes;
            OptionalItem = optionalItem;
            ItemIsActive = itemIsActive;

            ValidateCategories();
            ValidateItemImages();
        }

        private void ValidateItemImages()
        {
            EnsureImageCountWithinLimit();
            EnsureHasPrimaryImage();
            EnsureSinglePrimaryImage();
            EnsureThatTheImagesHaveUniqueUrls();
        }

        private void EnsureImageCountWithinLimit()
        {
            if (_itemImages.Count > MaximumNumberOfImages)
            {
                throw new MenuException(
                    $"O número máximo permitido de imagens por item do menu é de {MaximumNumberOfImages}.",
                    _itemImages.Count.ToString()
                );
            }
        }

        private void EnsureHasPrimaryImage()
        {
            if (!_itemImages.Any(ii => ii.IsPrimary))
            {
                throw new MenuException(
                    "Uma imagem deve ser marcada como principal para este item do menu.",
                    string.Empty
                );
            }
        }

        private void EnsureSinglePrimaryImage()
        {
            const int MaximumNumberOfPrimaryImages = 1;

            var primaryCount = _itemImages.Count(ii => ii.IsPrimary);
            if (primaryCount > MaximumNumberOfPrimaryImages)
            {
                throw new MenuException(
                    "Apenas uma imagem pode estar marcada como principal por item do menu.",
                    primaryCount.ToString()
                );
            }
        }

        private void EnsureThatTheImagesHaveUniqueUrls()
        {
            List<Uri>? repeatedImagesOfMenuItems = _itemImages
                .GroupBy(ii => ii.Url)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (repeatedImagesOfMenuItems.Any())
            {
                var duplicatedUrls = string.Join(", ", repeatedImagesOfMenuItems);
                throw new MenuException(
                    $"As imagens adicionadas ao item do menu estão repetidas: {duplicatedUrls}",
                    string.Empty
                );
            }
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

        public void AddImageReplacingThePrimaryOne(MenuItemImage newImage)
        {
            try
            {
                if (newImage.IsPrimary)
                {
                    _itemImages.RemoveAll(ii => ii.IsPrimary);
                }

                _itemImages.Add(newImage);
                ValidateItemImages();
            }
            catch (Exception ex)
            {
                _itemImages.Remove(newImage);
                throw new MenuException(
                    $"Não foi possível adicionar a nova imagem ao item do menu. {ex.Message}",
                    ex.Message
                );
            }
        }
    }
}