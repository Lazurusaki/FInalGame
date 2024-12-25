using System.Collections.Generic;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class AutoAimAttackState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly ITargetSelector _targetSelector;
        private readonly Transform _transform;
        private readonly List<Entity> _detectedTargets;
        private readonly ICondition _attackCondition;

        private readonly ReactiveEvent _attackEvent;
        
        public AutoAimAttackState(ReactiveVariable<Vector3> rotationDirection,
            ITargetSelector targetSelector,
            Transform transform,
            List<Entity> detectedTargets,
            ReactiveEvent attackEvent,
            ICondition attackCondition)
        {
            _rotationDirection = rotationDirection;
            _targetSelector = targetSelector;
            _transform = transform;
            _detectedTargets = detectedTargets;
            _attackEvent = attackEvent;
            _attackCondition = attackCondition;
        }

        public void Update(float deltaTime)
        {
            if (_targetSelector.TrySelectTarget(_detectedTargets, out Entity selectedTarget) == false)
                return;

            if (selectedTarget.TryGetTransform(out Transform targetTransform) == false)
                return;

            _rotationDirection.Value = targetTransform.position - _transform.position;

            _attackEvent?.Invoke();
        }
    }
}