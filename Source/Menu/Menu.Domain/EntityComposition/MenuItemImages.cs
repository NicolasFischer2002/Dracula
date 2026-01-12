using Menu.Domain.Entities;
using Menu.Domain.Exceptions;

namespace Menu.Domain.EntityComposition
{
    public sealed class MenuItemImages
    {
        private readonly HashSet<MenuItemImage> _itemImages;
        public IReadOnlyCollection<MenuItemImage> ItemImages => _itemImages.AsReadOnly();
        private const int MaximumNumberOfImages = 3;

        public MenuItemImages(IEnumerable<MenuItemImage> itemImages)
        {
            MenuException.ThrowIfNull(itemImages, nameof(itemImages));
            _itemImages = [.. new HashSet<MenuItemImage>(itemImages)];
            EnsureValidState(_itemImages);
        }

        private void EnsureValidState(IReadOnlyCollection<MenuItemImage> images)
        {
            MenuException.ThrowIfNull(images, nameof(images));

            EnsureImageCountWithinLimit(images);
            EnsureHasPrimaryImage(images);
            EnsureSinglePrimaryImage(images);
            EnsureThatTheImagesHaveUniqueUrls(images);
        }

        private static void EnsureImageCountWithinLimit(IReadOnlyCollection<MenuItemImage> images)
        {
            MenuException.ThrowIf(
                images.Count > MaximumNumberOfImages,
                $"O número máximo permitido de imagens por item do menu é de {MaximumNumberOfImages}."
            );
        }

        private static void EnsureHasPrimaryImage(IReadOnlyCollection<MenuItemImage> images)
        {
            MenuException.ThrowIf(
                !images.Any(ii => ii.IsPrimary),
                "Uma imagem deve ser marcada como principal para este item do menu."
            );
        }

        private static void EnsureSinglePrimaryImage(IReadOnlyCollection<MenuItemImage> images)
        {
            const int MaximumNumberOfPrimaryImages = 1;
            var primaryCount = images.Count(ii => ii.IsPrimary);
            
            MenuException.ThrowIf(
                primaryCount > MaximumNumberOfPrimaryImages,
                "Apenas uma imagem pode estar marcada como principal por item do menu."
            );
        }

        private static void EnsureThatTheImagesHaveUniqueUrls(IReadOnlyCollection<MenuItemImage> images)
        {
            var duplicates = images
                .GroupBy(ii => ii.Url)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            MenuException.ThrowIf(
                duplicates.Any(),
                $"As imagens adicionadas ao item do menu estão repetidas: {string.Join(", ", duplicates)}"
            );
        }

        public void SetPrimaryImage(MenuItemImage newPrimaryImage)
        {
            MenuException.ThrowIfNull(newPrimaryImage, nameof(newPrimaryImage));

            if (!newPrimaryImage.IsPrimary)
            {
                newPrimaryImage.MarkAsPrimary();
            }

            FailIfImageNotFound(newPrimaryImage);

            var projected = _itemImages
                .Where(ii => !ii.IsPrimary)
                .Append(newPrimaryImage)
                .ToHashSet();

            EnsureValidState(projected);

            _itemImages.Clear();
            _itemImages.UnionWith(projected);
        }

        public void RemoveImageByUrl(Uri imageUrl)
        {
            MenuException.ThrowIfNull(imageUrl, nameof(imageUrl));
            FailIfImageNotFound(imageUrl);

            var projected = _itemImages
                .Where(ii => !ii.Url.Equals(imageUrl))
                .ToHashSet();

            EnsureValidState(projected);
            _itemImages.Clear();
            _itemImages.UnionWith(projected);
        }

        private void FailIfImageNotFound(MenuItemImage image)
        {
            MenuException.ThrowIf(
                !_itemImages.Contains(image), 
                "Imagem não encontrada no item."
            );
        }

        private void FailIfImageNotFound(Uri imageUrl)
        {
            MenuException.ThrowIf(
                _itemImages.FirstOrDefault(ii => ii.Url == imageUrl) is null,
                "Imagem não encontrada no item."
            );
        }
    }
}