using System;
using System.Collections;

namespace FinalGame.Develop.MainMenu
{
    public interface IMenu
    {
        public event Action<MenuItem> ItemSelected;

        public IEnumerator Start();
    }
}
