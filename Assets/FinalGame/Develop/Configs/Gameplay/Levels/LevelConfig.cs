
using System.Collections.Generic;
using FinalGame.Develop.Configs.Gameplay.Levels.WaveStage;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<WaveConfig> _waveConfigs;

        public IReadOnlyList<WaveConfig> WaveConfigs => _waveConfigs;
    }
}
