using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Creatures
{
    [CreateAssetMenu (menuName = "Configs/Gameplay/Creatures/GhostConfig", fileName = "GhostConfig")]
    public class GhostConfig : CreatureConfig
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float SelfTriggerDamage { get; private set; }
    }
}