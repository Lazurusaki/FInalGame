using System;
using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.AbilityDrop;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using FinalGame.Resources.Configs.Gameplay.Abilities.DropOptions;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Presenters
{
    public class SelectAbilityListPresenter
    {
        public event Action ProvideComplete;
        
        private const int AbilitiesCount = 3;

        private readonly SelectAbilityListView _view;
        
        private Entity _entity;
        private AbilityDropService _abilityDropService;
        private AbilityPresentersFactory _abilityPresentersFactory;

        private List<SelectAbilityPresenter> _presenters = new();
        private SelectAbilityPresenter _selectedPresenter;

        public SelectAbilityListPresenter(SelectAbilityListView view, Entity entity, AbilityDropService abilityDropService, AbilityPresentersFactory abilityPresentersFactory)
        {
            _view = view;
            _entity = entity;
            _abilityDropService = abilityDropService;
            _abilityPresentersFactory = abilityPresentersFactory;
        }

        public void Enable()
        {
            _view.Selected += OnSelected;

            List<AbilityDropOption> dropOptions = _abilityDropService.Drop(AbilitiesCount, _entity);
            
            for (var i = 0; i < dropOptions.Count; i++)
            {
                SelectAbilityView selectAbilityView = _view.SpawnItem();

                SelectAbilityPresenter presenter = _abilityPresentersFactory
                    .CreateSelectAbilityPresenter(dropOptions[i].Config, selectAbilityView, _entity, dropOptions[i].Level);

                presenter.Selected += OnPresenterSelected;
                
                presenter.Enable();
                _presenters.Add(presenter);
            }
        }

        public void Disable()
        {
            foreach (var presenter in _presenters)
            {
                _view.Remove(presenter.View);
                presenter.Selected -= OnPresenterSelected;
                presenter.Disable();
            }
            
            _presenters.Clear();
            _view.Selected -= OnSelected;
        }

        public void EnableSubscription()
        {
            _view.Subscribe();

            foreach (var presenter in _presenters)
                presenter.EnableSubscription();
        }
        
        public void DisableSubscription()
        {
            _view.Unsubscribe();

            foreach (var presenter in _presenters)
                presenter.DisableSubscription();
        }

        private void OnPresenterSelected(SelectAbilityPresenter selected)
        {
            _view.Select(selected.View);
            _selectedPresenter = selected;
        }
        
        private void OnSelected()
        {
            _selectedPresenter.Provide();
            ProvideComplete?.Invoke();
        }

        
    }
}