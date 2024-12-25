using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Team;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Enemy
{
    public class EnemyFactory
    {
        private readonly int _team = TeamTypes.Enemies;
        
        private readonly EntityFactory _entityFactory;
        private readonly AIFactory _aiFactory;
        
        private EntitiesBuffer _entitiesBuffer;
        
        public EnemyFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _aiFactory = container.Resolve<AIFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
        }

        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = _entityFactory.CreateGhost(position, _team);
            AIStateMachine brain = _aiFactory.CreateGhostBehavior(entity);

            entity.AddBehavior(new StateMachineBrainBehavior(brain));
            
            _entitiesBuffer.Add(entity);
            
            return entity;
        }
    }
}