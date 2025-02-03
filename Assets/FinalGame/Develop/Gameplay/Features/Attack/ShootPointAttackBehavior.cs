using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class ShootPointAttackBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _attackTrigger;

        private readonly EntityFactory _factory;
        
        private Transform _shootPoint;
        private Transform _aim;
        private Entity _owner;

        private IReadOnlyVariable<float> _damage;

        private ICondition _condition;

        private IDisposable _disposableAttackTriggerEvent;

        public ShootPointAttackBehavior(EntityFactory factory)
        {
            _factory = factory;
        }

        public void OnInit(Entity entity)
        {
            _attackTrigger = entity.GetInstantAttackTrigger();
            _condition = entity.GetAttackCondition();
            _aim = entity.GetAimTransform();
            _shootPoint = entity.GetShootPoint();
            _owner = entity;

            _disposableAttackTriggerEvent = _attackTrigger.Subscribe(OnAttackTrigger);
        }

        public void OnDispose()
        {
            _disposableAttackTriggerEvent.Dispose();
        }

        private void OnAttackTrigger()
        {
            if (_condition.Evaluate())
            {
                Vector3 direction = (_aim.position - _shootPoint.position).normalized;
                Debug.Log(_shootPoint.position);
                _factory.CreateRocket(_shootPoint.position, direction,  _owner);
            }
        }
    }
}