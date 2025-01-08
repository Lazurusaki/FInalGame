using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Team;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private const int Team = TeamTypes.MainHero;

        private readonly EntitiesBuffer _entitiesBuffer;
        
        private readonly EntityFactory _entityFactory;
        private readonly AIFactory _aiFactory;
        private readonly MainHeroHolderService _mainHeroHolderService;
        
        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aiFactory = container.Resolve<AIFactory>();
            _mainHeroHolderService = container.Resolve<MainHeroHolderService>();
        }

        public Entity Create(Vector3 position, MainHeroConfig config)
        {
            Entity entity = _entityFactory.CreateMainHero(position, config, Team);
            AIStateMachine brain = _aiFactory.CreateMainHeroBehavior(entity, new NearestDamageableTargetSelector(entity.GetTransform(),entity.GetTeam()));

            entity.AddIsMainHero(new ReactiveVariable<bool>(true));
            entity.AddBehavior(new StateMachineBrainBehavior(brain));
            _mainHeroHolderService.Register(entity);
            _entitiesBuffer.Add(entity);
            
            return entity;
        }
    }
}