using System;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Gameplay.Features.GameModes;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;

namespace FinalGame.Develop.Gameplay.States
{
    public class StageProcessState : State, IUpdatableState
    {
        private LevelConfig _levelConfig;
        private GameModesFactory _gameModesFactory;
        private StageProviderService _stageProviderService;

        private WaveGameMode _gameMode;

        private IDisposable _gameModeEnterDisposableEvent;
        
        public StageProcessState(LevelConfig levelConfig, GameModesFactory gameModesFactory, StageProviderService stageProviderService)
        {
            _levelConfig = levelConfig;
            _gameModesFactory = gameModesFactory;
            _stageProviderService = stageProviderService;
        }

        public ReactiveEvent StageComplete { get; } = new();

        public override void Enter()
        {
            base.Enter();

            _gameMode = _gameModesFactory.CreateWaveGameMode();
            _gameMode.Start(_levelConfig.WaveConfigs[_stageProviderService.NextStageIndex.Value]);

            _gameModeEnterDisposableEvent = _gameMode.Ended.Subscribe(OnGameModeEnded);
        }

        private void OnGameModeEnded()
        {
            _stageProviderService.CompleteStage();
            
            if (_stageProviderService.HasNextStage())
                _stageProviderService.SwitchToNext();
            
            StageComplete.Invoke();
        }

        public override void Exit()
        {
            base.Exit();
            
            _gameModeEnterDisposableEvent?.Dispose();
            _gameMode.Cleanup();
            _gameMode = null;
        }

        public void Update(float deltaTime)
        {
        }
    }
}