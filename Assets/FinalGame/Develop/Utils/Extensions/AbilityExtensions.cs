using FinalGame.Develop.Gameplay.Features.Ability;
using FinalGame.Resources.Configs.Gameplay.Abilities;

namespace FinalGame.Develop.Utils.Extensions
{
    public static class AbilityExtensions
    {
        public static bool IsUpgradable(this Ability ability) => IsUpgradable(ability.MaxLevel);
        
        public static bool IsUpgradable(this AbilityConfig config) => IsUpgradable(config.MaxLevel);

        private static bool IsUpgradable(int maxLevel) => maxLevel > 1;
    }
}