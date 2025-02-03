using System;
using FinalGame.Develop.Configs.Gameplay.Creatures;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Levels.WaveStage
{
    [Serializable]
    public class WaveItemConfig
    {
        [field: SerializeField] public CreatureConfig EnemyConfig { get; private set; }
       
        [field: SerializeField] public int EnemiesCount { get; private set; }
    }
}