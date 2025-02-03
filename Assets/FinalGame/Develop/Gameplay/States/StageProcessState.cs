using System;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Aim;
using FinalGame.Develop.Gameplay.Features.Attack;
using FinalGame.Develop.Gameplay.Features.GameModes;
using FinalGame.Develop.Gameplay.Features.GameModes.Wave;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.MainHero;
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
        private IInputService _inputService;
        private AimPresenterCreatorService _aimPresenterCreatorService;
        private MainHeroHolderService _mainHeroHolderService;
        private EntityFactory _entityFactory;

        private IDisposable _gameModeEnterDisposableEvent;
        
        public StageProcessState(LevelConfig levelConfig, GameModesFactory gameModesFactory, StageProviderService stageProviderService, IInputService inputService, AimPresenterCreatorService aimPresenterCreatorService, MainHeroHolderService mainHeroHolderService , EntityFactory entityFactory)
        {
            _levelConfig = levelConfig;
            _gameModesFactory = gameModesFactory;
            _stageProviderService = stageProviderService;
            _inputService = inputService;
            _aimPresenterCreatorService = aimPresenterCreatorService;
            _mainHeroHolderService = mainHeroHolderService;
            _entityFactory = entityFactory;
        }

        public ReactiveEvent StageComplete { get; } = new();

        public override void Enter()
        {
            base.Enter();
            _gameMode = _gameModesFactory.CreateWaveGameMode();
            _gameMode.Start(_levelConfig.WaveConfigs[_stageProviderService.NextStageIndex.Value]);

            _gameModeEnterDisposableEvent = _gameMode.Ended.Subscribe(OnGameModeEnded);

            _inputService.IsEnabled = true;
            _aimPresenterCreatorService.Enable();
            _mainHeroHolderService.MainHero.AddBehavior(new ShootPointAttackBehavior(_entityFactory));
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
            _mainHeroHolderService.MainHero.TryRemoveBehavior<ShootPointAttackBehavior>();
            _gameModeEnterDisposableEvent?.Dispose();
            _gameMode.Cleanup();
            _gameMode = null;
            _aimPresenterCreatorService.Disable();
        }

        public void Update(float deltaTime)
        {
        }
    }
}