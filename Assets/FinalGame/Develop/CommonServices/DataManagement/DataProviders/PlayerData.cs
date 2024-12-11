using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Wallet;

namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public List<int> CompletedLevels;
        
        public GameStats GameStats;
    }
    
    public struct GameStats
    {
        public int Wins; 
        public int Losses;
    }
}
