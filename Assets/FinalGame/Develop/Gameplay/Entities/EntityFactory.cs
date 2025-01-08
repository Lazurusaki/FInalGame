using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Attack;
using FinalGame.Develop.Gameplay.Features.Damage;
using FinalGame.Develop.Gameplay.Features.Death;
using FinalGame.Develop.Gameplay.Features.DetectEntities;
using FinalGame.Develop.Gameplay.Features.Energy;
using FinalGame.Develop.Gameplay.Features.Movement;
using FinalGame.Develop.Gameplay.Features.Skills;
using FinalGame.Develop.Gameplay.Features.Teleport;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities
{
    public class EntityFactory
    {
        private const string GhostPrefabPath = "Gameplay/Creatures/Ghost";
        private const string ShifterPrefabPath = "Gameplay/Creatures/Shifter";
        private const string MainHeroPrefabPath = "Gameplay/Creatures/MainHero";
        private const string ArrowPrefabPath = "Gameplay/Projectiles/Arrow";

        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _assetsLoader;
        private readonly EntitiesBuffer _entitiesBuffer;

        public EntityFactory(DIContainer container)
        {
            _container = container;
            _assetsLoader = container.Resolve<ResourcesAssetLoader>();
            _entitiesBuffer = container.Resolve<EntitiesBuffer>();
        }

        public Entity CreateGhost(Vector3 position, GhostConfig config, int team)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(GhostPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(config.MoveSpeed))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddIsDeathProcess()
                .AddSelfTriggerDamage(new ReactiveVariable<float>(config.SelfTriggerDamage))
                .AddTeam(new ReactiveVariable<int>(team));
           
            //MY
            
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
        
        public Entity CreateMainHero(Vector3 position, MainHeroConfig config, int team)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(MainHeroPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(config.MoveSpeed))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddAttackTrigger()
                .AddIsAttackProcess()
                .AddInstantAttackEvent()
                .AddDamage(new ReactiveVariable<float>(config.Damage))
                .AddAttackInterval(new ReactiveVariable<float>(config.AttackInterval))
                .AddAttackCooldown()
                .AddIsDead()
                .AddIsDeathProcess()
                .AddTeam(new ReactiveVariable<int>(team))
                .AddDetectedEntitiesBuffer();

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

            ICompositeCondition attackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false))
                .Add(new FuncCondition(() => instance.GetIsAttackProcess().Value == false))
                .Add(new FuncCondition(() => instance.GetAttackCooldown().Value <= 0))
                .Add(new FuncCondition(() => instance.GetIsMoving().Value == false));

            ICompositeCondition attackCancelCondition = new CompositeCondition(LogicOperations.OrOperation)
                .Add(new FuncCondition(() => instance.GetIsMoving().Value))
                .Add(new FuncCondition(() => instance.GetIsDead().Value));

            instance
                .AddMoveCondition(moveCondition)
                .AddRotationCondition(rotationCondition)
                .AddDeathCondition(deathCondition)
                .AddTakeDamageCondition(takeDamageCondition)
                .AddSelfDestroyCondition(selfDestroyCondition)
                .AddAttackCondition(attackCondition)
                .AddAttackCancelCondition(attackCancelCondition);

            instance
                .AddBehavior(new CharacterControllerMovementBehavior())
                .AddBehavior(new RotationBehavior())
                .AddBehavior(new DeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new AttackBehavior())
                .AddBehavior(new InstantShootBehavior(this))
                .AddBehavior(new AttackCancelBehavior())
                .AddBehavior(new AttackCooldownBehavior())
                .AddBehavior(new AttackCooldownRestartBehavior())
                .AddBehavior(new AttackCooldownRestartOnMoveBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new UpdateEntitiesBuffer(_entitiesBuffer));
                
            instance.Initialize();
            
            return instance;
        }

        public Entity CreateArrow(Vector3 position, Vector3 direction, float damage)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(ArrowPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveSpeed(new ReactiveVariable<float>(3500))
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddIsMoving();
            
            ICompositeCondition moveCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => true));
            
            ICompositeCondition rotationCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => true));

            instance
                .AddMoveCondition(moveCondition)
                .AddRotationCondition(rotationCondition);

            instance
                .AddBehavior(new RigidbodyMovementBehavior())
                .AddBehavior(new ForceRotationBehavior());
            
            instance.Initialize();
            
            _entitiesBuffer.Add(instance);
            
            return instance;
        }
        
        public Entity CreateShifter(Vector3 position, int team)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(ShifterPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddHealth(new ReactiveVariable<float>(100))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddEnergy(new ReactiveVariable<float>(100))
                .AddMaxEnergy(new ReactiveVariable<float>(100))
                .AddSpendEnergyRequest()
                .AddSpendEnergyEvent()
                .AddRestoreEnergyCooldown(new ReactiveVariable<float>(1))
                .AddRadiusAttackDamage(new ReactiveVariable<float>(30))
                .AddRadiusAttackRadius(new ReactiveVariable<float>(4))
                .AddRadiusAttackTrigger()
                .AddTeleportRadius(new ReactiveVariable<float>(5))
                .AddTeleportEnergyCost(new ReactiveVariable<float>(20))
                .AddTeleportTrigger()
                .AddTeleportStartEvent()
                .AddTeleportEndEvent()
                .AddRadialAttackTeleportTrigger()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddIsDeathProcess()
                .AddTeam(new ReactiveVariable<int>(team));
            
            ICompositeCondition takeDamageCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));
            
            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetHealth().Value <= 0));

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value))
                .Add(new FuncCondition(() => instance.GetIsDeathProcess().Value == false));

            ICompositeCondition teleportCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false))
                .Add(new FuncCondition(() => instance.GetEnergy().Value >= instance.GetTeleportEnergyCost().Value));
            
            ICompositeCondition radiusAttackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            ICompositeCondition spendEnergyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false))
                .Add(new FuncCondition(() => instance.GetEnergy().Value > 0));
            
            ICompositeCondition restoreEnergyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false))
                .Add(new FuncCondition(() => instance.GetEnergy().Value < instance.GetMaxEnergy().Value));

            instance
                .AddDeathCondition(deathCondition)
                .AddTakeDamageCondition(takeDamageCondition)
                .AddSelfDestroyCondition(selfDestroyCondition)
                .AddTeleportCondition(teleportCondition)
                .AddRadiusAttackCondition(radiusAttackCondition)
                .AddSpendEnergyCondition(spendEnergyCondition)
                .AddRestoreEnergyCondition(restoreEnergyCondition);

            instance
                .AddBehavior(new DeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new RadialAttackTeleportBehavior())
                .AddBehavior(new TeleportBehavior())
                .AddBehavior(new DealRadiusDamageBehavior())
                .AddBehavior(new SpendEnergyFilterBehavior())
                .AddBehavior(new SpendEnergyBehavior())
                .AddBehavior(new GradualRestoreEnergyBehavior());
            
            instance.Initialize();
            
            return instance;
        }
        
        //Gameplay

        private const string StartStageTriggerPrefabPath = "Gameplay/Levels/StartStageTrigger";

        public Entity CreateStartStageTrigger(Vector3 position)
        {
            Entity prefab = _assetsLoader.LoadResource<Entity>(StartStageTriggerPrefabPath);
            Entity instance = Object.Instantiate(prefab, position, Quaternion.identity, null);
            instance.Initialize();
            _entitiesBuffer.Add(instance);
            
            return instance;
        }
    }
}