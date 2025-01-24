using UnityEditor;
using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        
        public abstract int MaxLevel { get; }
        
        //meta data
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}


