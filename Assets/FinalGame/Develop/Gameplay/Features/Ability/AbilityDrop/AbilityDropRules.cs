using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using FinalGame.Resources.Configs.Gameplay.Abilities.DropOptions;

namespace FinalGame.Develop.Gameplay.Features.Ability.AbilityDrop
{
    public class AbilityDropRules
    {
        public bool IsAvailable(AbilityDropOption abilityDropOption, Entity entity)
        {
            switch (abilityDropOption.Config)
            {
                case StatChangeAbilityConfig statChangeAbilityConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                           && modifiedStats.ContainsKey(statChangeAbilityConfig.StatType);
            }

            return false;
        }
    }
}