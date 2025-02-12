using System;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Configs.Player.Stats;
using FinalGame.Develop.Gameplay.Features.Stats;
using UnityEngine;

namespace FinalGame.Develop.MainMenu.Features.StatsUpgradeMenu
{
    public class UpgradableStatPresenter
    {
        private UpgradableStatView _view;
        private StatsViewConfig _statsViewConfig;
        private StatsUpgradeService _upgradeStatsService;
        private WalletService _walletService;
        private StatTypes _statType;
        private CurrencyIconsConfig _currencyIconsConfig;

        public UpgradableStatPresenter(
            UpgradableStatView view,
            StatTypes type,
            StatsViewConfig statsShowConfig,
            StatsUpgradeService upgradeStatsService,
            WalletService walletService,
            CurrencyIconsConfig currencyIconsConfig)
        {
            _view = view;
            _statsViewConfig = statsShowConfig;
            _upgradeStatsService = upgradeStatsService;
            _walletService = walletService;
            _statType = type;
            _currencyIconsConfig = currencyIconsConfig;
        }

        public UpgradableStatView View => _view;

        public void Enable()
        {
            StatViewConfig statShowData = _statsViewConfig.GetStatViewData(_statType);

            _view.Initialize(statShowData.Name, statShowData.Sprite, GetStatValueText());

            UpdateBuyButtonState();

            _view.BuyButtonView.Click += OnBuyButtonClicked;
            _upgradeStatsService.GetStatLevelFor(_statType).Changed += OnStatUpgradeLevelChanged;

            _walletService.GetCurrency(_upgradeStatsService.GetUpgradeCostTypeFor(_statType)).Changed += OnWalletChanged;
        }

        public void Disable()
        {
            _view.BuyButtonView.Click -= OnBuyButtonClicked;
            _upgradeStatsService.GetStatLevelFor(_statType).Changed -= OnStatUpgradeLevelChanged;
            _walletService.GetCurrency(_upgradeStatsService.GetUpgradeCostTypeFor(_statType)).Changed -= OnWalletChanged;
        }

        private void OnStatUpgradeLevelChanged(int arg1, int arg2) => _view.SetStatValueText(GetStatValueText());


        private void OnWalletChanged(int arg1, int arg2) => UpdateBuyButtonState();


        private void OnBuyButtonClicked()
        {
            if (_upgradeStatsService.TryGetUpgradeCostFor(_statType, out CurrencyTypes currencyType, out int cost))
            {
                if(_walletService.HasEnough(currencyType, cost))
                {
                    if (_upgradeStatsService.TryUpgradeStat(_statType) == false)
                        throw new Exception();

                    _walletService.Spend(currencyType, cost);
                }
                else
                {
                    Debug.Log("Not enough currency");
                }
            }
            else
            {
                Debug.Log("Already max");
            }
        }

        private void UpdateBuyButtonState()
        {
            if(_upgradeStatsService.TryGetUpgradeCostFor(_statType, out CurrencyTypes currencyType, out int cost))
            {
                _view.BuyButtonView.SetPriceText(cost.ToString());
                _view.BuyButtonView.ShowIcon();
                _view.BuyButtonView.SetIcon(_currencyIconsConfig.GetSpriteFor(currencyType));

                if (_walletService.HasEnough(currencyType, cost))
                    _view.BuyButtonView.Unlock();
                else
                    _view.BuyButtonView.Lock();
            }
            else
            {
                _view.BuyButtonView.HideIcon();
                _view.BuyButtonView.Lock();
                _view.BuyButtonView.SetPriceText("MAX");
            }
        }

        private string GetStatValueText()
        {
            float statValue = _upgradeStatsService.GetCurrentStatValueFor(_statType);
            string result = statValue.ToString();

            if(_upgradeStatsService.TryGetStatValueForNextLevel(_statType, out float nextStatValue))
            {
                result += $"<color=green>>{nextStatValue}</color>";
            }

            return result;
        }
    }
}