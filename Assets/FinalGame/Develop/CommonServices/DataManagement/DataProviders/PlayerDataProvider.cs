using System;
using System.Collections.Generic;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.Gameplay.Features.Stats;
using CurrencyTypes = FinalGame.Develop.CommonServices.Wallet.CurrencyTypes;

namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;
        
        public PlayerDataProvider(ISaveLoadService saveLoadService, 
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                WalletData = InitWalletData(),
                CompletedLevels = new (),
                StatUpgradeLevel = InitStatsUpgradesLevels()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData.Add(currencyType, _configsProviderService.StartWalletConfig.getStartValueFor(currencyType));

            return walletData;
        }

        private Dictionary<StatTypes, int> InitStatsUpgradesLevels()
        {
            Dictionary<StatTypes, int> statsUpgradesLevels = new();

            foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
                statsUpgradesLevels.Add(statType, 1);

            return statsUpgradesLevels;
        }
    }
}
