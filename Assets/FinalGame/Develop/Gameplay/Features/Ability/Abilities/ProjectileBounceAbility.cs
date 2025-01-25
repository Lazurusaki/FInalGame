using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Bounce;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class ProjectileBounceAbility : Ability, IDisposable
    {
        private ProjectileBounceAbilityConfig _config;
        private readonly Entity _owner;
        private readonly EntitiesBuffer _creaturesBuffer;

        public ProjectileBounceAbility(
            ProjectileBounceAbilityConfig config,
            Entity owner,
            EntitiesBuffer creaturesBuffer,
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _owner = owner;
            _creaturesBuffer = creaturesBuffer;
        }

        public override void Activate()
        {
            _creaturesBuffer.Added += OnCreatureAdded;
        }

        private void OnCreatureAdded(Entity entity)
        {
            if (entity.TryGetIsProjectile(out bool isProjectile) 
                && isProjectile
                && entity.TryGetOwner(out Entity projectileOwner) 
                && projectileOwner == _owner)
            {
                entity
                    .AddBounceCount(new ReactiveVariable<int>(_config.GetBounceCountBy(CurrentLevel.Value)))
                    .AddBounceEvent()
                    .AddBounceLayer(_config.BoucneLayer);

                entity
                    .GetDeathCondition()
                    .Add(new FuncCondition(() => entity.GetBounceCount().Value + 1 == 0) , 5);
                    
                entity    
                    .AddBehavior(new BounceDetectorBehavior())
                    .AddBehavior(new BounceCountDecreaseBehavior())
                    .AddBehavior(new ReflectMovementDirectionOnBounceBehavior())
                    .AddBehavior(new ReflectRotationOnBounceBehavior());
            }
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}