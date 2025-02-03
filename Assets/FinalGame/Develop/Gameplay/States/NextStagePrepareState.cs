using System;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave.Presenters;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.UI;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;

namespace FinalGame.Develop.Gameplay.States
{
    public class NextStagePrepareState : State, IUpdatableState
    {
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;

        private WavePreparePresenter _presenter;
        private IInputService _inputService;
        
        //private Entity _nextStageTrigger;

        public NextStagePrepareState(WavePreparePresenter presenter, IInputService inputService)
        {
            _presenter = presenter;
            _inputService = inputService;
        }

        public ReactiveEvent OnNextStageTriggerComplete { get; } = new();

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
            //_presenter = _gameplayUIPresentersFactory.CreateWavePreparePresenter();
            _presenter.Enable();
            
            _presenter.Ready += OnReady;
        }

        public void OnReady()
        {
            OnNextStageTriggerComplete?.Invoke();
           //_presenter.Disable();
        }

        public override void Exit()
        {
            base.Exit();
            _presenter.Disable();
            //Object.Destroy(_nextStageTrigger.gameObject);
        }

        public void Update(float deltaTime)
        {
        }
    }
}