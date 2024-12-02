using System;
using System.Collections;
using FinalGame.Develop.CommonServices.SceneManagement;
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
        
        public IEnumerator Run(DIContainer container, GameplaySceneInputArgs sceneInputArgs)
        {
            _container = container;
            _sceneInputArgs = sceneInputArgs;
            
            RegisterGameModeFactory();
            RegisterConditionFactory();
            
            _gameMode = _container.Resolve<GameModeFactory>().CreateGameMode(sceneInputArgs.GameModeName);
            
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
    }
}