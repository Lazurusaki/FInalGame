
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Wallet;
using FinalGame.Develop.Configs.Gameplay.Levels.WaveStage;
using FinalGame.Resources.Configs.Gameplay.Abilities.DropOptions;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private Currency _reward;
        [SerializeField] private float _maxHealth;
        [SerializeField] private List<WaveConfig> _waveConfigs;

        public IReadOnlyList<WaveConfig> WaveConfigs => _waveConfigs;
        
        public float MaxHealth => _maxHealth;
        public Currency Reward => _reward;
    }
}
