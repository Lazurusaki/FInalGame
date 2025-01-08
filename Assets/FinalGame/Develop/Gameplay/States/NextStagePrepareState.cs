using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FinalGame.Develop.Gameplay.States
{
    public class NextStagePrepareState : State, IUpdatableState
    {
        private readonly EntityFactory _entityFactory;
        private Entity _nextStageTrigger;

        private IDisposable _nextStageTriggerDisposableEvent;

        public NextStagePrepareState(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public ReactiveEvent OnNextStageTriggerComplete { get; } = new();

        public override void Enter()
        {
            base.Enter();

            _nextStageTrigger = _entityFactory.CreateStartStageTrigger(Vector3.zero + Vector3.forward * 2);
            _nextStageTriggerDisposableEvent = _nextStageTrigger.GetSelfTriggerReceiver().Enter.Subscribe(OnStartStageTriggerEnter);
        }

        private void OnStartStageTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Entity entity) && entity.TryGetIsMainHero(out var isMainHero) && isMainHero.Value)
                OnNextStageTriggerComplete?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();
            
            _nextStageTriggerDisposableEvent.Dispose();
            Object.Destroy(_nextStageTrigger.gameObject);
        }

        public void Update(float deltaTime)
        {
        }
    }
}