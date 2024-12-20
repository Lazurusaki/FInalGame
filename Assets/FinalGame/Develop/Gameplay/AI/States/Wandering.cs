using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class Wandering : State , IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Vector3> _rotationDirection;

        private readonly float _cooldownBetweenMovement;

        private float _time;

        public Wandering(ReactiveVariable<Vector3> movementDirection, ReactiveVariable<Vector3> rotationDirection, float cooldownBetweenMovement)
        {
            _movementDirection = movementDirection;
            _rotationDirection = rotationDirection;
            _cooldownBetweenMovement = cooldownBetweenMovement;
        }

        public override void Enter()
        {
            base.Enter();

            _movementDirection.Value = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            _rotationDirection.Value = _movementDirection.Value;
            _time = 0;
        }

        public override void Exit()
        {
            base.Exit();
            
            _movementDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time > _cooldownBetweenMovement)
            {
                
                GenerateNewDirection();
                _time = 0;
            }
        }

        private void GenerateNewDirection()
        {
            _movementDirection.Value = Quaternion.Euler(0, Random.Range(-30, 30), 0) * (-_movementDirection.Value).normalized;
            _rotationDirection.Value = _movementDirection.Value;
        }
    }
}