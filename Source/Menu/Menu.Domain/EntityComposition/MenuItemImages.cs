using Menu.Domain.Entities;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    public sealed class MenuItemImages
    {
        private readonly List<MenuItemImage> _itemImages;
        public IReadOnlyCollection<MenuItemImage> ItemImages => _itemImages.AsReadOnly();
        private const int MaximumNumberOfImages = 3;

        public MenuItemImages(IEnumerable<MenuItemImage> itemImages)
        {
            _itemImages = itemImages is null ? throw new MenuException(
                "A coleção de imagens do item do menu não pode ser nula.",
                string.Empty
            ) : new List<MenuItemImage>(itemImages);

            ValidateItemImages(_itemImages);
        }

        private void ValidateItemImages(IReadOnlyCollection<MenuItemImage> images)
        {
            EnsureImageCountWithinLimit(images);
            EnsureHasPrimaryImage(images);
            EnsureSinglePrimaryImage(images);
            EnsureThatTheImagesHaveUniqueUrls(images);
        }

        private void EnsureImageCountWithinLimit(IReadOnlyCollection<MenuItemImage> images)
        {
            if (images.Count > MaximumNumberOfImages)
            {
                throw new MenuException(
                    $"O número máximo permitido de imagens por item do menu é de {MaximumNumberOfImages}.",
                    images.Count().ToString()
                );
            }
        }

        private void EnsureHasPrimaryImage(IReadOnlyCollection<MenuItemImage> images)
        {
            if (!images.Any(ii => ii.IsPrimary))
            {
                throw new MenuException(
                    "Uma imagem deve ser marcada como principal para este item do menu.",
                    string.Empty
                );
            }
        }

        private void EnsureSinglePrimaryImage(IReadOnlyCollection<MenuItemImage> images)
        {
            const int MaximumNumberOfPrimaryImages = 1;

            var primaryCount = images.Count(ii => ii.IsPrimary);
            if (primaryCount > MaximumNumberOfPrimaryImages)
            {
                throw new MenuException(
                    "Apenas uma imagem pode estar marcada como principal por item do menu.",
                    primaryCount.ToString()
                );
            }
        }

        private void EnsureThatTheImagesHaveUniqueUrls(IReadOnlyCollection<MenuItemImage> images)
        {
            List<Uri>? repeatedImagesOfMenuItems = images
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

        public void SetPrimaryImage(MenuItemImage newImage)
        {
            if (newImage is null)
                throw new MenuException("A imagem atrelada ao item do menu não pode ser nula.", string.Empty);

            var projectedImages = _itemImages
                    .Where(ii => !ii.IsPrimary)
                    .ToList();

            projectedImages.Add(newImage);

            ValidateItemImages(projectedImages);

            _itemImages.Clear();
            _itemImages.AddRange(projectedImages);
        }

        public void RemoveImageByUrl(Uri imageUrl)
        {
            var imageToRemove = _itemImages.FirstOrDefault(ii => ii.Url == imageUrl);

            if (imageToRemove is null)
            {
                throw new MenuException(
                    "A imagem que deveria ser removida não está atrelada ao item do menu.",
                    imageUrl.ToString()
                );
            }

            var projectedImages = _itemImages
                .Where(ii => ii.Url != imageUrl)
                .ToList();

            ValidateItemImages(projectedImages);

            _itemImages.Clear();
            _itemImages.AddRange(projectedImages);
        }
    }
}