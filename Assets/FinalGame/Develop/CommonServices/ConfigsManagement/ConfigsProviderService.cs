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
        
        public void LoadAll()
        {
            //add configs here
            
            LoadStartWalletConfig();
            LoadCurrencyIconsConfig();
            LoadLevelListConfig();
        }

        

        private void LoadStartWalletConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");

        private void LoadCurrencyIconsConfig()
            => CurrencyIconsConfig =
                _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");

        private void LoadLevelListConfig()
            => LevelsListConfig =
                _resourcesAssetLoader.LoadResource<LevelListConfig>("Configs/Gameplay/Levels/LevelListConfig");
    }
}
