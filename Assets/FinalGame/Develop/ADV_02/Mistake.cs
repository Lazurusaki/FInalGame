using System;
using FinalGame.Develop.Gameplay;

namespace FinalGame.Develop.ADV_02
{
    public class Mistake : ICondition
    {
        public event Action Completed;
    
        public Mistake(ISequenceGameMode gameMode)
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