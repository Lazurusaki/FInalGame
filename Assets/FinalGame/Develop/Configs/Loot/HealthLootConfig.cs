using UnityEngine;

namespace FinalGame.Develop.Configs.Loot
{
    [CreateAssetMenu(menuName = "Configs/Loot/HealthLootConfig", fileName = "HealthLootConfig")]
    public class HealthLootConfig : LootConfig
    {
        [field: SerializeField] public float Health { get; private set; }
    }
}