using System;

namespace FinalGame.Develop.Gameplay
{
    public class ValuesGuessed : ICondition
    {
        public event Action Completed;

        public ValuesGuessed(IGameMode gameMode)
        {
            gameMode.Success += () => Completed?.Invoke();
        }
    
        public void Start()
        {
        
        }

        public void Reset()
        {
        
        }
    }
}
