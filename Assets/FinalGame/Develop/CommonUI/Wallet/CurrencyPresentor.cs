using System;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Utils.Reactive;
using Unity.VisualScripting;

namespace FinalGame.Develop.CommonUI.Wallet
{
    public class CurrencyPresentor : IInitializable, IDisposable
    {
        //model link
        private readonly IReadOnlyVariable<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        //view link
        private IconWithText _currencyView;

        public CurrencyPresentor(
            IReadOnlyVariable<int> currency, 
            CurrencyTypes currencyType, 
            IconWithText currencyView, 
            CurrencyIconsConfig currencyIconsConfig)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyView = currencyView;
            _currencyIconsConfig = currencyIconsConfig;
        }

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            _currency.Changed += OncurrencyChanged;
            _currencyView.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));
        }

        private void OncurrencyChanged(int oldValue, int nweValue) => UpdateValue(nweValue);
        
        public void Dispose()
        {
            _currency.Changed -= OncurrencyChanged;
        }

        private void UpdateValue(int value) => _currencyView.SetText(value.ToString());
    }
}
