using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.DataManagement;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.ConfigsManagement;

namespace FinalGame.Develop.ADV_02
{
    public class GameDataProvider //: DataProvider<GameData>
    {
        private readonly ConfigsProviderService _configsProviderService;
        
        // public GameDataProvider (ISaveLoadService saveLoadService, 
        //     ConfigsProviderService configsProviderService) : base(saveLoadService)
        // {
        //     _configsProviderService = configsProviderService;
        // }
        //
        // protected override GameData GetOriginData()
        // {
        //     return new GameData
        //     {
        //         WinReward = _configsProviderService.GameConfig.WinRewrd,
        //         LossPenalty = _configsProviderService.GameConfig.LossPenalty
        //     };
        // }
    }
}