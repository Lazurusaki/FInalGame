using System;
using System.Collections.Generic;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class StatsEffect : IStatsEffect
    {
        private readonly StatTypes _statType;
        private readonly Func<float, float> _applyEffect;

        public StatsEffect(StatTypes statType, Func<float, float> applyEffect)
        {
            _statType = statType;
            _applyEffect = applyEffect;
        }

        public void ApplyTo(Dictionary<StatTypes, float> stats)
        {
            stats[_statType] = _applyEffect(stats[_statType]);
        }
    }
}