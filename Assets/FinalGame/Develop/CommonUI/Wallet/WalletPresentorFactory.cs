using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;

namespace FinalGame.Develop.CommonUI.Wallet
{
    public class WalletPresentorFactory
    {
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;
        
        public WalletPresentorFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        public CurrencyPresentor CreateCurrencyPresentor(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresentor(_walletService.GetCurrency(currencyType), currencyType, view,
                _configsProviderService.CurrencyIconsConfig);
    }
}
