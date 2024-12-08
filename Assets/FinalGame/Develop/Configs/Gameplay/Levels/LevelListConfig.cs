using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/LevelListConfig", fileName = "LevelListConfig")]
    public class LevelListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetBy(int level)
        {
            var levelIndex = level - 1;

            if (level > _levels.Count)
                throw new ArgumentException($"Level {nameof(level)} is not exist");

            return _levels[levelIndex];
        }
    }
}
