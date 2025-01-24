using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/AddArrowsAbilityConfig", fileName = "AddArrowsAbilityConfig")]
    public class AddArrowsAbilityConfig : AbilityConfig
    {
        [SerializeField] private List<Config> _addArrowsByLevel;

        public override int MaxLevel => _addArrowsByLevel.Count;

        public List<DirectionShootConfig> GetBy(int level) => _addArrowsByLevel[level - 1].DirectionShootConfigs;

        [Serializable]
        private class Config
        {
            [field: SerializeField] public List<DirectionShootConfig> DirectionShootConfigs { get; private set; }
        }
    }

    [Serializable]
    public class DirectionShootConfig
    {
        [field: SerializeField] public int Angle { get; private set; }
        [field: SerializeField] public int NumberOfProjectiles { get; private set; }
    }
    
}