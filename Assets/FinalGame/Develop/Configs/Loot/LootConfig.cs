using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Configs.Loot
{
    
    public class LootConfig: ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public Entity Prefab { get; private set; }
        
    }
}