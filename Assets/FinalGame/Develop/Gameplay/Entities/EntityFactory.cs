using System.Collections.Generic;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Configs.Gameplay.Projectiles;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Ability;
using FinalGame.Develop.Gameplay.Features.Ability.Abilities;
using FinalGame.Develop.Gameplay.Features.Aim;
using FinalGame.Develop.Gameplay.Features.Attack;
using FinalGame.Develop.Gameplay.Features.Damage.Behaviors;
using FinalGame.Develop.Gameplay.Features.Death;
using FinalGame.Develop.Gameplay.Features.DetectEntities;
using FinalGame.Develop.Gameplay.Features.Detonate;
using FinalGame.Develop.Gameplay.Features.Energy;
using FinalGame.Develop.Gameplay.Features.Health;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Movement;
using FinalGame.Develop.Gameplay.Features.Skills;
using FinalGame.Develop.Gameplay.Features.Stats;
using FinalGame.Develop.Gameplay.Features.Teleport;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Resources.Configs.Gameplay.Structures;
using UnityEngine;
using SelfDestroyBehavior = FinalGame.Develop.Gameplay.Features.Death.SelfDestroyBehavior;

namespace FinalGame.Develop.Gameplay.Entities
{
    public class EntityFactory
    {
        private const string GhostPrefabPath = "Gameplay/Creatures/Ghost";
        private const string BomberPrefabPath = "Gameplay/Creatures/Bomber";
        private const string ShifterPrefabPath = "Gameplay/Creatures/Shifter";
        private const string CastlePrefabPath = "Gameplay/Structures/Castle";
        private const string MainHeroPrefabPath = "Gameplay/Creatures/MainHero";
        private const string ArrowPrefabPath = "Gameplay/Projectiles/Arrow";
        private const string RocketPrefabPath = "Gameplay/Projectiles/Rocket";
        private const string StartStageTriggerPrefabPath = "Gameplay/Levels/StartStageTrigger";
        private const string BombPrefabPath = "Gameplay/Projectiles/Bomb";

        private readonly LayerMask _projectileDeathLayer = 1 << 7; // 7th layer in order

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

        public Entity CreateBomber(Vector3 position, BomberConfig config, int team)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(BomberPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(Random.Range(config.MaxSpeed, config.MinSpeed)))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddDetonateDistance(new ReactiveVariable<float>(config.DetonateDistance))
                .AddRadiusAttackDamage(new ReactiveVariable<float>(config.Damage))
                .AddRadiusAttackRadius(new ReactiveVariable<float>(config.DamageRadius))
                .AddRadiusAttackTrigger()
                .AddIsDead()
                .AddIsDeathProcess()
                .AddTeam(new ReactiveVariable<int>(team));

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

            ICompositeCondition radiusAttackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            instance
                .AddMoveCondition(moveCondition)
                .AddRotationCondition(rotationCondition)
                .AddDeathCondition(deathCondition)
                .AddTakeDamageCondition(takeDamageCondition)
                .AddSelfDestroyCondition(selfDestroyCondition)
                .AddRadiusAttackCondition(radiusAttackCondition);


            //ADD BEHAVIORS
            instance
                .AddBehavior(new CharacterControllerMovementBehavior())
                .AddBehavior(new RotationBehavior())
                .AddBehavior(new DeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new DetonateBehavior())
                .AddBehavior(new SelfDestroyBehavior());

            instance.Initialize();

            return instance;
        }


