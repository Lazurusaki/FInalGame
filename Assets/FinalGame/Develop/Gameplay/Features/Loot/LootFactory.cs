using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class LootFactory
    {
        private readonly EntityFactory _entityFactory;
        private readonly EntitiesBuffer _entitiesBuffer;

        public LootFactory(DIContainer container)
        {
            _entityFactory = container.Resolve<EntityFactory>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
        }

        public Entity CreateExperienceLoot(Entity prefab, Vector3 position, float experience)
        {
            Entity pickupable = _entityFactory.CreatePickupable(prefab, position);

            pickupable
                .AddExperience(new ReactiveVariable<float>(experience))
                .AddBehavior(new CollectExperienceToTargetBehavior());
            
            _entitiesBuffer.Add(pickupable);

            return pickupable;
        }
        
        public Entity CreateHealthLoot(Entity prefab, Vector3 position, float Health)
        {
            Entity pickupable = _entityFactory.CreatePickupable(prefab, position);

            pickupable
                .AddHealth(new ReactiveVariable<float>(Health))
                .AddBehavior(new CollectHealthToTargetBehavior());
            
            _entitiesBuffer.Add(pickupable);

            return pickupable;
        }
        
        public Entity CreateCoinsLoot(Entity prefab, Vector3 position, int coins)
        {
            Entity pickupable = _entityFactory.CreatePickupable(prefab, position);

            pickupable
                .AddCoins(new ReactiveVariable<int>(coins))
                .AddBehavior(new CollectCoinsToTargetBehavior());
            
            _entitiesBuffer.Add(pickupable);

            return pickupable;
        }
    }
}