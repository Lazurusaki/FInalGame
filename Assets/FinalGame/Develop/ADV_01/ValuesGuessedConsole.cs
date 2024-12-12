using System;

namespace FinalGame.Develop.Gameplay
{
    public class ValuesGuessedConsole : ICondition
    {
        public event Action Completed;

        public ValuesGuessedConsole(IGameModeConsole gameModeConsole)
        {
            gameModeConsole.Success += () => Completed?.Invoke();
        }
    
        public void Start()
        {
        
        }

        public void Reset()
        {
        
        }
    }
}
