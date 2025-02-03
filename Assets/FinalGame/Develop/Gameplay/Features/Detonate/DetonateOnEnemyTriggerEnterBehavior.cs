using System;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using Unity.VisualScripting;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Detonate
{
    public class DetonateOnEnemyTriggerEnterBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _detonateTrigger;
        private Entity _self;

        private IDisposable _disposableAttackTriggerEvent;

        public void OnInit(Entity entity)
        {
            _detonateTrigger = entity.GetRadiusAttackTrigger();
            _self = entity;

            _disposableAttackTriggerEvent = entity.GetSelfTriggerReceiver().Enter.Subscribe(OnTrigger);
        }

        private void OnTrigger(Collider obj)
        {
            var otherEntity = obj.GetComponentInParent<Entity>();
                
            if (otherEntity is null)
                return;
                
            if (otherEntity == _self || otherEntity.GetTeam().Value == _self.GetTeam().Value)
                return;
            
            _detonateTrigger.Invoke();
        }

        public void OnDispose()
        {
            _disposableAttackTriggerEvent.Dispose();
        }
    }
}