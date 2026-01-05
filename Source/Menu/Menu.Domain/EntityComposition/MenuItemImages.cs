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
            ArgumentNullException.ThrowIfNull(itemImages, nameof(itemImages));

            _itemImages = new List<MenuItemImage>(itemImages);

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
                throw new MenuException("Uma imagem deve ser marcada como principal para este item do menu.");
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
                throw new MenuException($"As imagens adicionadas ao item do menu estão repetidas: {duplicatedUrls}");
            }
        }

        public void SetPrimaryImage(MenuItemImage newImage)
        {
            ArgumentNullException.ThrowIfNull(newImage, nameof(newImage));

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
            ArgumentNullException.ThrowIfNull(imageUrl, nameof(imageUrl));

            var newImages = _itemImages.Where(ii => ii.Url != imageUrl).ToList();
            ValidateItemImages(newImages);

            _itemImages.Clear();
            _itemImages.AddRange(newImages);
        }
    }
}