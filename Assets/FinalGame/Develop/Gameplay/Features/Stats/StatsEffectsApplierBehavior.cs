using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using UnityEngine.Rendering;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class StatsEffectsApplierBehavior : IEntityInitialize, IEntityDispose
    {
        private  StatsEffectsList _statsEffectsList;
        private  Dictionary<StatTypes, float> _baseStats;
        private  Dictionary<StatTypes, float> _modifiedStats;
        
        public void OnInit(Entity entity)
        {
            _statsEffectsList = entity.GetStatsEffectsList();
            _baseStats = entity.GetBaseStats();
            _modifiedStats = entity.GetModifiedStats();

            _statsEffectsList.Added += OnStatsEffectAdded;
            _statsEffectsList.Removed += OnStatsEffectRemoved;
            
            RecalculateStats();
        }

        private void OnStatsEffectRemoved(IStatsEffect obj)
            => RecalculateStats();

        private void OnStatsEffectAdded(IStatsEffect statsEffect)
            => RecalculateStats();

        public void OnDispose()
        {
            _statsEffectsList.Added -= OnStatsEffectAdded;
        }

        private void RecalculateStats()
        {
            foreach (var stat in _baseStats.Keys)
                _modifiedStats[stat] = _baseStats[stat];
            
            foreach ( var statEffect in _statsEffectsList.Elements)
                statEffect.ApplyTo(_modifiedStats);
        }
    }
}