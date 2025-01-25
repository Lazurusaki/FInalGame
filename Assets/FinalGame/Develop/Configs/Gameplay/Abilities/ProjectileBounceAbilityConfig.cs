using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/ProjectileBounceAbilityConfig", fileName = "ProjectileBounceAbilityConfig")]
    public class ProjectileBounceAbilityConfig : AbilityConfig
    {
        [SerializeField] private List<int> _bounceCountByLevel;
        
        [field: SerializeField] public LayerMask BoucneLayer { get; private set; }

        public override int MaxLevel => _bounceCountByLevel.Count;

        public int GetBounceCountBy(int level) => _bounceCountByLevel[level - 1];
    }
}