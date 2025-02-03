using FinalGame.Develop.CommonServices.AssetsManagement;
using FinalGame.Develop.Configs.Common.Results;
using FinalGame.Develop.Configs.Common.Wallet;
using FinalGame.Develop.Configs.Gameplay;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Configs.Gameplay.Projectiles;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using FinalGame.Resources.Configs.Gameplay.Structures;
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
        public MainHeroConfig MainHeroConfig { get; private set; }
        public AbilitiesConfigContainer AbilitiesConfigContainer { get; private set; }        
        
        public ExperienceForLevelUplConfig ExperienceForLevelUplConfig { get; private set; }
        
        public CastleConfig CastleConfig { get; private set; }
        
        public BombConfig BombConfig { get; private set; }
        
        public ResultsIconsConfig GameResultsIconsConfig { get; private set; }
        
        public void LoadAll()
        {
            //add configs here
            
            LoadStartWalletConfig();
            LoadCurrencyIconsConfig();
            LoadLevelListConfig();
            LoadMainHeroConfig();
            LoadAbilitiesConfigContainer();
            LoadExperienceForLevelUpConfig();
            
            LoadCastleConfig();
            LoadBombConfig();
            LoadGameResultsIconsConfig();
        }

        private void LoadStartWalletConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");

        private void LoadCurrencyIconsConfig()
            => CurrencyIconsConfig =
                _resourcesAssetLoader.LoadResource<CurrencyIconsConfig>("Configs/Common/Wallet/CurrencyIconsConfig");

        private void LoadLevelListConfig()
            => LevelsListConfig =
                _resourcesAssetLoader.LoadResource<LevelListConfig>("Configs/Gameplay/Levels/LevelListConfig");

        private void LoadMainHeroConfig()
            => MainHeroConfig =
                _resourcesAssetLoader.LoadResource<MainHeroConfig>("Configs/Gameplay/Creatures/MainHeroConfig");

        private void LoadAbilitiesConfigContainer()
            => AbilitiesConfigContainer =
                _resourcesAssetLoader.LoadResource<AbilitiesConfigContainer>(
                    "Configs/Gameplay/Abilities/AbilitiesContainer");

        private void LoadExperienceForLevelUpConfig()
            => ExperienceForLevelUplConfig =
                _resourcesAssetLoader.LoadResource<ExperienceForLevelUplConfig>(
                    "Configs/Gameplay/ExperienceForLevelUplConfig");

        private void LoadCastleConfig()
            => CastleConfig =
                _resourcesAssetLoader.LoadResource<CastleConfig>(
                    "Configs/Gameplay/Structures/CastleConfig");
        
        private void LoadBombConfig()
            => BombConfig =
                _resourcesAssetLoader.LoadResource<BombConfig>(
                    "Configs/Gameplay/Projectiles/BombConfig");

        private void LoadGameResultsIconsConfig()
            => GameResultsIconsConfig =
                _resourcesAssetLoader.LoadResource<ResultsIconsConfig>(
                    "Configs/Common/Results/ResultsIconsConfig");
    }
}
