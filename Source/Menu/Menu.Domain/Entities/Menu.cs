using Menu.Domain.Exceptions;

namespace Menu.Domain.Entities
{
    public sealed class Menu
    {
        public Guid Id { get; }
        private readonly HashSet<MenuItem> _menuItems;
        public IReadOnlyCollection<MenuItem> MenuItems => _menuItems.AsReadOnly();

        public Menu(Guid id, IEnumerable<MenuItem> menuItems)
        {
            MenuException.ThrowIfNull(menuItems, "Os itens do menu não podem ser nulos.");

            Id = id;
            _menuItems = [.. menuItems];
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            MenuException.ThrowIfNull(menuItem, "Adição de um item no menu: valor nulo identificado.");
            FailIfItemAlreadyExists(menuItem);
            _menuItems.Add(menuItem);
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            MenuException.ThrowIfNull(menuItem, "Remoção de um item do menu: valor nulo identificado.");
            FailIfItemNotFound(menuItem);
            _menuItems.Remove(menuItem);
        }

        public void RemoveMenuItem(Guid menuItemId)
        {
            var menuItem = FindMenuItem(menuItemId);
            _menuItems.Remove(menuItem);
        }

        private MenuItem FindMenuItem(Guid menuItemId)
        {
            return _menuItems.FirstOrDefault(m => m.Id == menuItemId)
                ?? throw new MenuException("Item não encontrado no menu.");
        }

        private void FailIfItemAlreadyExists(MenuItem menuItem)
        {
            MenuException.ThrowIf(
                _menuItems.Contains(menuItem),
                "O item já está presente no menu."
            );
        }

        private void FailIfItemNotFound(MenuItem menuItem)
        {
            MenuException.ThrowIf(
                !_menuItems.Contains(menuItem),
                "Item não encontrado no menu."
            );
        }
    }
}