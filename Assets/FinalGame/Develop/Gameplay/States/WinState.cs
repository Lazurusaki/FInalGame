using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Utils.Reactive;
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
        private readonly WalletService _walletService;
        private readonly MainHeroHolderService _mainHeroHolderService;

        public WinState(IPauseService pauseService, 
            IInputService inputService, 
            CompletedLevelsService completedLevelsService, 
            PlayerDataProvider playerDataProvider, 
            GameplaySceneInputArgs gameplaySceneInputArgs, 
            SceneSwitcher sceneSwitcher,
            WalletService walletService,
            MainHeroHolderService mainHeroHolderService) : base(pauseService, inputService)
        {
            _completedLevelsService = completedLevelsService;
            _playerDataProvider = playerDataProvider;
            _gameplaySceneInputArgs = gameplaySceneInputArgs;
            _sceneSwitcher = sceneSwitcher;
            _walletService = walletService;
            _mainHeroHolderService = mainHeroHolderService;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Win!");

            _walletService.Add(CurrencyTypes.Gold,_mainHeroHolderService.MainHero.GetCoins().Value);
            
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