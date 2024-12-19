using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Damage;
using FinalGame.Develop.Gameplay.Features.Death;
using FinalGame.Develop.Gameplay.Features.Movement;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities
{
    public class EntityFactory
    {
        private const string GhostPrefabPath = "Gameplay/Creatures/Ghost";

        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _assetsLoader;

        public EntityFactory(DIContainer container)
        {
            _container = container;
            _assetsLoader = container.Resolve<ResourcesAssetLoader>();
        }

        public Entity CreateEntity(Vector3 position)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(GhostPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddHealth(new ReactiveVariable<float>(100))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddIsDeathProcess()
                .AddSelfTriggerDamage(new ReactiveVariable<float>(20));

            // All conditions set here
            
            ICompositeCondition moveCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));
            
            ICompositeCondition rotationCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            ICompositeCondition takeDamageCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetHealth().Value <= 0));

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value))
                .Add(new FuncCondition(() => instance.GetIsDeathProcess().Value == false));


            instance
                .AddMoveCondition(moveCondition)
                .AddRotationCondition(rotationCondition)
                .AddDeathCondition(deathCondition)
                .AddTakeDamageCondition(takeDamageCondition)
                .AddSelfDestroyCondition(selfDestroyCondition);

            instance
                .AddBehavior(new CharacterControllerMovementBehavior())
                .AddBehavior(new RotationBehavior())
                .AddBehavior(new DeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new DealTouchDamageBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new SelfDestroyBehavior());
            
            instance.Initialize();
            
            return instance;
        }
    }
}