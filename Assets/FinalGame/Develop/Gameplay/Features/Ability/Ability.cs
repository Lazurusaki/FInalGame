using System;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Ability
{
    public abstract class Ability
    {
        private ReactiveVariable<int> _currentLevel;

        protected Ability(string id ,int currentLevel , int maxLevel)
        {
            if (currentLevel > maxLevel)
                throw new ArgumentException(nameof(currentLevel));
            
            ID = id;
            _currentLevel =  new ReactiveVariable<int>(currentLevel);
            MaxLevel = maxLevel;
        }

        public string ID { get; }
        public int MaxLevel { get;  }

        public IReadOnlyVariable<int> CurrentLevel => _currentLevel;

        public void AddLevel(int level)
        {
            int temp = _currentLevel.Value + level;

            if (temp > MaxLevel)
                throw new ArgumentException(nameof(level));

            _currentLevel.Value = temp;
        }
        public abstract void Activate();
    }
}