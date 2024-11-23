using System;
using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Infrastructure;
using FinalGame.Develop.MainMenu.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FinalGame.Develop.CommonServices.SceneManagement
{
    public class SceneSwitcher
    {
        private const string ErrorSceneTransitionMessage = "Transition is not possible";

        private readonly DIContainer _projectContainer;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly ILoadingScreen _loadingScreen;
        private readonly ISceneLoader _sceneLoader;

        private DIContainer _nextSceneContainer;

        public SceneSwitcher(DIContainer projectContainer, ICoroutinePerformer coroutinePerformer, ILoadingScreen loadingScreen, ISceneLoader sceneLoader)
        {
            _projectContainer = projectContainer;
            _coroutinePerformer = coroutinePerformer;
            _loadingScreen = loadingScreen;
            _sceneLoader = sceneLoader;
            
        }

        public void ProcessSwitchSceneFor(IOutputSceneArgs outputSceneArgs)
        {
            switch (outputSceneArgs)
            {
                case BootstrapSceneOutputArgs bootstrapSceneOutputArgs:
                {
                    _coroutinePerformer.StartPerform(ProcessSwitchFromBootstrapScene(bootstrapSceneOutputArgs));
                    break;
                }
                case MainMenuSceneOutputArgs mainMenuSceneOutputArgs:
                {
                    _coroutinePerformer.StartPerform(ProcessSwitchFromMainMenuScene(mainMenuSceneOutputArgs));
                    break;
                }
                case GameplaySceneOutputArgs gameplaySceneOutputArgs:
                {
                    _coroutinePerformer.StartPerform(ProcessSwitchFromGameplayScene(gameplaySceneOutputArgs));
                    break;
                }
                
                default:
                    throw new ArgumentException(nameof(outputSceneArgs));
            }
        }

        private IEnumerator ProcessSwitchFromBootstrapScene(BootstrapSceneOutputArgs bootstrapSceneOutputArgs)
        {
            switch (bootstrapSceneOutputArgs.NextSceneInputArgs)
            {
                case MainMenuSceneInputArgs mainMenuInputSceneArgs:
                {
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputSceneArgs);
                    break;
                }
                
                default: 
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }
        
        private IEnumerator ProcessSwitchFromMainMenuScene(MainMenuSceneOutputArgs mainMenuSceneOutputArgs)
        {
            switch (mainMenuSceneOutputArgs.NextSceneInputArgs)
            {
                case GameplaySceneInputArgs gameplaySceneInputArgs:
                {
                    yield return ProcessSwitchToGameplayScene(gameplaySceneInputArgs);
                    break;
                }
                
                default: 
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }
        
        private IEnumerator ProcessSwitchFromGameplayScene(GameplaySceneOutputArgs gameplaySceneOutputArgs)
        {
            switch (gameplaySceneOutputArgs.NextSceneInputArgs)
            {
                case MainMenuSceneInputArgs mainMenuInputSceneArgs:
                {
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputSceneArgs);
                    break;
                }
                
                case GameplaySceneInputArgs gameplaySceneInputArgs:
                {
                    yield return ProcessSwitchToGameplayScene(gameplaySceneInputArgs);
                    break;
                }
                
                default: 
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchToMainMenuScene(MainMenuSceneInputArgs mainMenuSceneInputArgs)
        {
            _loadingScreen.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.MainMenu);

            var mainMenuBootstrap = Object.FindAnyObjectByType<MainMenuBootstrap>();

            if (mainMenuBootstrap == null)
                throw new NullReferenceException(nameof(mainMenuBootstrap));

            _nextSceneContainer = new DIContainer(_projectContainer);
            
            yield return mainMenuBootstrap.Run(_nextSceneContainer, mainMenuSceneInputArgs);
            
            _loadingScreen.Hide();
            
            //mainMenuBootstrap.Runs(_currentSceneContainer, mainMenuSceneInputArgs);
        }
        
        private IEnumerator ProcessSwitchToGameplayScene(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            _loadingScreen.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.Gameplay);

            var gameplayBootstrap = Object.FindAnyObjectByType<GameplayBootstrap>();

            if (gameplayBootstrap == null)
                throw new NullReferenceException(nameof(gameplayBootstrap));
            
            _nextSceneContainer = new DIContainer(_projectContainer);

            yield return gameplayBootstrap.Run(_nextSceneContainer, gameplaySceneInputArgs);
            
            _loadingScreen.Hide();
        }
    }
}
