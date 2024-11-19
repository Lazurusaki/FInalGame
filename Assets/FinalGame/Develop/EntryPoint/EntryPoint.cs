using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using UnityEngine;

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

            RegisterResourcesAssetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);
            RegisterLoadingScreen(projectContainer);
            RegisterSceneLoader(projectContainer);
            RegisterSceneSwitcher(projectContainer);

            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));

            //регистрация глобальных сервисов
            //аналог Global Context в популярных DI фреймворках
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

        private void RegisterSceneLoader(DIContainer container) =>
            container.RegisterAsSingle(c => new SceneLoader());

        private void RegisterSceneSwitcher(DIContainer container) =>
            container.RegisterAsSingle(c =>
                new SceneSwitcher(
                    c,
                    c.Resolve<CoroutinePerformer>(),
                    c.Resolve<LoadingScreen>(), 
                    c.Resolve<SceneLoader>()));
    }
}