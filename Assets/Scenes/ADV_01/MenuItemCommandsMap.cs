using System;
using System.Collections.Generic;

namespace FinalGame.Develop.MainMenu
{
    public class MenuItemCommandsMap
    {
        private readonly Dictionary<MenuItem, IMenuCommand> _map = new();

        public MenuItemCommandsMap(IMenu menu)
        {
            menu.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(MenuItem item)
        {
            if (_map.ContainsKey(item))
                _map[item].Execute();
            else
                throw new InvalidOperationException("Menu item is not bound");
        }

        public void Bind(MenuItem menuItem, IMenuCommand command)
        {
            _map.Add(menuItem,command);
        }
    }
}
