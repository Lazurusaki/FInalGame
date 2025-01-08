using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Levels.WaveStage
{
    [Serializable]
    public class WaveConfig
    {
        [SerializeField] private List<WaveItemConfig> _waveItems;

        public IReadOnlyList<WaveItemConfig> WaveItems => _waveItems;
    }
}
