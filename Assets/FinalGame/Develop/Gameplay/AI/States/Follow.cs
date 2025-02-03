using System.Linq;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Team;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class Follow : State, IUpdatableState
    {
        private readonly EntitiesBuffer _entitiesBuffer;
        
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        
        private readonly Entity _self;
        private readonly ITargetSelector _targetSelector;
        
        private Entity _target;
        
        public Follow(EntitiesBuffer entitiesBuffer, Entity self, ReactiveVariable<Vector3> movementDirection, ReactiveVariable<Vector3> rotationDirection, ITargetSelector targetSelector)
        {
            _entitiesBuffer = entitiesBuffer;
            _self = self;
            _movementDirection = movementDirection;
            _rotationDirection = rotationDirection;
            _targetSelector = targetSelector;
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _target = null;
        }

        public override void Exit()
        {
            base.Exit();
            _movementDirection.Value = Vector3.zero;
        }
        
        public void Update(float deltaTime)
        {
            if (_target is not null)
            {
                _movementDirection.Value = (_target.GetTransform().position - _self.GetTransform().position).normalized;
                _rotationDirection.Value = _movementDirection.Value;
                return;
            }

            if (_targetSelector.TrySelectTarget(_entitiesBuffer.Elements, out  _target) == false)
                _movementDirection.Value = Vector3.zero;
        }
    }
}