using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.Configs.Gameplay
{
    [CreateAssetMenu (menuName = "Configs/Gameplay/ExperienceForLevelUplConfig", fileName = "ExperienceForLevelUplConfig")]
    public class ExperienceForLevelUplConfig : ScriptableObject
    {
        [SerializeField] private List<float> _experienceForLevel;

        public int MaxLevel => _experienceForLevel.Count;

        public float GetExperienceFor(int level)
        {
            return _experienceForLevel[level - 1];
        }
    }
}