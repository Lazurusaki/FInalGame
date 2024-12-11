using System;
using FinalGame.Develop.CommonServices.DataManagement;

namespace FinalGame.Develop.ADV_02
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public GameStats GameStats;
    }
    
    public struct GameStats
    {
        public int Wins; 
        public int Losses;
    }
}