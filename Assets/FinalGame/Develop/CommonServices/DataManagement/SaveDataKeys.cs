using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;

namespace FinalGame.Develop.CommonServices.DataManagement
{
    public static class SaveDataKeys
    {
        private static Dictionary<Type, string> Keys = new Dictionary<Type, string>()
        {
            {typeof(PlayerData), "PlayerData" }
        };

        public static string GetKeyFor<TData>() where TData : ISaveData
            => Keys[typeof(TData)];
    }
}
