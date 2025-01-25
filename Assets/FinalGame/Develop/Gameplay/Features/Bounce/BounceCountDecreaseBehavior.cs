using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Bounce
{
    public class BounceCountDecreaseBehavior: IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<int> _bounceCount;
        private ReactiveEvent<RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;
        
        public void OnInit(Entity entity)
        {
            _bounceCount = entity.GetBounceCount();
            _bounceEvent = entity.GetBounceEvent();

            _bounceDisposable = _bounceEvent.Subscribe(OnBounce);
        }

        private void OnBounce(RaycastHit obj)
        {
            _bounceCount.Value--;
        }

        public void OnDispose()
        {
            _bounceDisposable.Dispose();
        }
    }
}