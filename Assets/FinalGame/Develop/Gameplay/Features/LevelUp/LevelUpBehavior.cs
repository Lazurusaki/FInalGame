using FinalGame.Develop.Configs.Gameplay;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.LevelUp
{
    public class LevelUpBehavior: IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _level;
        private ExperienceForLevelUplConfig _config;

        public LevelUpBehavior(ExperienceForLevelUplConfig config)
        {
            _config = config;
        }

        public float CurrentLimitForExp => _config.GetExperienceFor(_level.Value + 1);

        public void OnInit(Entity entity)
        {
            _experience = entity.GetExperience();
            _level = entity.GetLevel();

            _experience.Changed += OnExperienceChanged;
        }

        public void OnDispose()
        {
            _experience.Changed -= OnExperienceChanged;
        }

        private void OnExperienceChanged(float arg1, float newExp)
        {
            if (arg1 >= newExp)
                return;
            
            if (_level.Value >= _config.MaxLevel)
                return;
            
            while (newExp >= CurrentLimitForExp)
            {
                newExp -= CurrentLimitForExp;
                _level.Value++;
                
                if (_level.Value == _config.MaxLevel)
                    break;
            }

            _experience.Value = newExp;
        }
    }
}
