using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Bounce
{
    public class ReflectRotationOnBounceBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<Vector3> _rotation;
        private ReactiveEvent <RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;
        
        public void OnInit(Entity entity)
        {
            _rotation = entity.GetRotationDirection();
            _bounceEvent = entity.GetBounceEvent();
            _bounceDisposable = _bounceEvent.Subscribe(OnBounce);
        }

        private void OnBounce(RaycastHit hit)
        {
            _rotation.Value = Vector3.Reflect(_rotation.Value, hit.normal);
        }

        public void OnDispose()
        {
            _bounceDisposable.Dispose();
        }
    }
}