using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.DataManagement;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.LevelsService;
using FinalGame.Develop.CommonServices.LoadingScreen;
using FinalGame.Develop.CommonServices.Results;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Timer;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using UnityEngine;

namespace FinalGame.Develop.EntryPoint
{
    //регистрация глобальных сервисов для старта игры
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private DIContainer _projectContainer = new DIContainer();

        private void Awake()
        {
            SetupAppSettings();

            ProcessRegistrations();

            _projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(_projectContainer));

            //регистрация глобальных сервисов
            //аналог Global Context в популярных DI фреймворках
        }

        private void ProcessRegistrations()
        {
            RegisterResourcesAssetLoader();
            RegisterCoroutinePerformer();
            RegisterLoadingScreen();
            RegisterSceneLoader();
            RegisterSceneSwitcher();
            RegisterSaveLoadService();
            RegisterPlayerDataProvider();
            RegisterWalletService();
            RegisterGameResultsService();
            RegisterConfigsProvider();
            RegisterCompletedLevelsService();
            RegisterTimerFactory();

            _projectContainer.Initialize();
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }

        private void RegisterResourcesAssetLoader() =>
            _projectContainer.RegisterAsSingle(c => new ResourcesAssetLoader());

        private void RegisterCoroutinePerformer()
        {
            _projectContainer.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                var resourcesAssetLoader = _projectContainer.Resolve<ResourcesAssetLoader>();
                var coroutinePerformerPrefab =
                    resourcesAssetLoader.LoadResource<CoroutinePerformer>(InfrastructureAssetPaths
                        .CoroutinePerformerPath);
                return Instantiate(coroutinePerformerPrefab);
            });
        }

        private void RegisterLoadingScreen()
        {
            _projectContainer.RegisterAsSingle<ILoadingScreen>(c =>
            {
                var resourcesAssetLoader = _projectContainer.Resolve<ResourcesAssetLoader>();
                var loadingScreenPrefab =
                    resourcesAssetLoader.LoadResource<LoadingScreen>(InfrastructureAssetPaths.LoadingScreenPath);
                return Instantiate(loadingScreenPrefab);
            });
        }

        private void RegisterSceneLoader()
            => _projectContainer.RegisterAsSingle<ISceneLoader>(c => new SceneLoader());

        private void RegisterSceneSwitcher()
            => _projectContainer.RegisterAsSingle(c
                => new SceneSwitcher(
                    c,
                    c.Resolve<ICoroutinePerformer>(),
                    c.Resolve<ILoadingScreen>(),
                    c.Resolve<ISceneLoader>()));

        private void RegisterSaveLoadService()
            => _projectContainer.RegisterAsSingle<ISaveLoadService>(c
                => new SaveLoadService(new LocalDataRepository(), new JsonSerializer()));

        private void RegisterPlayerDataProvider()
            => _projectContainer.RegisterAsSingle(c
                => new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>()));

        private void RegisterWalletService()
            => _projectContainer.RegisterAsSingle(c
                => new WalletService(c.Resolve<PlayerDataProvider>())).NonLazy();
        
        private void RegisterGameResultsService()
            => _projectContainer.RegisterAsSingle(c
                => new GameResultsStatsService(c.Resolve<PlayerDataProvider>())).NonLazy();

        private void RegisterConfigsProvider()
            => _projectContainer.RegisterAsSingle(c
                => new ConfigsProviderService(c.Resolve<ResourcesAssetLoader>()));

        private void RegisterCompletedLevelsService()
            => _projectContainer.RegisterAsSingle(c
                => new CompletedLevelsService(c.Resolve<PlayerDataProvider>())).NonLazy();

        private void RegisterTimerFactory()
            => _projectContainer.RegisterAsSingle(c => new TimerServiceFactory(c));
    }
}