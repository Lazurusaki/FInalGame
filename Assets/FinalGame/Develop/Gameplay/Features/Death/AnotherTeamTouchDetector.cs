using System;
using FinalGame.Develop.Gameplay.AI.Sensors;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Death
{
    public class AnotherTeamTouchDetector : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<bool> _isTouchAnotherTeam;
        private TriggerReceiver _selfTriggerReceiver;
        private ReactiveVariable<int> _sourceTeam;

        private int _enteredCounter;

        private IDisposable _enterDisposable;
        private IDisposable _exitDisposable;
        
        public void OnInit(Entity entity)
        {
            _selfTriggerReceiver = entity.GetSelfTriggerReceiver();
            _isTouchAnotherTeam = entity.GetIsTouchAnotherTeam();
            _sourceTeam = entity.GetTeam();

            _enterDisposable = _selfTriggerReceiver.Enter.Subscribe(OnSelfTriggerEnter);
            _exitDisposable = _selfTriggerReceiver.Exit.Subscribe(OnSelfTriggerExit);
        }

        private void  OnSelfTriggerEnter(Collider other)
        {
            if (other.TryGetEntity(out Entity entity))
            {
                if (entity.HasTeam() == false)
                    return;

                if (entity.MatchWithTeam(_sourceTeam.Value) == false)
                {
                    _enteredCounter++;
                    _isTouchAnotherTeam.Value = true;
                }
            }
        }

        private void OnSelfTriggerExit(Collider other)
        {
            if (other.TryGetEntity(out Entity entity))
            {
                if (entity.HasTeam() == false)
                    return;

                if (entity.MatchWithTeam(_sourceTeam.Value) == false)
                {
                    _enteredCounter--;
                    
                    if (_enteredCounter == 0)
                        _isTouchAnotherTeam.Value = true;
                }
            }
        }

        public void OnDispose()
        {
            _enterDisposable.Dispose();
            _exitDisposable.Dispose();
        }
    }
}