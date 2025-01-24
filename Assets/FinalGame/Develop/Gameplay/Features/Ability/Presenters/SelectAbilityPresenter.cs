using System;
using System.Linq;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Presenters
{
    public class SelectAbilityPresenter
    {
        public event Action<SelectAbilityPresenter> Selected;
        
        private AbilityFactory _abilityFactory;
        private Entity _entity;

        private int _level;

        public SelectAbilityPresenter(
            AbilityConfig abilityConfig, 
            SelectAbilityView view,  
            AbilityFactory abilityFactory, 
            Entity entity,
            int level)
        {
            _abilityFactory = abilityFactory;
            _entity = entity;
            _level = level;
            AbilityConfig = abilityConfig;
            View = view;
        }

        public AbilityConfig AbilityConfig { get; }
        public SelectAbilityView View { get; }

        public void Enable()
        {
            View.SetName(AbilityConfig.Name);
            View.SetDescription(AbilityConfig.Description);
            View.Icon.SetIcon(AbilityConfig.Icon);

            InitByAbilityList();

            View.Selected += OnViewClicked;
            
        }

        public void Disable()
        {
            View.Selected -= OnViewClicked;
        }

        public void Provide()
        {
            Ability ability;

            if (AbilityConfig.IsUpgradable())
            {
                ability = _entity.GetAbilityList().Elements.FirstOrDefault(abil => abil.ID == AbilityConfig.ID);

                if (ability is not null)
                {
                    ability.AddLevel(_level);
                    return;
                }
            }

            ability = _abilityFactory.CreateAbilityFor(_entity, AbilityConfig, _level);
            _entity.GetAbilityList().Add(ability);
        }
        
        public void EnableSubscription() => View.Subscribe();
        
        public void DisableSubscription() => View.Unsubscribe();
        
        private void OnViewClicked() => Selected?.Invoke(this);
        
        private void InitByAbilityList()
        {
            if (AbilityConfig.IsUpgradable())
            {
                Ability ability = _entity.GetAbilityList().Elements.FirstOrDefault(abil => abil.ID == AbilityConfig.ID);

                if (ability is not null)
                {
                    View.Icon.ShowLevel();
                    View.Icon.SetLevel("LV." + ability.CurrentLevel.Value);
                    View.SetTabletText("LV." + ability.CurrentLevel.Value + "-> LV." + (ability.CurrentLevel.Value + _level));
                }
                else
                {
                    View.Icon.HideLevel();
                    View.SetTabletText("New LV." + _level);
                }
            }
            else
            {
                View.Icon.HideLevel();
                View.SetTabletText("NEW");
            }
        }

    }
}
