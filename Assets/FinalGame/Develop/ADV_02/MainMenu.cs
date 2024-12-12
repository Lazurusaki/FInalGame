using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay;
using FinalGame.Develop.MainMenu.UI;
using Unity.VisualScripting;

namespace FinalGame.Develop.ADV_02
{
    public class MainMenu : IInitializeable
    {
        private readonly DIContainer _container;
        
        public MainMenu(DIContainer container)
        {
            _container = container;
        }
        
        public void Initialize()
        {
            _container.Resolve<MainMenuUIRoot>().StartGameButton.onClick.AddListener(StartGame);
            _container.Resolve<MainMenuUIRoot>().ResetStatsButton.onClick.AddListener(ResetStats);
        }
        

        public void Dispose()
        {
            _container.Resolve<MainMenuUIRoot>().StartGameButton.onClick.RemoveListener(StartGame);
            _container.Resolve<MainMenuUIRoot>().ResetStatsButton.onClick.RemoveListener(ResetStats);
        }
        
        private void StartGame()
        {
            _container.Resolve<SceneSwitcher>().ProcessSwitchSceneFor(new MainMenuSceneOutputArgs(
                new GameplaySceneInputArgs(_container.Resolve<GameModeNameHandler>().GameMode)));
        }

        private void ResetStats()
        {
            var wallet = _container.Resolve<WalletService>();
            var resetStatsCurrency = _container.Resolve<ConfigsProviderService>().GameRewardsConfig.GetResetCurrency();
            var playerStats = _container.Resolve<GameResultsStatsService>();
            
           if (wallet.HasEnough(resetStatsCurrency.CurrencyType, resetStatsCurrency.Value))
           {
               wallet.Spend(resetStatsCurrency.CurrencyType, resetStatsCurrency.Value);
               playerStats.Reset();
           }
        }
    }
}