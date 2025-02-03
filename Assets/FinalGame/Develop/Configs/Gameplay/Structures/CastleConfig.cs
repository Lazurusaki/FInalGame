using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Structures
{
    [CreateAssetMenu (menuName = "Configs/Gameplay/Structures/CastleConfig", fileName = "CastleConfig")]
    public class CastleConfig : ScriptableObject
    {
            [field: SerializeField] public float Damage { get; private set; }
            [field: SerializeField] public float DamageRadius { get; private set; }
            [field: SerializeField] public float AttackCooldown { get; private set; }
    }
}