using SharedKernel.Formatters;

namespace Menu.Domain.Entities
{
    public sealed class MenuItemImage
    {
        public Guid Id { get; }
        public Uri Url { get; private set; }
        public string ContentType { get; private set; }
        public bool IsPrimary { get; private set; }

        internal MenuItemImage(Guid id, Uri url, string contentType, bool isPrimary)
        {
            Id = id;
            Url = url;
            ContentType = StringNormalizer.Normalize(contentType);
            IsPrimary = isPrimary;
        }

        public void MarkAsPrimary() => IsPrimary = true;
        public void UnmarkAsPrimary() => IsPrimary = false;
    }
}