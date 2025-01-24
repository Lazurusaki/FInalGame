using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/StatChangeAbilityConfigContainer",
        fileName = "StatChangeAbilityConfigContainer")]
    public class AbilitiesConfigContainer : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilityConfigs;
        
        private IReadOnlyList<AbilityConfig> AbilityConfigs => _abilityConfigs;
        public AbilityConfig GetConfigBy(string ID)
            => _abilityConfigs.First(config => config.ID == ID);
    }
}