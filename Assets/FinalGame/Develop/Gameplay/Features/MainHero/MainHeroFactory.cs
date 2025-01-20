using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability;
using FinalGame.Develop.Gameplay.Features.Team;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private const int Team = TeamTypes.MainHero;

        private readonly EntitiesBuffer _entitiesBuffer;
        private readonly AbilityFactory _abilityFactory;
        private readonly EntityFactory _entityFactory;
        private readonly AIFactory _aiFactory;
        private readonly MainHeroHolderService _mainHeroHolderService;
        private readonly ConfigsProviderService _configs;
        
        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aiFactory = container.Resolve<AIFactory>();
            _mainHeroHolderService = container.Resolve<MainHeroHolderService>();
            _abilityFactory = container.Resolve<AbilityFactory>();
            _configs = container.Resolve<ConfigsProviderService>();
        }

        public Entity Create(Vector3 position, MainHeroConfig config)
        {
            Entity entity = _entityFactory.CreateMainHero(position, config, Team);
            AIStateMachine brain = _aiFactory.CreateMainHeroBehavior(entity, new NearestDamageableTargetSelector(entity.GetTransform(),entity.GetTeam()));

            AbilityList abilityList = new AbilityList();
            //IAbility testAbility = _abilityFactory.CreateAbilityFor(entity, _configs.AbilitiesConfigContainer.GetConfigBy("MoveSpeed2"));
            //abilityList.Add(testAbility);
            
            entity
                .AddIsMainHero(new ReactiveVariable<bool>(true))
                .AddAbilityList(abilityList);

            entity
                .AddBehavior(new StateMachineBrainBehavior(brain))
                .AddBehavior(new AbilityOnAddActivatorBehavior());
            
            _mainHeroHolderService.Register(entity);
            _entitiesBuffer.Add(entity);
            
            return entity;
        }
    }
}