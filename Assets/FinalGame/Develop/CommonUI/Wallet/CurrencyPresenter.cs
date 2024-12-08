using System;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Utils.Reactive;
using Unity.VisualScripting;

namespace FinalGame.Develop.CommonUI.Wallet
{
    public class CurrencyPresenter : IInitializable, IDisposable
    {
        //model link
        private readonly IReadOnlyVariable<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        //view link
        private readonly IconWithText _view;
        
        public CurrencyPresenter(
            IReadOnlyVariable<int> currency, 
            CurrencyTypes currencyType, 
            IconWithText view, 
            CurrencyIconsConfig currencyIconsConfig)
        {
            _currency = currency;
            _currencyType = currencyType;
            _view = view;
            _currencyIconsConfig = currencyIconsConfig;
        }
        
        public IconWithText View => _view;

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _currency.Changed += OnCurrencyChanged;
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));
        }

        private void OnCurrencyChanged(int oldValue, int nweValue) => UpdateValue(nweValue);
        
        public void Dispose()
        {
            _currency.Changed -= OnCurrencyChanged;
        }

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
