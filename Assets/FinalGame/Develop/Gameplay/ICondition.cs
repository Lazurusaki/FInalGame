using System;

namespace FinalGame.Develop.Gameplay
{
    public interface ICondition
    {
        public event Action Completed;

        public void Start();
        public void Reset();
    }
}
