using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.Results;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Gameplay.UI;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly LevelConfig _levelConfig;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;
        private readonly WalletService _walletService;
        private readonly GameResultsStatsService _gameResultsStatsService;

        public WinState(IInputService inputService, 
            PlayerDataProvider playerDataProvider, 
            LevelConfig levelConfig, 
            SceneSwitcher sceneSwitcher,  
            GameplayUIPresentersFactory gameplayUIPresentersFactory,
            WalletService walletService,
            GameResultsStatsService gameResultsStatsService) : base(inputService)
        {
            _playerDataProvider = playerDataProvider;
            _levelConfig = levelConfig;
            _sceneSwitcher = sceneSwitcher;
            _gameplayUIPresentersFactory = gameplayUIPresentersFactory;
            _walletService = walletService;
            _gameResultsStatsService = gameResultsStatsService;
        }

        public override void Enter()
        {
            base.Enter();

            _gameResultsStatsService.Add(GameResults.Win);
            _walletService.Add(_levelConfig.Reward.CurrencyType,_levelConfig.Reward.Value);
            var presenter = _gameplayUIPresentersFactory.CreateVictoryResultPresenter(_levelConfig);
            presenter.Enable();
            presenter.Continue += OnContinue;
            _playerDataProvider.Save();
        }

        private void OnContinue()
        {
            _sceneSwitcher.ProcessSwitchSceneFor(new GameplaySceneOutputArgs(new MainMenuSceneInputArgs()));
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _sceneSwitcher.ProcessSwitchSceneFor(new GameplaySceneOutputArgs(new MainMenuSceneInputArgs()));
        }
    }
}