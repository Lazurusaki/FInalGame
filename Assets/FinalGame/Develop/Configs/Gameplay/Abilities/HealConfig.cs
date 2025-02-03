using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/HealConfig", fileName = "HealConfig")]
    public class HealConfig : AbilityConfig
    {
        [SerializeField] private float _value;

        public float Value => _value;
        
        public override int MaxLevel { get; }
    }
}