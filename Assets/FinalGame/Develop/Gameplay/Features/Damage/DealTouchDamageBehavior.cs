using System;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Damage
{
    public class DealTouchDamageBehavior : IEntityInitialize, IEntityDispose
    {
        private TriggerReceiver _triggerReceiver;
        private IReadOnlyVariable<float> _damage;

        private IDisposable _disposableTriggerEnter;
        
        public void OnInit(Entity entity)
        {
            _triggerReceiver = entity.GetSelfTriggerReceiver();
            _damage = entity.GetSelfTriggerDamage();

            _disposableTriggerEnter = _triggerReceiver.Enter.Subscribe(OnTriggerEnter);
        }

        private void OnTriggerEnter(Collider colloder)
        {
            var otherEntity = colloder.GetComponentInParent<Entity>();

            if (otherEntity is not null)
            {
                Debug.Log("sds");
                otherEntity.TryTakeDamage(_damage.Value);
            }
        }

        public void OnDispose()
        {
            
        }
    }
}