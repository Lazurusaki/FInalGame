using System.Linq;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using FinalGame.Resources.Configs.Gameplay.Abilities.DropOptions;

namespace FinalGame.Develop.Gameplay.Features.Ability.AbilityDrop
{
    public class AbilityDropRules
    {
        public bool IsAvailable(AbilityDropOption abilityDropOption, Entity entity)
        {
            if (abilityDropOption.Config.IsUpgradable())
            {
                if (entity.GetAbilityList().Elements.Any(ability =>
                        ability.ID == abilityDropOption.Config.ID
                        && ability.CurrentLevel.Value + abilityDropOption.Level > ability.MaxLevel))
                {
                    return false;
                }
            }
            
            switch (abilityDropOption.Config)
            {
                case StatChangeAbilityConfig statChangeAbilityConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                           && modifiedStats.ContainsKey(statChangeAbilityConfig.StatType);
            }

            return true;
        }
    }
}