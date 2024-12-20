using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Damage
{
    public class DealRadiusDamageBehavior : IEntityInitialize, IEntityDispose
    {
        private IReadOnlyVariable<float> _radius;
        private IReadOnlyVariable<float> _damage;
        private ReactiveEvent _radiusAttackTrigger;
        private Vector3 _originPosition;
        private Entity _self;

        private IDisposable _disposableTriggerEnterEvent;

        public void OnInit(Entity entity)
        {
            _radius = entity.GetRadiusAttackRadius();
            _damage = entity.GetRadiusAttackDamage();
            _radiusAttackTrigger = entity.GetRadiusAttackTrigger();
            _originPosition = entity.GetTransform().position;
            _self = entity;

            _disposableTriggerEnterEvent = _radiusAttackTrigger.Subscribe(OnRadiusAttackTrigger);
        }

        private void OnRadiusAttackTrigger()
        {
            Collider[] colliders = Physics.OverlapSphere(_originPosition, _radius.Value);

            foreach (var collider in colliders)
            {
                var otherEntity = collider.GetComponentInParent<Entity>();
                
                if (otherEntity is not null)
                {
                    if (otherEntity == _self)
                        continue;
                    
                    Debug.Log("Radial Damage");
                    otherEntity.TryTakeDamage(_damage.Value);
                }
            }
        }

        public void OnDispose()
        {
            _disposableTriggerEnterEvent.Dispose();
        }
    }
}