using Menu.Domain.Exceptions;

namespace Menu.Domain.Entities
{
    // Berlim => Continuação
    // Adicionar métodos de Adicionar/Remover itens
    public sealed class Menu
    {
        public Guid Id { get; }
        private readonly HashSet<MenuItem> _menuItems;
        public IReadOnlyCollection<MenuItem> MenuItems => _menuItems.AsReadOnly();

        public Menu(Guid id, IEnumerable<MenuItem> menuItems)
        {
            ArgumentNullException.ThrowIfNull(menuItems, nameof(menuItems));

            if (HasDuplicates(menuItems))
            {
                throw new MenuException("A lista de itens do menu possui itens duplicados.");
            }

            Id = id;
            _menuItems = new HashSet<MenuItem>(menuItems);
        }

        private static bool HasDuplicates(IEnumerable<MenuItem> items) =>
            items.GroupBy(i => i).Any(group => group.Count() > 1);
    }
}