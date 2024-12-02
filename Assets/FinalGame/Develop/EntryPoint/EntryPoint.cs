using System.ComponentModel;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.DataManagement;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

namespace FinalGame.Develop.EntryPoint
{
    //регистрация глобальных сервисов для старта игры
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private void Awake()
        {
            SetupAppSettings();

            var projectContainer = new DIContainer();

            ProcessRegistrations(projectContainer);
            
            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));

            //регистрация глобальных сервисов
            //аналог Global Context в популярных DI фреймворках
        }

        private void ProcessRegistrations(DIContainer projectContainer)
        {
            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);
            RegisterLoadingScreen(projectContainer);
            RegisterSceneLoader(projectContainer);
            RegisterSceneSwitcher(projectContainer);
            RegisterSaveLoadService(projectContainer);
            RegisterPlayerDataProvider(projectContainer);
            RegisterWalletService(projectContainer);
            
            projectContainer.Initialize();
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }

        private void RegisterResourcesAssetLoader(DIContainer container) =>
            container.RegisterAsSingle(c => new ResourcesAssetLoader());

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

        private void RegisterLoadingScreen(DIContainer container)
        {
            container.RegisterAsSingle<ILoadingScreen>(c =>
            {
                var resourcesAssetLoader = container.Resolve<ResourcesAssetLoader>();
                var loadingScreenPrefab =
                    resourcesAssetLoader.LoadResource<LoadingScreen>(InfrastructureAssetPaths.LoadingScreenPath);
                return Instantiate(loadingScreenPrefab);
            });
        }

        private void RegisterSceneLoader(DIContainer container)
            => container.RegisterAsSingle<ISceneLoader>(c => new SceneLoader());

        private void RegisterSceneSwitcher(DIContainer container)
            => container.RegisterAsSingle(c
                => new SceneSwitcher(
                    c,
                    c.Resolve<ICoroutinePerformer>(),
                    c.Resolve<ILoadingScreen>(),
                    c.Resolve<ISceneLoader>()));

        private void RegisterSaveLoadService(DIContainer container)
            => container.RegisterAsSingle<ISaveLoadService>(c
                => new SaveLoadService(new LocalDataRepository(), new JsonSerializer()));

        private void RegisterPlayerDataProvider(DIContainer container)
            => container.RegisterAsSingle(c 
                => new PlayerDataProvider(c.Resolve<ISaveLoadService>()));

        private void RegisterWalletService(DIContainer container)
            => container.RegisterAsSingle(c 
                => new WalletService(c.Resolve<PlayerDataProvider>())).NonLazy();
    }
}
