using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class PlayerInputState : State, IUpdatableState
    {
        private IInputService _inputService;
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Vector3> _rotationDirection;

        public PlayerInputState(IInputService inputService, ReactiveVariable<Vector3> movementDirection, ReactiveVariable<Vector3> rotationDirection)
        {
            _inputService = inputService;
            _movementDirection = movementDirection;
            _rotationDirection = rotationDirection;
        }

        public void Update(float deltaTime)
        {
            _movementDirection.Value = _inputService.Direction;
            _rotationDirection.Value = _movementDirection.Value;
        }

        public override void Exit()
        {
            base.Exit();
            
            _movementDirection.Value = Vector3.zero;;
        }
    }
}