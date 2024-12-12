using System;

namespace FinalGame.Develop.Gameplay
{
    public class MistakeConsole : ICondition
    {
        public event Action Completed;
    
        public MistakeConsole(IGameModeConsole gameModeConsole)
        {
            gameModeConsole.Fail += () => Completed?.Invoke();
        }
    
        public void Start()
        {
        }

        public void Reset()
        {
        }
    }
}
