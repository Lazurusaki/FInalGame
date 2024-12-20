using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class CharacterControllerMovementBehavior : IEntityInitialize, IEntityUpdate
    {
        private CharacterController _characterController;

        private IReadOnlyVariable<float> _speed;
        private IReadOnlyVariable<Vector3> _direction;
        
        private ReactiveVariable<bool> _isMoving;
        
        private ICondition _condition;
        
        public void OnInit(Entity entity)
        {
            _speed = entity.GetMoveSpeed();
            _direction = entity.GetMoveDirection();
            _characterController = entity.GetCharacterController();
            _condition = entity.GetMoveCondition();
            _isMoving = entity.GetIsMoving();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
            {
                _isMoving.Value = false;
                return;
            }

            var velocity = _direction.Value.normalized * _speed.Value;

            _isMoving.Value = velocity.magnitude > 0;
            _characterController.Move(velocity * deltaTime);
        }
    }
}