using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Configs.Loot;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class DropLootService
    {
        private const float HealthDropChance = 0.5f;
        private const float CoinsDropChance = 0.5f;
        private const float ExpDropChance = 1;
        private const int ExpPotionSize = 300;
        
        
        private readonly LootListConfig _lootListConfig;
        private readonly LootFactory _lootFactory;

        public DropLootService(ConfigsProviderService configsProviderService, LootFactory lootFactory)
        {
            _lootListConfig = configsProviderService.LootListConfig;
            _lootFactory = lootFactory;
        }

        public void DropLootFor(Entity entity)
        {
            Transform entityTransform = entity.GetTransform();
            
            DropExp(entityTransform.position);
            DropHealth(entityTransform.position);
            DropCoins(entityTransform.position);
        }

        private void DropExp(Vector3 position)
        {
            if (_lootListConfig is null) Debug.Log("sdsd");
            
            List<ExperienceLootConfig> expConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(ExperienceLootConfig))
                .Cast<ExperienceLootConfig>()
                .ToList();
            
            if (expConfigs.Count > 0 && Random.value <= ExpDropChance)
            {
                ExperienceLootConfig expLootConfig = expConfigs[Random.Range(0, expConfigs.Count)];
                
                int potionsCount = expLootConfig.Experience / ExpPotionSize;
                int restOfExp = expLootConfig.Experience % ExpPotionSize;

                for (int i = 0; i < potionsCount; i++)
                    _lootFactory.CreateExperienceLoot(expLootConfig.Prefab, position, ExpPotionSize);

                if (restOfExp > 0)
                    _lootFactory.CreateExperienceLoot(expLootConfig.Prefab, position, restOfExp);
            }    
        }
        
        private void DropCoins(Vector3 position)
        {
            List<CoinsLootConfig> coinConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(CoinsLootConfig))
                .Cast<CoinsLootConfig>()
                .ToList();

            if (coinConfigs.Count > 0 && Random.value <= CoinsDropChance)
            {
                CoinsLootConfig coinsLootConfig = coinConfigs[Random.Range(0, coinConfigs.Count)];
                _lootFactory.CreateCoinsLoot(coinsLootConfig.Prefab, position, coinsLootConfig.Coins);
            }
        }
        
        private void DropHealth(Vector3 position)
        {
            List<HealthLootConfig> healthConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(HealthLootConfig))
                .Cast<HealthLootConfig>()
                .ToList();

            if (healthConfigs.Count > 0 && Random.value <= HealthDropChance)
            {
                HealthLootConfig healthLootConfig = healthConfigs[Random.Range(0, healthConfigs.Count)];
                _lootFactory.CreateHealthLoot(healthLootConfig.Prefab, position, healthLootConfig.Health);
            }
        }
    }
}