using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Wallet;
using UnityEngine;
using CurrencyTypes = FinalGame.Develop.CommonServices.Wallet.CurrencyTypes;

namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        public PlayerDataProvider(ISaveLoadService saveLoadService) : base(saveLoadService)
        { 
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData
            {
                WalletData = InitWalletData()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData.Add(currencyType, 0);

            return walletData;
        }
    }
}
