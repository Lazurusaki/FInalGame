using System;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Bounce
{
    public class BounceDetectorBehavior : IEntityInitialize, IEntityUpdate, IEntityDispose
    {
        private LayerMask _bounceLayer;
        private ReactiveEvent<RaycastHit> _bounceEvent;
        private Transform _transform;
        private TriggerReceiver _selfTriggerReceiver;

        private Vector3 _previousPosition;
        private Collider _previousObject;

        private IDisposable _triggerStayDisposable;

        private Entity _self;
        
        public void OnInit(Entity entity)
        {
            _transform = entity.GetTransform();
            _bounceLayer = entity.GetBounceLayer();
            _bounceEvent = entity.GetBounceEvent();
            _selfTriggerReceiver = entity.GetSelfTriggerReceiver();

            _self = entity;

            _triggerStayDisposable = _selfTriggerReceiver.Stay.Subscribe(OnSelfTriggerStay);
            
            _previousPosition = _transform.position;
        }

        public void OnUpdate(float deltaTime)
        {
            _previousPosition = _transform.position;
        }

        public void OnDispose()
        {
            _triggerStayDisposable.Dispose();
        }

        private void OnSelfTriggerStay(Collider otherObject)
        {
            if (_previousObject == otherObject)
                return;

            Debug.Log("OnSelfTriggerStay Start");
            
            if (Physics.Raycast(
                    _previousPosition,
                    _transform.forward,
                    out RaycastHit hit,
                    500,
                    _bounceLayer))
            {
                Debug.Log("enter");
                
                if (hit.collider == otherObject)
                {
                    
                    _previousObject = otherObject;
                    _bounceEvent.Invoke(hit);
                    Debug.DrawLine(_previousPosition, _transform.position, Color.red);
                }
            }
            
            Debug.Log("OnSelfTriggerStay End");
        }
    }
}