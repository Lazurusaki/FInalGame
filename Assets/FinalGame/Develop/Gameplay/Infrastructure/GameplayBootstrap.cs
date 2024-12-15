using System;
using System.Collections;
using FinalGame.Develop.ADV_02;
using FinalGame.Develop.ADV_02.UI;
using FinalGame.Develop.CommonServices.AssetsManagement;
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
        private ISequenceGameMode _gameMode;
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
            RegisterGameplayUIRoot();
            RegisterGuessSequencePresenter();
            RegisterGuessValidatePresenter();

            _gameMode = _container.Resolve<GameModeFactory>().CreateGameMode(container, sceneInputArgs.GameModeName);

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

            switch (gameResult)
            {
                case GameResults.Win:
                    _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new GameplaySceneOutputArgs(
                        new MainMenuSceneInputArgs()));
                    break;
                case GameResults.Loose:
                    _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new GameplaySceneOutputArgs(
                        new GameplaySceneInputArgs(_sceneInputArgs.GameModeName)));
                    break;
                default:
                    throw new ArgumentException($"Unexpected game result - {gameResult}");
            }
        }

        private void RegisterGameplayUIRoot()
        {
            _container.RegisterAsSingle(c =>
            {
                var gameplayUiRootPrefab = c.Resolve<ResourcesAssetLoader>()
                    .LoadResource<GameplayUiRoot>("CommonUI/ADV_02/GameplayUiRoot");
                return Instantiate(gameplayUiRootPrefab);
            }).NonLazy();
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

        private void RegisterGuessSequencePresenter()
        {
            _container.RegisterAsSingle(c => new GuessSequencePresenter(
                c.Resolve<GameModeHandler>().GameMode,
                c.Resolve<GameplayUiRoot>().GeneratedSequence)).NonLazy();
        }

        private void RegisterGuessValidatePresenter()
        {
            _container.RegisterAsSingle(c => new GuessValidatePresenter(
                c.Resolve<GameModeHandler>().GameMode,
                c.Resolve<GameplayUiRoot>().GuessValidateView)).NonLazy();
        }
    }
}