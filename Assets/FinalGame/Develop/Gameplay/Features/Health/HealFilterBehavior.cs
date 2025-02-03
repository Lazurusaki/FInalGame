using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Health
{
    public class HealFilterBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _healEvent;
        private ReactiveEvent<float> _healRequest;
        private ICondition _healCondition;

        private IDisposable _disposableHealRequest;
        
        public void OnInit(Entity entity)
        {
            _healEvent = entity.GetHealEvent();
            _healRequest = entity.GetHealRequest();
            _healCondition = entity.GetHealCondition();

            _disposableHealRequest = _healRequest.Subscribe(OnHealRequest);
        }

        private void OnHealRequest(float value)
        {
            if (value < 0)
                throw new ArgumentException("Negative value is not allowed");

            if (_healCondition.Evaluate())
                _healEvent.Invoke(value);
        }

        public void OnDispose()
        {
            _disposableHealRequest.Dispose();
        }
    }
}