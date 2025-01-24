using System;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.Abilities;
using FinalGame.Resources.Configs.Gameplay.Abilities;

namespace FinalGame.Develop.Gameplay.Features.Ability
{
    public class AbilityFactory
    {
        private DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public Ability CreateAbilityFor(Entity entity, AbilityConfig config, int currentLevel)
        {
            switch (config)
            {
                case StatChangeAbilityConfig statChangeAbilityConfig:
                    return new StatChangeAbility(entity, statChangeAbilityConfig, currentLevel);
                
                case AddArrowsAbilityConfig addArrowsAbilityConfig:
                    return new AddArrowsAbility(addArrowsAbilityConfig, entity, currentLevel);
                
                default:
                    throw new ArgumentException("Unknown config");
            }
        }
    }
}