using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class RotationBehavior :IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;
        
        private IReadOnlyVariable<Vector3> _direction;
        private IReadOnlyVariable<float> _rotationSpeed;

        private ICondition _condition;
        
        public void OnInit(Entity entity)
        {
            _direction = entity.GetRotationDirection();
            _rotationSpeed = entity.GetRotationSpeed();
            _transform = entity.GetTransform();
            _condition = entity.GetRotationCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
                return;
            
            if (_direction.Value == Vector3.zero)
                return;
            
            var lookRotation = Quaternion.LookRotation(_direction.Value.normalized);
            var step = _rotationSpeed.Value * deltaTime;
            
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation , lookRotation ,step);
        }
    }
}