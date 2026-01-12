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
            ArgumentNullException.ThrowIfNull(menuItems, nameof(menuItems));

            Id = id;
            _menuItems = [.. menuItems];
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            ArgumentNullException.ThrowIfNull(menuItem);
            FailIfItemAlreadyExists(menuItem);
            _menuItems.Add(menuItem);
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            ArgumentNullException.ThrowIfNull(menuItem);
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