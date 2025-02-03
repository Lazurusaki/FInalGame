using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using Unity.Mathematics;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Detonate
{
    public class DetonateView : EntityView
    {
        [SerializeField] private ParticleSystem _detonateEffectPrefab;
        [SerializeField] private ParticleSystem _explosionDecalPrefab;

        private Transform _entityTransform;

        private ReactiveEvent _detonateEvent;
        
        private IDisposable _disposableDetonateEvent;
        
        protected override void OnEntityInitialized(Entity entity)
        {
            _entityTransform = entity.GetTransform();

            _detonateEvent = entity.GetRadiusAttackTrigger();
            
            _disposableDetonateEvent = _detonateEvent.Subscribe(OnDetonate);
        }

        private void OnDetonate()
        {
            Instantiate(_detonateEffectPrefab, _entityTransform.position, quaternion.identity, null );
            Instantiate(_explosionDecalPrefab, _entityTransform.position, Quaternion.Euler(UnityEngine.Vector3.right * 90), null );
        }
        
        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _disposableDetonateEvent.Dispose();
        }
    }
}