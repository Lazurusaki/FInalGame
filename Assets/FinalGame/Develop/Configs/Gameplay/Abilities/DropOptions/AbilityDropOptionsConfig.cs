using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Abilities.DropOptions
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/AbilityDropOptionsConfig",
        fileName = "AbilityDropOptionsConfig")]
    public class AbilityDropOptionsConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityDropOption> _dropOptions;

        public IReadOnlyList<AbilityDropOption> DropOptions => _dropOptions;
    }
}