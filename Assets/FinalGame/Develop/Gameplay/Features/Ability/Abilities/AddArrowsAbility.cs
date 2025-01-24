using System;
using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Attack;
using FinalGame.Resources.Configs.Gameplay.Abilities;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class AddArrowsAbility : Ability, IDisposable
    {
        private AddArrowsAbilityConfig _config;
        private Entity _entity;
        
        public AddArrowsAbility(AddArrowsAbilityConfig config, Entity entity, int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _entity = entity;
        }
        
        public override void Activate()
        {
            for (int i = 0; i < CurrentLevel.Value; i++)
                AddArrowsBy(i+1);
            
            CurrentLevel.Changed += OnCurrentLevelChanged;
        }

        private void OnCurrentLevelChanged(int arg1, int newLevel)
        {
            AddArrowsBy(newLevel);
        }

        private void AddArrowsBy(int level)
        {
            List<DirectionShootConfig> directionShootConfigs = _config.GetBy(level);

            InstantShootingDirectionsArgs instantShootingDirectionsArgs = _entity.GetInstantShootingDirections();

            foreach (var directionShotConfig in directionShootConfigs)
            {
                instantShootingDirectionsArgs.Add(new InstantShotDirectionArgs(directionShotConfig.Angle,directionShotConfig.NumberOfProjectiles));
            }
        }

        public void Dispose()
        {
        }

        
    }
}
