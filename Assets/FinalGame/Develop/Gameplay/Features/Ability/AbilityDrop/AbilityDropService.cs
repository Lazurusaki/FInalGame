using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Resources.Configs.Gameplay.Abilities.DropOptions;

namespace FinalGame.Develop.Gameplay.Features.Ability.AbilityDrop
{
    public class AbilityDropService
    {
        private AbilityDropOptionsConfig _abilityDropOptionsConfig;
        private AbilityDropRules _abilityDropRules;

        public AbilityDropService(AbilityDropOptionsConfig abilityDropOptionsConfig, AbilityDropRules abilityDropRules)
        {
            _abilityDropOptionsConfig = abilityDropOptionsConfig;
            _abilityDropRules = abilityDropRules;
        }

        public List<AbilityDropOption> Drop(int count, Entity entity)
        {
            List<AbilityDropOption> availableDropOptions
                = new List<AbilityDropOption>(_abilityDropOptionsConfig.DropOptions
                    .Where(dropOption => _abilityDropRules.IsAvailable(dropOption, entity)));

            List<AbilityDropOption> selectedOptions = new();

            for (int i = 0; i < count; i++)
            {
                AbilityDropOption selectedOption =
                    availableDropOptions[UnityEngine.Random.Range(0, availableDropOptions.Count)];
                selectedOptions.Add(selectedOption);
                availableDropOptions.Remove(selectedOption);
            }

            return selectedOptions;
        }
    }
}