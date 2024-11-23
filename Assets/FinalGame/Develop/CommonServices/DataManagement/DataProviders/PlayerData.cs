using System;
using System.Collections.Generic;

namespace FinalGame.Develop.CommonServices.DataManagement.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public int Money;
        public List<int> CompletedLevels;
    }
}
