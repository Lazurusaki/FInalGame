using System;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Loot;
using FinalGame.Develop.Gameplay.Features.Team;
using FinalGame.Develop.Utils.Conditions;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Enemy
{
    public class EnemyFactory
    {
        private readonly int _team = TeamTypes.Enemies;
        
        private readonly EntityFactory _entityFactory;
        private readonly AIFactory _aiFactory;
        
        private EntitiesBuffer _entitiesBuffer;
        private DropLootService _dropLootService;
        
        public EnemyFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _aiFactory = container.Resolve<AIFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
            _dropLootService = container.Resolve<DropLootService>();
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
                default:
                    throw new ArgumentException("Unknown config");
            }

            entity.AddBehavior(new StateMachineBrainBehavior(brain));
            
            AddDropLootBehaviorTo(entity);
            
            _entitiesBuffer.Add(entity);
            
            return entity;
        }

        private void AddDropLootBehaviorTo(Entity entity)
        {
            ICompositeCondition dropLootCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => entity.GetIsDead().Value))
                .Add(new FuncCondition(() => entity.GetIsLootDropped().Value == false));

            entity
                .AddIsLootDropped()
                .AddDropLootCondition(dropLootCondition);

            entity.GetSelfDestroyCondition()
                .Add(new FuncCondition(() => entity.GetIsLootDropped().Value));

            entity.AddBehavior(new DropLootBehavior(_dropLootService));
        }
    }
}