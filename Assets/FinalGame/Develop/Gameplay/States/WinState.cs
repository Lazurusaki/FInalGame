using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly CompletedLevelsService _completedLevelsService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly GameplaySceneInputArgs _gameplaySceneInputArgs;
        private readonly SceneSwitcher _sceneSwitcher;

        public WinState(IPauseService pauseService, IInputService inputService, CompletedLevelsService completedLevelsService, PlayerDataProvider playerDataProvider, GameplaySceneInputArgs gameplaySceneInputArgs, SceneSwitcher sceneSwitcher) : base(pauseService, inputService)
        {
            _completedLevelsService = completedLevelsService;
            _playerDataProvider = playerDataProvider;
            _gameplaySceneInputArgs = gameplaySceneInputArgs;
            _sceneSwitcher = sceneSwitcher;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Win!");

            _completedLevelsService.TryAddLevelCompleted(_gameplaySceneInputArgs.LevelNumber);
            _playerDataProvider.Save();
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _sceneSwitcher.ProcessSwitchSceneFor(new GameplaySceneOutputArgs(new MainMenuSceneInputArgs()));
        }
    }
}