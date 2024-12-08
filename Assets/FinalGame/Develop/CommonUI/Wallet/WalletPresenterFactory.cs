using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.DI;

namespace FinalGame.Develop.CommonUI.Wallet
{
    public class WalletPresenterFactory
    {
        private readonly WalletService _walletService;
        private readonly ConfigsProviderService _configsProviderService;
        
        public WalletPresenterFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        public WalletPresenter CreateWalletPresenter(IconsWithTextList view)
            => new WalletPresenter(_walletService, view, this);

        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresenter(_walletService.GetCurrency(currencyType), currencyType, view,
                _configsProviderService.CurrencyIconsConfig);
    }
}
