using System.Collections;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap: MonoBehaviour
    {
        private DIContainer _container;
        private ICoroutinePerformer _coroutinePerformer;
        
        public IEnumerator Run(DIContainer container, MainMenuSceneInputArgs mainMenuSceneInputArgs)
        {
            _container = container;
            _coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
            
            ProcessRegistrations();
            
            yield return new WaitForSeconds(1);

            Launch();
        }

        private void Launch()
        {
            ShowMenuMessage();
            
            _coroutinePerformer.StartPerform(SelectGameMode());
        }
        
        private void ProcessRegistrations()
        {
           RegisterCoroutinePerformer(_container); 
        }
        
        private void ShowMenuMessage()
        {
            Debug.Log("Main Menu");
            Debug.Log($"Choose Game mode: 1 - {GameModes.Numbers}, 2 - {GameModes.Letters}");
        }

        private IEnumerator SelectGameMode()
        {
            bool isModeSelected = false;
            GameModes gameMode = default;

            while (isModeSelected == false)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
                {
                    isModeSelected = true;
                    gameMode = Input.GetKeyDown(KeyCode.Alpha1) ? GameModes.Numbers : GameModes.Letters;
                }

                yield return null;
            }

            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new MainMenuSceneOutputArgs(
                new GameplaySceneInputArgs(gameMode)));
        }
        
        private void RegisterCoroutinePerformer(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                var resourcesAssetLoader = container.Resolve<ResourcesAssetLoader>();
                var coroutinePerformerPrefab =
                    resourcesAssetLoader.LoadResource<CoroutinePerformer>(InfrastructureAssetPaths
                        .CoroutinePerformerPath);
                return Instantiate(coroutinePerformerPrefab);
            });
        }
    }
}
