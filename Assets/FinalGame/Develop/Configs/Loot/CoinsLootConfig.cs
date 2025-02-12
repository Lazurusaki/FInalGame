using UnityEngine;

namespace FinalGame.Develop.Configs.Loot
{
    [CreateAssetMenu(menuName = "Configs/Loot/CoinsLootConfig", fileName = "CoinsLootConfig")]
    public class CoinsLootConfig : LootConfig
    {
        [field: SerializeField] public int Coins { get; private set; }
    }
}