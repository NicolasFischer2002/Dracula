namespace Menu.Domain.Entities
{
    public sealed class MenuItemImage
    {
        public Guid Id { get; init; }
        public Uri Url { get; private set; }
        public string ContentType { get; private set; }
        public bool IsPrimary { get; private set; }

        internal MenuItemImage(Guid id, Uri url, string contentType, bool isPrimary)
        {
            Id = id;
            Url = url;
            ContentType = contentType;
            IsPrimary = isPrimary;
        }

        internal void MarkAsPrimary() => IsPrimary = true;
        internal void UnmarkAsPrimary() => IsPrimary = false;
    }
}