using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Features.Stats;
using FinalGame.Develop.MainMenu.UI;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Features.StatsUpgradeMenu
{
    public class StatsUpgradePopupFactory
    {
        private readonly DIContainer _container;
        private readonly ResourcesAssetLoader _resourcesAssetLoader;
        private readonly MainMenuUIRoot _mainMenuUIRoot;
        private readonly ConfigsProviderService _configsProviderService;

        public StatsUpgradePopupFactory(DIContainer container)
        {
            _container = container;
            _mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            _resourcesAssetLoader = _container.Resolve<ResourcesAssetLoader>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();
        }

        public UpgradableStatPresenter CreateUpgradableStatPresenter(UpgradableStatView view, StatTypes statType)
        {
            return new UpgradableStatPresenter(
                view,
                statType,
                _configsProviderService.StatsViewConfig,
                _container.Resolve<StatsUpgradeService>(),
                _container.Resolve<WalletService>(),
                _configsProviderService.CurrencyIconsConfig);
        }

        public StatsUpgradePopupPresenter CreatePopup()
        {
            StatsUpgradePopupView statsUpgradePopupViewPrefab = _resourcesAssetLoader.LoadResource<StatsUpgradePopupView>("MainMenu/UI/PlayerUpgradePopupMenu/StatsUpgradePopupView");
            StatsUpgradePopupView statsUpgradePopupView = Object.Instantiate(statsUpgradePopupViewPrefab, _mainMenuUIRoot.PopupsLayer);

            return new StatsUpgradePopupPresenter(
                statsUpgradePopupView,
                _container.Resolve<StatsUpgradePopupFactory>(),
                _container.Resolve<WalletPresenterFactory>(),
                _container.Resolve<StatsUpgradeService>());
        }

        public CharacterPreviewPresenter CreateCharacterPreviewPresenter()
        {
            return new CharacterPreviewPresenter(_container.Resolve<ISceneLoader>(), _container.Resolve<ICoroutinePerformer>());
        }
    }
}