using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FinalGame.Develop.Gameplay.Features.Teleport
{
    public class TeleportBehavior : IEntityInitialize, IEntityDispose
    {
        private ICondition _condition;
        private Transform _transform;
        private ReactiveEvent _teleportTrigger;
        private IReadOnlyVariable<float> _radius;
        
        private ReactiveEvent _teleportStartEvent;
        private ReactiveEvent _teleportEndEvent;
        
        private IDisposable _teleportTriggerDispose;
        
        public void OnInit(Entity entity)
        {
            _condition = entity.GetTeleportCondition();
            _radius = entity.GetTeleportRadius();
            _transform = entity.GetTransform();
            _teleportTrigger = entity.GetTeleportTrigger();
            _teleportStartEvent = entity.GetTeleportStartEvent();
            _teleportEndEvent = entity.GetTeleportEndEvent();

            _teleportTriggerDispose = _teleportTrigger.Subscribe(OnTeleportTrigger);
        }

        private void OnTeleportTrigger()
        {
            if (_condition.Evaluate() == false) 
                return;
            
            _teleportStartEvent?.Invoke();
            _transform.position = GenerateRandomPoint();
            _teleportEndEvent?.Invoke();
        }
        
        public void OnDispose()
        {
            _teleportTriggerDispose.Dispose();
        }
        
        private Vector3 GenerateRandomPoint()
        {
            var randomCircle = Random.insideUnitCircle * _radius.Value; 
            return new Vector3(randomCircle.x, _transform.position.y, randomCircle.y) + _transform.position;
        }
    }
}