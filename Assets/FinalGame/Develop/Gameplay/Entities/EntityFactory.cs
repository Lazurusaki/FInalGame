﻿using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Attack;
using FinalGame.Develop.Gameplay.Features.Damage;
using FinalGame.Develop.Gameplay.Features.Death;
using FinalGame.Develop.Gameplay.Features.Energy;
using FinalGame.Develop.Gameplay.Features.Movement;
using FinalGame.Develop.Gameplay.Features.Skills;
using FinalGame.Develop.Gameplay.Features.Teleport;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using Unity.VisualScripting;
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

        public EntityFactory(DIContainer container)
        {
            _container = container;
            _assetsLoader = container.Resolve<ResourcesAssetLoader>();
        }

        public Entity CreateGhost(Vector3 position)
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
        
        public Entity CreateMainHero(Vector3 position)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(MainHeroPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(6))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddHealth(new ReactiveVariable<float>(100))
                .AddMaxHealth(new ReactiveVariable<float>(100))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddAttackTrigger()
                .AddIsAttackProcess()
                .AddInstantAttackEvent()
                .AddDamage(new ReactiveVariable<float>(20))
                .AddAttackInterval(new ReactiveVariable<float>(0.1f))
                .AddAttackCooldown()
                .AddIsDead()
                .AddIsDeathProcess();

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
                .AddBehavior(new SelfDestroyBehavior());
                
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
            
            return instance;
        }
        
        public Entity CreateShifter(Vector3 position)
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
                .AddIsDeathProcess();
            
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
    }
}