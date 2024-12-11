using System;

namespace FinalGame.Develop.Gameplay
{
    public class Mistake : ICondition
    {
        public event Action Completed;
    
        public Mistake(IGameMode gameMode)
        {
            gameMode.Fail += () => Completed?.Invoke();
        }
    
        public void Start()
        {
        }

        public void Reset()
        {
        }
    }
}
