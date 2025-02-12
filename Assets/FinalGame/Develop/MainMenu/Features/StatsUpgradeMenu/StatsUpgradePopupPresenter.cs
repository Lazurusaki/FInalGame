using System.Collections.Generic;
using FinalGame.Develop.CommonUI.Wallet;
using FinalGame.Develop.Gameplay.Features.Stats;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Features.StatsUpgradeMenu
{
    public class StatsUpgradePopupPresenter
    {
        private readonly StatsUpgradePopupView _view;
        private readonly StatsUpgradePopupFactory _factory;
        private readonly WalletPresenterFactory _walletPresentersFactory;
        private readonly StatsUpgradeService _statsUpgradeService;

        private List<UpgradableStatPresenter> _upgradableStatPresenters = new();
        private WalletPresenter _walletPresenter;
        private CharacterPreviewPresenter _characterPreviewPresenter;

        public StatsUpgradePopupPresenter(
            StatsUpgradePopupView view,
            StatsUpgradePopupFactory statsPresentersFactory,
            WalletPresenterFactory walletPresentersFactory,
            StatsUpgradeService statsUpgradeService)
        {
            _view = view;
            _factory = statsPresentersFactory;
            _walletPresentersFactory = walletPresentersFactory;
            _statsUpgradeService = statsUpgradeService;
        }

        public void Enable()
        {
            _view.SetTitle("UPGRADE YOUR STATS");

            _view.CloseRequest += OnCloseRequest;

            _walletPresenter = _walletPresentersFactory.CreateWalletPresenter(_view.CurrencyListView);
            _walletPresenter.Initialize();

            _characterPreviewPresenter = _factory.CreateCharacterPreviewPresenter();
            _characterPreviewPresenter.Enable();    

            foreach (StatTypes statType in _statsUpgradeService.AvailableStats)
            {
                UpgradableStatView upgradableStatView = _view.SpawnElement();
                UpgradableStatPresenter upgradableStatPresenter = _factory.CreateUpgradableStatPresenter(upgradableStatView, statType);

                upgradableStatPresenter.Enable();
                _upgradableStatPresenters.Add(upgradableStatPresenter);
            }
        }

        public void Disable()
        {
            foreach (UpgradableStatPresenter presenter in _upgradableStatPresenters)
            {
                presenter.Disable();
                _view.Remove(presenter.View);
            }

            _upgradableStatPresenters.Clear();

            _walletPresenter.Dispose();
            _characterPreviewPresenter.Disable();

            _view.CloseRequest -= OnCloseRequest;    

            Object.Destroy(_view.gameObject);
        }

        private void OnCloseRequest()
        {
            Disable();
        }
    }
}