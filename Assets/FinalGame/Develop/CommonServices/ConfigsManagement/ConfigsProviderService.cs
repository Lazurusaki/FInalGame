using FinalGame.Develop.ADV_02.Configs;
using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels;
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

        public GameResultIconsConfig GameResultIconsConfig { get; private set; }
        
        public GameRewardsConfig GameRewardsConfig { get; private set; }
        
        public void LoadAll()
        {
            //add configs here
            
            LoadStartWalletConfig();
            LoadCurrencyIconsConfig();
            LoadLevelListConfig();
            
            //ADV_02
            LoadGameResultIconsConfig();
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
        
        private void LoadGameResultIconsConfig()
            => GameResultIconsConfig = _resourcesAssetLoader.LoadResource<GameResultIconsConfig>("Configs/ADV_02/GameResulIconsConfig");
        
        private void LoadGameConfig()
            => GameRewardsConfig = _resourcesAssetLoader.LoadResource<GameRewardsConfig>("Configs/ADV_02/GameRewardsConfig");
    }
}
