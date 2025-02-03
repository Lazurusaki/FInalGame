using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Creatures
{
    [CreateAssetMenu (menuName = "Configs/Gameplay/Creatures/BomberConfig", fileName = "BomberConfig")]
    public class BomberConfig : CreatureConfig
    {
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        
        [field: SerializeField] public float DetonateDistance { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        
        [field: SerializeField] public float DamageRadius { get; private set; }
    }
}