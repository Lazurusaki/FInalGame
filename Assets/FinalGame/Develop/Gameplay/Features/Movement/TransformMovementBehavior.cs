using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class TransformMovementBehavior : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<Vector3> _direction;
        private IReadOnlyVariable<float> _speed;
        private ReactiveVariable<bool> _isMoving;
        private ICondition _moveCondition;
        private Entity _entity;
        
        
        public void OnInit(Entity entity)
        {
            _speed = entity.GetMoveSpeed();
            _transform = entity.GetTransform();
            _direction = entity.GetMoveDirection();
            _isMoving = entity.GetIsMoving();
            _moveCondition = entity.GetMoveCondition();

            _entity = entity;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_moveCondition.Evaluate() == false)
                return;
            
            Vector3 velocity = _direction.Value.normalized * _speed.Value;
            
            _isMoving.Value = velocity.magnitude > 0;
            
            _transform.Translate(velocity * deltaTime);
        }
    }
}