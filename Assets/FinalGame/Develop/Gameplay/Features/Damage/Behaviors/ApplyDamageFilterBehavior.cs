using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Damage.Behaviors
{
    public class ApplyDamageFilterBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _takeDamageEvent;
        private ReactiveEvent<float> _takeDamageRequest;
        private ICondition _takeDamageCondition;

        private IDisposable _disposableTakeDamageRequest;
        
        public void OnInit(Entity entity)
        {
            _takeDamageEvent = entity.GetTakeDamageEvent();
            _takeDamageRequest = entity.GetTakeDamageRequest();
            _takeDamageCondition = entity.GetTakeDamageCondition();

            _disposableTakeDamageRequest = _takeDamageRequest.Subscribe(OnTakeDamageRequest);
        }

        private void OnTakeDamageRequest(float damage)
        {
            if (damage < 0)
                throw new ArgumentException("Negative Damage is not allowed");

            if (_takeDamageCondition.Evaluate())
                _takeDamageEvent.Invoke(damage);
        }

        public void OnDispose()
        {
            _disposableTakeDamageRequest.Dispose();
        }
    }
}