        public Entity CreateMainHero(Vector3 position, MainHeroConfig config, int team)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(MainHeroPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            Dictionary<StatTypes, float> baseStats = new()
            {
                { StatTypes.MoveSpeed, config.MoveSpeed },
                { StatTypes.MaxHealth, config.MaxHealth },
                { StatTypes.AttackInterval, config.AttackInterval },
                { StatTypes.Damage, config.Damage }
            };

            Dictionary<StatTypes, float> modifiedStats = new(baseStats);

            instance
                .AddStatsEffectsList()
                .AddBaseStats(baseStats)
                .AddModifiedStats(modifiedStats)
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(baseStats[StatTypes.MoveSpeed]))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddHealth(new ReactiveVariable<float>(baseStats[StatTypes.MaxHealth]))
                .AddMaxHealth(new ReactiveVariable<float>(baseStats[StatTypes.MaxHealth]))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddAttackTrigger()
                .AddIsAttackProcess()
                .AddInstantAttackEvent()
                .AddInstantShootingDirections(new InstantShootingDirectionsArgs(new InstantShotDirectionArgs(0, 1)))
                .AddDamage(new ReactiveVariable<float>(baseStats[StatTypes.Damage]))
                .AddAttackInterval(new ReactiveVariable<float>(baseStats[StatTypes.AttackInterval]))
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
                .AddBehavior(new StatsEffectsApplierBehavior())
                .AddBehavior(new MoveSpeedModifierApplierBehavior())
                .AddBehavior(new MaxHealthModifierApplierBehavior())
                .AddBehavior(new DamageModifierApplierBehavior())
                .AddBehavior(new AttackIntervalModifierApplierBehavior())
                .AddBehavior(new CharacterControllerMovementBehavior())
                .AddBehavior(new RotationBehavior())
                .AddBehavior(new DeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new AttackBehavior())
                .AddBehavior(new MultipleInstantShootBehavior(this))
                .AddBehavior(new AttackCancelBehavior())
                .AddBehavior(new AttackCooldownBehavior())
                .AddBehavior(new AttackCooldownRestartBehavior())
                .AddBehavior(new AttackCooldownRestartOnMoveBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new UpdateEntitiesBuffer(_entitiesBuffer));
            ;
            instance.Initialize();

