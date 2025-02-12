using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.CommonServices.DataManagement.DataProviders;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Player.Stats;
using FinalGame.Develop.ConfigsManagement;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class StatsUpgradeService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private ConfigsProviderService _configsProviderService;

        public event Action<StatTypes> Upgraded;

        private Dictionary<StatTypes, ReactiveVariable<int>> _statLevels = new();

        public StatsUpgradeService(PlayerDataProvider playerDataProvider, ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public List<StatTypes> AvailableStats => _statLevels.Keys.ToList();

        public IReadOnlyVariable<int> GetStatLevelFor(StatTypes statType)
            => _statLevels[statType];

        private PlayerStatsUpgradeConfig PlayerStatsByLevelConfig => _configsProviderService.StatsUpgradesConfig;

        public float GetCurrentStatValueFor(StatTypes type)
        {
            return PlayerStatsByLevelConfig.GetStatConfig(type).StatValues[_statLevels[type].Value - 1];
        }

        public CurrencyTypes GetUpgradeCostTypeFor(StatTypes type)
        {
            return PlayerStatsByLevelConfig.GetStatConfig(type).CostType;
        }

        public bool TryGetStatValueForNextLevel(StatTypes type, out float statValue)
        {
            StatUpgradeCostConfig statData = PlayerStatsByLevelConfig.GetStatConfig(type);

            if (statData.StatValues.Count <= _statLevels[type].Value)
            {
                statValue = 0;
                return false;
            }

            statValue = statData.StatValues[_statLevels[type].Value];
            return true;
        }

        public bool TryGetUpgradeCostFor(StatTypes type, out CurrencyTypes costType, out int cost)
        {
            StatUpgradeCostConfig statData = PlayerStatsByLevelConfig.GetStatConfig(type);

            if (statData.UpgradeToNextLevelCost.Count <= _statLevels[type].Value - 1)
            {
                costType = default(CurrencyTypes);
                cost = 0;
                return false;
            }

            costType = statData.CostType;
            cost = statData.UpgradeToNextLevelCost[_statLevels[type].Value - 1];
            return true;
        }

        public bool TryUpgradeStat(StatTypes type)
        {
            StatUpgradeCostConfig statData = PlayerStatsByLevelConfig.GetStatConfig(type);

            if (statData.StatValues.Count <= _statLevels[type].Value)
                return false;

            _statLevels[type].Value += 1;
            Upgraded?.Invoke(type);
            return true;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (var statLevel in data.StatUpgradeLevel)
            {
                if (_statLevels.ContainsKey(statLevel.Key))
                    _statLevels[statLevel.Key].Value = statLevel.Value;
                else
                    _statLevels.Add(statLevel.Key, new ReactiveVariable<int>(statLevel.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (var stat in _statLevels)
            {
                if (data.StatUpgradeLevel.ContainsKey(stat.Key))
                    data.StatUpgradeLevel[stat.Key] = stat.Value.Value;
                else
                    data.StatUpgradeLevel.Add(stat.Key, stat.Value.Value);
            }
        }
    }
}