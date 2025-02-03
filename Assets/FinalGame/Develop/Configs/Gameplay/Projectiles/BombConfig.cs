using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Projectiles
{
    [CreateAssetMenu (menuName = "Configs/Gameplay/Projectiles/BombConfig", fileName = "BombConfig")]
    public class BombConfig : ScriptableObject
    {
        [field : SerializeField] public float Damage { get; private set; }
        [field : SerializeField] public float DamageRadius { get; private set; }
    }
}