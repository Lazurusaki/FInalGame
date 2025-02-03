using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.Results;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Gameplay.UI;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class LooseState : EndGameState, IUpdatableState
    {
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly LevelConfig _levelConfig;
        private readonly GameplayUIPresentersFactory _gameplayUIPresentersFactory;
        private readonly GameResultsStatsService _gameResultsStatsService;
        
        public LooseState(IInputService inputService, 
            PlayerDataProvider playerDataProvider, 
            SceneSwitcher sceneSwitcher,  
            LevelConfig levelConfig,
            GameplayUIPresentersFactory gameplayUIPresentersFactory,
            GameResultsStatsService gameResultsStatsService) : base(inputService)
        {
            _playerDataProvider = playerDataProvider;
            _sceneSwitcher = sceneSwitcher;
            _levelConfig = levelConfig;
            _gameplayUIPresentersFactory = gameplayUIPresentersFactory;
            _gameResultsStatsService = gameResultsStatsService;
        }

        public override void Enter()
        {
            base.Enter();
            
            _gameResultsStatsService.Add(GameResults.Loose);
            var presenter = _gameplayUIPresentersFactory.CreateDefeatResultPresenter();
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