using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;
using UnityEngine;
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
            return new PlayerData();
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData.Add(currencyType, _configsProviderService.StartWalletConfig.getStartValueFor(currencyType));

            return walletData;
        }
    }
}
