using UnityEngine;

namespace FinalGame.Develop.Configs.Loot
{
    [CreateAssetMenu(menuName = "Configs/Loot/ExperienceLootConfig", fileName = "ExperienceLootConfig")]
    public class ExperienceLootConfig : LootConfig
    {
        [field: SerializeField] public int Experience { get; private set; }
    }
}