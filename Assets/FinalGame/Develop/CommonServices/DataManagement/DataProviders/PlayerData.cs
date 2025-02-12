using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Gameplay.Features.Stats;

namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public List<int> CompletedLevels;

        public Dictionary<StatTypes, int> StatUpgradeLevel;
    }
}
