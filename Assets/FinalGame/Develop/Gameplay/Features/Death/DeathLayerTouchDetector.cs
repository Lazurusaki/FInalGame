using System;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Death
{
    public class DeathLayerTouchDetector : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<bool> _isTouchDeathLayer;
        private LayerMask _deathLayer;
        private TriggerReceiver _selfTriggerReceiver;

        private int _enteredCounter;
        private Entity _self;

        private IDisposable _enterDisposable;
        private IDisposable _exitDisposable;
        
        public void OnInit(Entity entity)
        {
            _selfTriggerReceiver = entity.GetSelfTriggerReceiver();
            _isTouchDeathLayer = entity.GetIsTouchDeathLayer();
            _deathLayer = entity.GetDeathLayer();
            _self = entity;

            _enterDisposable = _selfTriggerReceiver.Enter.Subscribe(OnSelfTriggerEnter);
            _exitDisposable = _selfTriggerReceiver.Exit.Subscribe(OnSelfTriggerExit);
        }

        private void  OnSelfTriggerEnter(Collider other)
        {
            if (other.MatchWithLayer(_deathLayer))
            {
                _enteredCounter++;
                _isTouchDeathLayer.Value = true;
            }
        }

        private void OnSelfTriggerExit(Collider other)
        {
            if (other.MatchWithLayer(_deathLayer))
            {
                _enteredCounter--;
                
                if (_enteredCounter == 0) 
                    _isTouchDeathLayer.Value = false;
            }
        }

        public void OnDispose()
        {
            _enterDisposable.Dispose();
            _exitDisposable.Dispose();
        }
    }
}