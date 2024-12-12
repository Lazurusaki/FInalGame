using System;
using System.Collections;
using Unity.VisualScripting;

namespace FinalGame.Develop.Gameplay
{
    public interface IGameModeConsole
    {
        public event Action Success;
        public event Action Fail;
        
        public IEnumerator Start();
    }
}
