using System;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.Configs.Gameplay.Abilities;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.Abilities;
using FinalGame.Develop.Gameplay.Features.Aim;
using FinalGame.Develop.Gameplay.Features.Input;
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
                
                case ProjectileBounceAbilityConfig projectileBounceAbilityConfig:
                    return new ProjectileBounceAbility(projectileBounceAbilityConfig, entity,
                        _container.Resolve<EntitiesBuffer>(), currentLevel);
                
                case PlaceBombAbilityConfig placeBombAbilityConfig:
                    return new PlaceBombAbility(placeBombAbilityConfig, 
                        entity, 
                        _container.Resolve<EntityFactory>(), 
                        currentLevel,
                        _container.Resolve<IInputService>(),
                        _container.Resolve<AimPresenterCreatorService>());
                
                case HealConfig healConfig:
                    return new HealAbility(entity, 
                        healConfig, 
                        currentLevel);
                
                default:
                    throw new ArgumentException("Unknown config");
            }
        }
    }
}