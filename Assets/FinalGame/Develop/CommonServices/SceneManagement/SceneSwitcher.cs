using System;
using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Infrastructure;
using FinalGame.Develop.MainMenu.Infrastructure;
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

        private DIContainer _currentSceneContainer;

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
                case BootstrapSceneOutputArgs outputBootstrapSceneArgs:
                {
                    _coroutinePerformer.StartPerform(ProcessSwitchFromBootstrapScene(outputBootstrapSceneArgs));
                    break;
                }
                case MainMenuSceneOutputArgs outputMainMenuSceneArgs:
                {
                    _coroutinePerformer.StartPerform(ProcessSwitchFromMainMenuScene(outputMainMenuSceneArgs));
                    break;
                }
                case GameplaySceneOutputArgs outputGameplaySceneArgs:
                {
                    _coroutinePerformer.StartPerform(ProcessSwitchFromGameplayScene(outputGameplaySceneArgs));
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

            _currentSceneContainer = new DIContainer(_projectContainer);

            yield return mainMenuBootstrap.Run(_currentSceneContainer, mainMenuSceneInputArgs);
            
            _loadingScreen.Hide();
        }
        
        private IEnumerator ProcessSwitchToGameplayScene(GameplaySceneInputArgs gameplaySceneInputArgs)
        {
            _loadingScreen.Show();

            yield return _sceneLoader.LoadAsync(SceneID.Empty);
            yield return _sceneLoader.LoadAsync(SceneID.Gameplay);

            var gameplayBootstrap = Object.FindAnyObjectByType<GameplayBootstrap>();

            if (gameplayBootstrap == null)
                throw new NullReferenceException(nameof(gameplayBootstrap));
            
            _currentSceneContainer = new DIContainer(_projectContainer);

            yield return gameplayBootstrap.Run(_currentSceneContainer, gameplaySceneInputArgs);
            
            _loadingScreen.Hide();
        }
    }
}
