using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability
{
    public class AbilityOnAddActivatorBehavior: IEntityInitialize, IEntityDispose
    {
        private AbilityList _abilityList;
        public void OnInit(Entity entity)
        {
            _abilityList = entity.GetAbilityList();
            _abilityList.Added += OnAbilityAdded;

            foreach (var ability in _abilityList.Elements)
                ability.Activate();
        }

        private void OnAbilityAdded(Ability ability)
        {
            ability.Activate();
        }

        public void OnDispose()
        {
            _abilityList.Added -= OnAbilityAdded;
        }
    }
}