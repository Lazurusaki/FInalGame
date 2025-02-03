using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Health
{
    public class HealBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _healEvent;
        private ReactiveVariable<float> _health;
        private IReadOnlyVariable<float> _maxHealth;
        private IDisposable _disposableHeal;

        public void OnInit(Entity entity)
        {
            _healEvent = entity.GetHealEvent();
            _health = entity.GetHealth();
            _maxHealth = entity.GetMaxHealth();

            _disposableHeal = _healEvent.Subscribe(OnHeal);
        }

        private void OnHeal(float value)
        {
            if (value < 0)
                throw new ArgumentException("Negative value is not allowed");

            _health.Value = Math.Min(_health.Value + value, _maxHealth.Value);
        }

        public void OnDispose()
        {
            _disposableHeal.Dispose();
        }
    }
}