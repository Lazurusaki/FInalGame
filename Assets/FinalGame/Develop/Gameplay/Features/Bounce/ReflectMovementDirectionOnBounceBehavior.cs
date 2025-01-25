using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Bounce
{
    public class ReflectMovementDirectionOnBounceBehavior : IEntityInitialize, IEntityDispose
    {
        private Transform _transform;
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveEvent <RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;
        
        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _movementDirection = entity.GetMoveDirection();
            _bounceEvent = entity.GetBounceEvent();

            _bounceDisposable = _bounceEvent.Subscribe(OnBounce);
        }

        private void OnBounce(RaycastHit hit)
        {
            _movementDirection.Value = Vector3.Reflect(_movementDirection.Value, hit.normal);
            _transform.position = hit.point + hit.normal * 0.1f;
        }

        public void OnDispose()
        {
            _bounceDisposable.Dispose();
        }
    }
}