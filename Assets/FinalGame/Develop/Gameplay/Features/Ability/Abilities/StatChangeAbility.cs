using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Stats;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class StatChangeAbility : Ability
    {
        private readonly Entity _entity;
        private readonly StatChangeAbilityConfig _config;

        public StatChangeAbility(Entity entity, StatChangeAbilityConfig config, int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _entity = entity;
            _config = config;
        }
               
        public override void Activate()
        {
            Debug.Log("activate");
            _entity.GetStatsEffectsList().Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}
