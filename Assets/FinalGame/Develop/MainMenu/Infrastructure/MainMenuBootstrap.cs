using System.Collections;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.DI;
using FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup;
using FinalGame.Develop.MainMenu.Features.StatsUpgradeMenu;
using FinalGame.Develop.MainMenu.UI;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap: MonoBehaviour
    {

        private DIContainer _container;
        
        public IEnumerator Run(DIContainer container, MainMenuSceneInputArgs mainMenuSceneInputArgs)
        {
            _container = container;
            
            ProcessRegistrations();
            
            InitializeUI();
            
            yield return new WaitForSeconds(0.1f);
        }

        private void InitializeUI()
        {
            var mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            
            mainMenuUIRoot.OpenLevelsMenuButton.Initialize(() =>
            {
                var levelsMenuPopupPresenter = _container.Resolve<LevelsMenuPopupFactory>().CreateLevelsMenuPopupPresenter();
                levelsMenuPopupPresenter.Enable();
            });
            
            mainMenuUIRoot.OpenUpgradeMenuButton.Initialize(() =>
            {
                var upgradeStatsPresenter = _container.Resolve<StatsUpgradePopupFactory>().CreatePopup();
                upgradeStatsPresenter.Enable();
            });
        }
        
        private void ProcessRegistrations()
        {
            RegisterLevelsMenuPopupFactory();
            RegisterWalletPresenterFactory();
            RegisterMainMenuUIRoot();
            RegisterWalletPresenter();
            RegisterPlayerStatsUpgradePopupFactory();
            
            _container.Initialize();    
        }

        private void RegisterMainMenuUIRoot()
        {
            _container.RegisterAsSingle( c =>
            {
                var mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUiRoot");
                return Instantiate(mainMenuUIRootPrefab);
            }).NonLazy();
        }

        private void RegisterWalletPresenterFactory()
            => _container.RegisterAsSingle(c=> new WalletPresenterFactory(c));

        private void RegisterWalletPresenter()
            => _container.RegisterAsSingle(c =>
                c.Resolve<WalletPresenterFactory>().CreateWalletPresenter(
                    c.Resolve<MainMenuUIRoot>().WalletView)).NonLazy();

        private void RegisterLevelsMenuPopupFactory()
            => _container.RegisterAsSingle(c => new LevelsMenuPopupFactory(c));

        private void RegisterPlayerStatsUpgradePopupFactory()
            => _container.RegisterAsSingle(c => new StatsUpgradePopupFactory(c));
    }
}
