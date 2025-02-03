using System;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.AI.Sensors;
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

        public Entity Create(Vector3 position, CreatureConfig config)
        {
            Entity entity;
            AIStateMachine brain;

            switch (config)
            {
                case GhostConfig ghostConfig:
                    entity = _entityFactory.CreateGhost(position, ghostConfig, _team);
                    brain = _aiFactory.CreateGhostBehavior(entity);
                    break;
                case BomberConfig bomberConfig:
                    entity = _entityFactory.CreateBomber(position, bomberConfig, _team);
                    brain = _aiFactory.CreateBomberBehavior(entity, new NearestDamageableTargetSelector(entity.GetTransform(), entity.GetTeam()));
                    break;
                
                default:
                    throw new ArgumentException("Unknown config");
            }

            entity.AddBehavior(new StateMachineBrainBehavior(brain));
            
            _entitiesBuffer.Add(entity);
            
            return entity;
        }
    }
}