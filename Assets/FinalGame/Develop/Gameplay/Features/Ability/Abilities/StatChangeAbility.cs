using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Stats;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class StatChangeAbility : IAbility
    {
        private readonly Entity _entity;
        private readonly StatChangeAbilityConfig _config;
        
        public StatChangeAbility(Entity entity, StatChangeAbilityConfig config)
        {
            _entity = entity;
            _config = config;
        }

        public string ID => _config.ID;
               
        public void Activate()
        {
            _entity.GetStatsEffectsList().Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}
