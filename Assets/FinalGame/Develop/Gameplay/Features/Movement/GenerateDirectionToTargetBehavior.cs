using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class GenerateDirectionToTargetBehavior : IEntityInitialize, IEntityUpdate
    {
        private IReadOnlyVariable<Entity> _target;
        private Transform _transform;
        private ReactiveVariable<Vector3> _moveDirection;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _transform = entity.GetTransform();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_target.Value is null)
            {
                if (_moveDirection.Value != Vector3.zero)
                    _moveDirection.Value = Vector3.zero;
                
                return;
            }

            _moveDirection.Value = _target.Value.GetTransform().position - _transform.position;
        }
    }
}