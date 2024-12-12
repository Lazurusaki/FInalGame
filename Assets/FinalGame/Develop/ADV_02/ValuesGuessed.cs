using System;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.ADV_02
{
    public class ValuesGuessed : ICondition
    {
        public event Action Completed;

        public ValuesGuessed(ISequenceGameMode gameMode)
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