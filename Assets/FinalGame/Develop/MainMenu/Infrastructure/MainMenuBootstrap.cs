using System;
using System.Collections;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
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
            
            mainMenuUIRoot.OpenLevelsMenuButton.Initialize(() =>
            {
                var levelsMenuPopupPresenter = _container.Resolve<LevelsMenuPopupFactory>().CreateLevelsMenuPopupPresenter();
                levelsMenuPopupPresenter.Enable();
            });
        }
        
        private void ProcessRegistrations()
        {
            RegisterLevelsMenuPopupFactory();
            RegisterWalletPresenterFactory();
            RegisterMainMenuUIRoot();
            RegisterWalletPresenter();
            
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
    }
}
