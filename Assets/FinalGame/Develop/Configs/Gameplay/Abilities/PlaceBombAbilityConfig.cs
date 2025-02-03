using FinalGame.Develop.Configs.Gameplay.Projectiles;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/PlaceBombAbilityConfig", fileName = "PlaceBombAbilityConfig")]
    public class PlaceBombAbilityConfig : AbilityConfig
    {
        [SerializeField] private BombConfig _bombConfig;
    
    
        public override int MaxLevel { get; }
    
    }
}
