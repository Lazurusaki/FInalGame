using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class PlaceBombAbilityBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _attackTrigger;

        private readonly EntityFactory _factory;
        
        private Transform _shootPoint;
        private Transform _aim;
        private Entity _owner;
        private IInputService _inputService;

        private IReadOnlyVariable<float> _damage;

        private ICondition _condition;

        private IDisposable _disposableAttackTriggerEvent;

        public PlaceBombAbilityBehavior(EntityFactory factory, IInputService inputService)
        {
            _factory = factory;
            _inputService = inputService;
        }

        public void OnInit(Entity entity)
        {
            _attackTrigger = entity.GetInstantAttackTrigger();
            _condition = entity.GetAttackCondition();
            _aim = entity.GetAimTransform();
            _owner = entity;

            _disposableAttackTriggerEvent = _attackTrigger.Subscribe(OnAttackTrigger);
        }

        private void OnAttackTrigger()
        {
            if (_condition.Evaluate())
            {
                _disposableAttackTriggerEvent.Dispose();
                _factory.CreateBomb(_aim.position, _owner);
                _owner.GetIsBombPlaced()?.Invoke();
            }
        }

        public void OnDispose()
        {
            _disposableAttackTriggerEvent.Dispose();
        }
    }
}