using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Damage
{
    public class ApplyDamageBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _takeDamageEvent;
        private ReactiveVariable<float> _health;

        private IDisposable _disposableTakeDamage;
        
        public void OnInit(Entity entity)
        {
            _takeDamageEvent = entity.GetTakeDamageEvent();
            _health = entity.GetHealth();

            _disposableTakeDamage = _takeDamageEvent.Subscribe(OnTakeDamage);
        }

        private void OnTakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentException("Negative Damage is not allowed");
            
            _health.Value = Math.Max(_health.Value - damage, 0);
        }
        
        public void OnDispose()
        {
            _disposableTakeDamage.Dispose();
        }
    }
}