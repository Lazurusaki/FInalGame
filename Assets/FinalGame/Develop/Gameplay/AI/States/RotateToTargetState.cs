using System.Collections.Generic;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class RotateToTargetState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly ITargetSelector _targetSelector;
        private readonly Transform _transform;
        private readonly List<Entity> _detectedTargets;

        public RotateToTargetState(ReactiveVariable<Vector3> rotationDirection, ITargetSelector targetSelector, Transform transform, List<Entity> detectedTargets)
        {
            _rotationDirection = rotationDirection;
            _targetSelector = targetSelector;
            _transform = transform;
            _detectedTargets = detectedTargets;
        }

        public void Update(float deltaTime)
        {
            if (_targetSelector.TrySelectTarget(_detectedTargets, out Entity selectedTarget))
                if (selectedTarget.TryGetTransform(out Transform targetTransform))
                    _rotationDirection.Value = targetTransform.position - _transform.position;
        }
    }
}