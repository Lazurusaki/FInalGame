using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using Unity.Mathematics;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Teleport
{
    public class TeleportView : EntityView
    {
        [SerializeField] private ParticleSystem _teleportStartEffectPrefab;
        [SerializeField] private ParticleSystem _teleportEndEffectPrefab;

        private Transform _entityTransform;
        
        private ReactiveEvent _teleportStartEvent;
        private ReactiveEvent _teleportEndEvent;
        
        private IDisposable _disposableTeleportStartEvent;
        private IDisposable _disposableTeleportEndEvent;
        
        protected override void OnEntityInitialized(Entity entity)
        {
            _entityTransform = entity.GetTransform();
            
            _teleportStartEvent = entity.GetTeleportStartEvent();
            _teleportEndEvent = entity.GetTeleportEndEvent();
            
            _disposableTeleportStartEvent = _teleportStartEvent.Subscribe(OnTeleportStart);
            _disposableTeleportEndEvent  = _teleportEndEvent.Subscribe(OnTeleportEnd);
        }

        private void OnTeleportStart()
        {
            Instantiate(_teleportStartEffectPrefab, _entityTransform.position, quaternion.identity, null );
        }
        
        private void OnTeleportEnd()
        {
            Instantiate(_teleportEndEffectPrefab,  _entityTransform.position, quaternion.identity, null );
        }

        protected override void OnEntityDisposed(Entity entity)
        {
            base.OnEntityDisposed(entity);

            _disposableTeleportStartEvent.Dispose();
            _disposableTeleportEndEvent.Dispose();;
        }
    }
}