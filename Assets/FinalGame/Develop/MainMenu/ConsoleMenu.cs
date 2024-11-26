using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.MainMenu
{
    public class ConsoleMenu : IMenu
    {
        private readonly KeyCode[] _menuKeys =
        {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
            KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0     
        };

        private readonly string _header;
        private readonly List<MenuItem> _items;

        public event Action<MenuItem> ItemSelected;

        public ConsoleMenu(string header, List<MenuItem> items)
        {
            if (items.Count > _menuKeys.Length)
                throw new ArgumentOutOfRangeException($"Console menu item count can't be more than {_menuKeys.Length}");
            
            _header = header;
            _items = items;
        }

        public IEnumerator Start()
        {
            bool isItemSelected = false;
            
            Show();
            
            while (isItemSelected == false)
            {
                for (var i = 0; i < _menuKeys.Length; i++)
                {
                    if (Input.GetKeyDown(_menuKeys[i]))
                    {
                        isItemSelected = true;
                        ItemSelected?.Invoke(_items[i]);
                        break;
                    }
                }      

                yield return null;
            }
        }
        
        private void Show()
        {
            Debug.Log(_header);
            
            for (var i=0; i<_items.Count; i++)
            {
                Debug.Log($"{i+1} - {_items[i].Name}");
            }
        }
    }
}
