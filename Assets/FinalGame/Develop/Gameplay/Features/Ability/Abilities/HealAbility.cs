using FinalGame.Develop.Configs.Gameplay.Abilities;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Stats;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Abilities
{
    public class HealAbility : Ability
    {
        private readonly Entity _entity;
        private readonly HealConfig _config;

        public HealAbility(Entity entity, HealConfig config, int currentLevel) : base(
            config.ID, currentLevel, config.MaxLevel)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.GetHealRequest().Invoke(_config.Value);

            // _entity.GetStatsEffectsList().Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}