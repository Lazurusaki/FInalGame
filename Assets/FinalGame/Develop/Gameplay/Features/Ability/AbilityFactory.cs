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

        public IAbility CreateAbilityFor(Entity entity, AbilityConfig config)
        {
            switch (config)
            {
                case StatChangeAbilityConfig changeAbilityConfig:
                    return new StatChangeAbility(entity, changeAbilityConfig);
                default:
                    throw new ArgumentException("Unknown config");
            }
        }
    }
}