using System;
using System.Collections;
using FinalGame.Develop.ADV_02;
using FinalGame.Develop.CommonServices.DataManagement;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using UnityEngine;
using MainMenuSceneInputArgs = FinalGame.Develop.CommonServices.SceneManagement.MainMenuSceneInputArgs;

namespace FinalGame.Develop.Gameplay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;
        private IGameMode _gameMode;
        private GameplaySceneInputArgs _sceneInputArgs;
        private Game _game;
        private GameResultsStatsService _gameResultsStatsService;
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;
            _sceneInputArgs = sceneInputArgs;

            RegisterGameModeFactory();
            RegisterConditionFactory();
            //RegisterGameDataProvider();

            _gameMode = _container.Resolve<GameModeFactory>().CreateGameMode(sceneInputArgs.GameModeName);

            _gameResultsStatsService = _container.Resolve<GameResultsStatsService>();
            _walletService = _container.Resolve<WalletService>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();

            RegisterGameModeHandler();

            _container.Initialize();

            var winCondition = _container.Resolve<ConditionFactory>().CreateCondition(EndGameConditions.ValuesGuessed);
            var looseCondition = _container.Resolve<ConditionFactory>().CreateCondition(EndGameConditions.Mistake);

            _game = new Game(container, winCondition, looseCondition);
            _game.GameOver += OnGameOver;

            yield return new WaitForSeconds(0.1f);

            _game.Start();
        }

        private void OnGameOver(GameResults gameResult)
        {
            _game.GameOver -= OnGameOver;
            
            UpdatePlayerData(gameResult);

            SaveGame();
        }

        private void UpdatePlayerData(GameResults gameResult)
        {
            _gameResultsStatsService.Add(gameResult);

            var reward = _configsProviderService.GameRewardsConfig.GetReward(gameResult);
            
            switch (gameResult)
            {
                case GameResults.Win:
                    HandleWin(reward);
                    break;
                case GameResults.Loose:
                    HandleLoose(reward);
                    break;
                default:
                    throw new ArgumentException($"Unexpected game result - {gameResult}");
            }
        }
        
        private void HandleWin(CurrencyReward reward)
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new GameplaySceneOutputArgs(
                new MainMenuSceneInputArgs()));

            _walletService.Add(reward.CurrencyType, reward.Value);
        }

        private void HandleLoose(CurrencyReward reward)
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new GameplaySceneOutputArgs(
                new GameplaySceneInputArgs(_sceneInputArgs.GameModeName)));
            
            _walletService.Spend(reward.CurrencyType, reward.Value);
        }

        private void SaveGame()
        {
            _container.Resolve<PlayerDataProvider>().Save();
        }
        
        private void RegisterGameModeHandler()
        {
            _container.RegisterAsSingle(c => new GameModeHandler(_gameMode));
        }

        private void RegisterGameModeFactory()
        {
            _container.RegisterAsSingle(c => new GameModeFactory());
        }
        
        private void RegisterConditionFactory()
        {
            _container.RegisterAsSingle(c => new ConditionFactory(_container));
        }
        
        // private void RegisterGameDataProvider()
        // {
        //     _container.RegisterAsSingle(c => new GameDataProvider(
        //         c.Resolve<SaveLoadService>(), c.Resolve<ConfigsProviderService>()));
        // }
    }
}