using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.DI;
using UnityEngine;


namespace FinalGame.Develop.CommonUI.Wallet
{
    public class WalletPresenter: IInitializeable, IDisposable
    {
        //Model
        private readonly WalletService _walletService;
        private readonly WalletPresenterFactory _factory;
        private readonly List<CurrencyPresenter> _currencyPresenters = new();
        
        //view
        private readonly IconsWithTextList _view;
        
        public WalletPresenter(WalletService walletService, IconsWithTextList view,  WalletPresenterFactory factory)
        {
            _walletService = walletService;
            _view = view;
            _factory= factory;
        }
        
        public void Initialize()
        {
            foreach (var currencyType in _walletService.AvailableCurrencies)
            {
                var currencyView = _view.SpawnElement();

                var currencyPresenter = _factory.CreateCurrencyPresenter(currencyView, currencyType);
                currencyPresenter.Initialize();
                _currencyPresenters.Add(currencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach (var currencyPresenter in _currencyPresenters)
            {
                _view.Remove(currencyPresenter.View);
                currencyPresenter.Dispose();
            }
            
            _currencyPresenters.Clear();
        }
    }
}