            return instance;
        }

        public Entity CreateArrow(Vector3 position, Vector3 direction, float damage, Entity owner)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(ArrowPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveSpeed(new ReactiveVariable<float>(3000))
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddIsMoving()
                .AddIsProjectile(true)
                .AddSelfTriggerDamage(new ReactiveVariable<float>(damage))
                .AddTeam(new ReactiveVariable<int>(owner.GetTeam().Value))
                .AddIsDead()
                .AddDeathLayer(_projectileDeathLayer)
                .AddIsTouchDeathLayer()
                .AddIsTouchAnotherTeam()
                .AddOwner(owner);

            ICompositeCondition moveCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => true));

            ICompositeCondition rotationCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => true));

            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsTouchDeathLayer().Value), 0)
                .Add(new FuncCondition(() => instance.GetIsTouchAnotherTeam().Value), 10, LogicOperations.OrOperation);

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value));

            instance
                .AddMoveCondition(moveCondition)
                .AddRotationCondition(rotationCondition)
                .AddDeathCondition(deathCondition)
                .AddSelfDestroyCondition(selfDestroyCondition);

            instance
                .AddBehavior(new RigidbodyMovementBehavior())
                .AddBehavior(new ForceRotationBehavior())
                .AddBehavior(new DeathLayerTouchDetector())
                .AddBehavior(new AnotherTeamTouchDetector())
                .AddBehavior(new InstantDeathBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new DealTouchDamageBehavior());

            instance.Initialize();

            _entitiesBuffer.Add(instance);

            return instance;
        }

        public Entity CreateRocket(Vector3 position, Vector3 direction, Entity owner)
        {
            var prefab = _assetsLoader.LoadResource<Entity>(RocketPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMoveSpeed(new ReactiveVariable<float>(3000))
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddIsMoving()
                .AddIsProjectile(true)
                .AddTeam(new ReactiveVariable<int>(owner.GetTeam().Value))
                .AddIsDead()
                .AddDeathLayer(_projectileDeathLayer)
                .AddIsTouchDeathLayer()
                .AddOwner(owner)
                .AddRadiusAttackTrigger()
                .AddRadiusAttackDamage(new ReactiveVariable<float>(owner.GetRadiusAttackDamage().Value))
                .AddRadiusAttackRadius(new ReactiveVariable<float>(owner.GetRadiusAttackRadius().Value));

            ICompositeCondition moveCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => true));

            ICompositeCondition rotationCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => true));

            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsTouchDeathLayer().Value), 0);

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value));

            ICompositeCondition radiusAttackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            instance
                .AddMoveCondition(moveCondition)
                .AddRotationCondition(rotationCondition)
                .AddDeathCondition(deathCondition)
                .AddSelfDestroyCondition(selfDestroyCondition)
                .AddRadiusAttackCondition(radiusAttackCondition);

            instance
                .AddBehavior(new RigidbodyMovementBehavior())
                .AddBehavior(new ForceRotationBehavior())
                .AddBehavior(new DeathLayerTouchDetector())
                .AddBehavior(new InstantDeathBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new DetonateOnTouchDeathLayerDeathBehavior());


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

        //ADV_FINAL
        public Entity CreateCastle(Vector3 position, CastleConfig config, LevelConfig levelConfig, int team)
        {
            Entity prefab = _assetsLoader.LoadResource<Entity>(CastlePrefabPath);
            Entity instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddHealth(new ReactiveVariable<float>(levelConfig.MaxHealth))
                .AddMaxHealth(new ReactiveVariable<float>(levelConfig.MaxHealth))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsDead()
                .AddIsDeathProcess()
                .AddTeam(new ReactiveVariable<int>(team))
                .AddInstantAttackTrigger()
                .AddInstantAttackEvent()
                .AddAttackInterval(new ReactiveVariable<float>(config.AttackCooldown))
                .AddRadiusAttackDamage(new ReactiveVariable<float>(config.Damage))
                .AddRadiusAttackRadius(new ReactiveVariable<float>(config.DamageRadius))
                .AddHealRequest()
                .AddHealEvent()
                .AddIsBombPlaced();

            ICompositeCondition takeDamageCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetHealth().Value <= 0));

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value))
                .Add(new FuncCondition(() => instance.GetIsDeathProcess().Value == false));

            ICompositeCondition attackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));
              //.Add(new FuncCondition(() => instance.GetAttackCooldown().Value <= 0))

            ICompositeCondition healCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false))
                .Add(new FuncCondition(() => instance.GetHealth().Value < instance.GetMaxHealth().Value));

            instance
                .AddDeathCondition(deathCondition)
                .AddTakeDamageCondition(takeDamageCondition)
                .AddSelfDestroyCondition(selfDestroyCondition)
                .AddAttackCondition(attackCondition)
                .AddHealCondition(healCondition);

            instance
                .AddBehavior(new DeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new MouseAimBehavior(_container.Resolve<IInputService>()))
                .AddBehavior(new MouseAttackBehavior(_container.Resolve<IInputService>()))
                .AddBehavior(new HealFilterBehavior())
                .AddBehavior(new HealBehavior());


            instance.Initialize();

            return instance;
        }

        public Entity CreateBomb(Vector3 position, Entity owner)
        {
            var config = _container.Resolve<ConfigsProviderService>().BombConfig;
            
            var prefab = _assetsLoader.LoadResource<Entity>(BombPrefabPath);

            var instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance
                .AddMaxHealth(new ReactiveVariable<float>(10))
                .AddHealth(new ReactiveVariable<float>(10))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddIsProjectile(true)
                .AddTeam(new ReactiveVariable<int>(owner.GetTeam().Value))
                .AddIsDead()
                .AddRadiusAttackDamage(new ReactiveVariable<float>(config.Damage))
                .AddRadiusAttackRadius(new ReactiveVariable<float>(config.DamageRadius))
                .AddOwner(owner)
                .AddRadiusAttackTrigger();
            
            ICompositeCondition takeDamageCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));
            
            ICompositeCondition deathCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetHealth().Value <= 0));

            ICompositeCondition selfDestroyCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value));

            ICompositeCondition radiusAttackCondition = new CompositeCondition(LogicOperations.AndOperation)
                .Add(new FuncCondition(() => instance.GetIsDead().Value == false));

            instance
                .AddDeathCondition(deathCondition)
                .AddSelfDestroyCondition(selfDestroyCondition)
                .AddRadiusAttackCondition(radiusAttackCondition)
                .AddTakeDamageCondition(takeDamageCondition);

            instance
                .AddBehavior(new InstantDeathBehavior())
                .AddBehavior(new ApplyDamageFilterBehavior())
                .AddBehavior(new ApplyDamageBehavior())
                .AddBehavior(new SelfDestroyBehavior())
                .AddBehavior(new DetonateBehavior())
                .AddBehavior(new DetonateOnEnemyTriggerEnterBehavior());


            instance.Initialize();

            _entitiesBuffer.Add(instance);

            return instance;
        }

        //Gameplay

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