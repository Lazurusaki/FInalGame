using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Team;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private readonly int _team = TeamTypes.MainHero;

        private readonly EntitiesBuffer _entitiesBuffer;
        
        private readonly EntityFactory _entityFactory;
        private AIFactory _aiFactory;
        
        public MainHeroFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _aiFactory = container.Resolve<AIFactory>();
        }

        public Entity Create(Vector3 position)
        {
            Entity entity = _entityFactory.CreateMainHero(position, _team);
            AIStateMachine brain = _aiFactory.CreateMainHeroBehavior(entity, new NearestDamageableTargetSelector(entity.GetTransform(),entity.GetTeam()));

            entity.AddBehavior(new StateMachineBrainBehavior(brain));
            _entitiesBuffer.Add(entity);
            
            return entity;
        }
    }
}