using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Movement
{
    public class ForceRotationBehavior : IEntityInitialize, IEntityUpdate
    {
        private Transform _transform;

        private IReadOnlyVariable<Vector3> _direction;

        private ICondition _condition;

        public void OnInit(Entity entity)
        {
            _direction = entity.GetRotationDirection();
            _transform = entity.GetTransform();
            _condition = entity.GetRotationCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
                return;

            if (_direction.Value == Vector3.zero)
                return;

            _transform.rotation = Quaternion.LookRotation(_direction.Value);
        }
    }
}