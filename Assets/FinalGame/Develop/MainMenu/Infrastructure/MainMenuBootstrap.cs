using System;
using System.Collections;
using System.Collections.Generic;
using FinalGame.Develop.ADV_02;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using FinalGame.Develop.MainMenu.UI;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap: MonoBehaviour
    {
        private readonly string _header = "Main Menu. Please Select Game Mode:";

        private DIContainer _container;
        
        private readonly List<MenuItem> _menuItems = new()
        {
            new MenuItem(GameModes.Numbers.ToString()), 
            new MenuItem(GameModes.Letters.ToString())
        };
        
        private readonly List<IMenuCommand> _commands = new()
        {
            new StartGame(GameModes.Numbers),
            new StartGame(GameModes.Letters)
        };
        
        public IEnumerator Run(DIContainer container, MainMenuSceneInputArgs mainMenuSceneInputArgs)
        {
            _container = container;
            
            ProcessRegistrations();
            
            InitializeCommands();
            
            IMenu mainMenu = new ConsoleMenu(_header, _menuItems);
            BindCommands(new MenuItemCommandsMap(mainMenu));
            
            yield return new WaitForSeconds(1);
            
            _container.Resolve<ICoroutinePerformer>().StartPerform(mainMenu.Start());
        }

        private void BindCommands(MenuItemCommandsMap map)
        {
            if (_menuItems.Count != _commands.Count)
                throw new InvalidOperationException("Menu items count must be equal commands count");
            
            for (var i = 0; i < _menuItems.Count; i++)
                map.Bind(_menuItems[i], _commands[i]);
        }

        private void ProcessRegistrations()
        {
            RegisterWalletPresenterFactory();
            RegisterMainMenuUIRoot();
            RegisterWalletPresenter();
            
            //ADV_02
            RegisterGameResultsStatsPresenterFactory();
            RegisterGameResultsStatsPresenter();
            
            
            _container.Initialize();    
        }

        private void InitializeCommands()
        {
            foreach (var command in _commands)
                command.Initialize(_container);
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
        
        private void RegisterGameResultsStatsPresenterFactory()
            => _container.RegisterAsSingle(c=> new GameResultsStatsPresenterFactory(c));
        
        private void RegisterGameResultsStatsPresenter()
            => _container.RegisterAsSingle(c =>
                c.Resolve<GameResultsStatsPresenterFactory>().CreateGameResultsStatsPresenter(
                    c.Resolve<MainMenuUIRoot>().GameResultsStatsView)).NonLazy();
    }
}
