using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Creatures
{
    [CreateAssetMenu (menuName = "Configs/Gameplay/Creatures/MainHeroConfig", fileName = "MainHeroConfig")]
    public class MainHeroConfig : CreatureConfig
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float AttackInterval { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}