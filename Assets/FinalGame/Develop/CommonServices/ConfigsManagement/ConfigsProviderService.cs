using FinalGame.Develop.ADV_02;
using FinalGame.Develop.ADV_02.Configs;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Gameplay;
using UnityEngine;

namespace FinalGame.Develop.ConfigsManagement
{
    public class ConfigsProviderService
    {
        private ResourcesAssetLoader _resourcesAssetLoader;

        public ConfigsProviderService(ResourcesAssetLoader resourcesAssetLoader)
        {
            _resourcesAssetLoader = resourcesAssetLoader;
        }
        
        public StartWalletConfig StartWalletConfig { get; private set; }
        
        public CurrencyIconsConfig CurrencyIconsConfig { get; private set; }
        
        public LevelListConfig LevelsListConfig { get; private set; }
        
        //ADV_02
        public GameResultIconsConfig GameResultIconsConfig { get; private set; }
        public GameConfig GameConfig { get; private set; }
        
        public void LoadAll()
        {
            //add configs here
            
            LoadStartWalletConfig();
            LoadCurrencyIconsConfig();
            LoadLevelListConfig();
            
            //ADV_02
            LoadGameResultsIconsConfig();
            LoadGameConfig();
        }
        
        private void LoadStartWalletConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");

        private void LoadCurrencyIconsConfig()
            => CurrencyIconsConfig =
                _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");

        private void LoadLevelListConfig()
            => LevelsListConfig =
                _resourcesAssetLoader.LoadResource<LevelListConfig>("Configs/Gameplay/Levels/LevelListConfig");
        
        //ADV_02
        private void LoadGameResultsIconsConfig()
            => GameResultIconsConfig =
                _resourcesAssetLoader.LoadResource<GameResultIconsConfig>("ADV_02/GameResultIconsConfig");

        private void LoadGameConfig()
            => GameConfig = _resourcesAssetLoader.LoadResource<GameConfig>("ADV_02/GameConfig");
    }
}
