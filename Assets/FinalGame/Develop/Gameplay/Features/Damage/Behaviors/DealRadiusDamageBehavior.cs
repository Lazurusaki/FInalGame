using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Damage.Behaviors
{
    public class DealRadiusDamageBehavior : IEntityInitialize, IEntityDispose
    {
        private IReadOnlyVariable<float> _radius;
        private IReadOnlyVariable<float> _damage;
        private ReactiveEvent _radiusAttackTrigger;
        private Transform _originTransform;
        private Entity _self;

        private IDisposable _disposableAttackTriggerEvent;

        public void OnInit(Entity entity)
        {
            _radius = entity.GetRadiusAttackRadius();
            _damage = entity.GetRadiusAttackDamage();
            _radiusAttackTrigger = entity.GetRadiusAttackTrigger();
            _originTransform = entity.GetTransform();
            _self = entity;

            _disposableAttackTriggerEvent = _radiusAttackTrigger.Subscribe(OnRadiusAttackTrigger);
        }

        private void OnRadiusAttackTrigger()
        {
            Collider[] colliders = Physics.OverlapSphere(_originTransform.position, _radius.Value);

            foreach (var collider in colliders)
            {
                var otherEntity = collider.GetComponentInParent<Entity>();
                
                if (otherEntity is null)
                    continue;
                
                if (otherEntity == _self || otherEntity.GetTeam().Value == _self.GetTeam().Value)
                        continue;
                
                Debug.Log("Radial Damage"); 
                otherEntity.TryTakeDamage(_damage.Value);
            }
        }

        public void OnDispose()
        {
            _disposableAttackTriggerEvent.Dispose();
        }
    }
}