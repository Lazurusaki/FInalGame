using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.MainMenu.UI;

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
            
           if (wallet.HasEnough(resetStatsCurrency.CurrencyType, resetStatsCurrency.Value))
           {
               wallet.Spend(resetStatsCurrency.CurrencyType, resetStatsCurrency.Value);
               _container.Resolve<GameResultsStatsService>().Reset();
               _container.Resolve<PlayerDataProvider>().Save();
           }
        }
    }
}