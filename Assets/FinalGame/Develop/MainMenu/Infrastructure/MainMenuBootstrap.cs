using System.Collections;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonUI.Results;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.MainMenu.Features.LevelsMenu.LevelsMenuPopup;
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

            mainMenuUIRoot.StartGameButton.Initialize(() =>
            {
                SceneSwitcher sceneSwitcher = _container.Resolve<SceneSwitcher>();
                int levelsCount = _container.Resolve<ConfigsProviderService>().LevelsListConfig.Levels.Count;
                int randomIndex = Random.Range(1, levelsCount + 1);
                
                sceneSwitcher.ProcessSwitchSceneFor(
                    new MainMenuSceneOutputArgs(new GameplaySceneInputArgs(randomIndex)));
            });
            
            /*
            mainMenuUIRoot.StartGameButton.Initialize(() =>
            {
                var levelsMenuPopupPresenter = _container.Resolve<LevelsMenuPopupFactory>().CreateLevelsMenuPopupPresenter();
                levelsMenuPopupPresenter.Enable();
            });
            */
        }
        
        private void ProcessRegistrations()
        {
            RegisterLevelsMenuPopupFactory();
            RegisterWalletPresenterFactory();
            RegisterWalletPresenter();
            RegisterMainMenuUIRoot();
            RegisterGameResultsPresenterFactory();
            RegisterGameResultsPresenter();
            
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
        
        private void RegisterGameResultsPresenterFactory()
            => _container.RegisterAsSingle(c=> new GameResultsStatsPresenterFactory(c));

        private void RegisterGameResultsPresenter()
            => _container.RegisterAsSingle(c =>
                c.Resolve<GameResultsStatsPresenterFactory>().CreateGameResultsStatsPresenter(
                    c.Resolve<MainMenuUIRoot>().GameResultsStatsView)).NonLazy();

        private void RegisterLevelsMenuPopupFactory()
            => _container.RegisterAsSingle(c => new LevelsMenuPopupFactory(c));
    }
}
