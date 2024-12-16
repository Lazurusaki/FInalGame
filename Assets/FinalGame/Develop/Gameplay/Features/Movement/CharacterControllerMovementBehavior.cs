using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class CharacterControllerMovementBehavior : IEntityInitialize, IEntityUpdate
    {
        private CharacterController _characterController;

        private IReadOnlyVariable<float> _speed;
        private IReadOnlyVariable<Vector3> _direction;
        
        public void OnInit(Entity entity)
        {
            _speed = entity.GetMoveSpeed();
            _direction = entity.GetMoveDirection();
            _characterController = entity.GetCharacterController();
        }
        
        public void OnUpdate(float deltaTime)
        {
            var velocity = _direction.Value.normalized * _speed.Value;

            _characterController.Move(velocity * deltaTime);
        }

        
    }
}