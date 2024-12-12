using System.Collections;
using System.Collections.Generic;
using FinalGame.Develop.ADV_02;
using FinalGame.Develop.ADV_02.UI;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
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
            
            yield return new WaitForSeconds(0.1f);
        }
        
        private void ProcessRegistrations()
        {
            RegisterWalletPresenterFactory();
            RegisterMainMenuUIRoot();
            RegisterWalletPresenter();
            
            //ADV_02
            RegisterMainMenuService();
            RegisterGameResultsStatsPresenterFactory();
            RegisterGameResultsStatsPresenter();
            RegisterGameModeNameHandler();
            RegisterGameModeTogglePresenter();
            
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

        private void RegisterMainMenuService()
            => _container.RegisterAsSingle(c => new ADV_02.MainMenu(_container)).NonLazy();

        private void RegisterWalletPresenterFactory()
            => _container.RegisterAsSingle(c=> new WalletPresenterFactory(c));

        private void RegisterWalletPresenter()
            => _container.RegisterAsSingle(c =>
                c.Resolve<WalletPresenterFactory>().CreateWalletPresenter(
                    c.Resolve<MainMenuUIRoot>().WalletView)).NonLazy();
        
        private void RegisterGameResultsStatsPresenterFactory()
            => _container.RegisterAsSingle(c=> new GameResultsStatsPresenterFactory(c));
        
        private void RegisterGameResultsStatsPresenter()
            => _container.RegisterAsSingle(c =>
                c.Resolve<GameResultsStatsPresenterFactory>().CreateGameResultsStatsPresenter(
                    c.Resolve<MainMenuUIRoot>().GameResultsStatsView)).NonLazy();

        private void RegisterGameModeNameHandler()
            => _container.RegisterAsSingle(c => new GameModeNameHandler()).NonLazy();

        private void RegisterGameModeTogglePresenter()
            => _container.RegisterAsSingle(c 
                => new GameModeTogglePresenter(
                        c.Resolve<GameModeNameHandler>(),
                        c.Resolve<MainMenuUIRoot>().GameModeToggle)).NonLazy();
    }
}
