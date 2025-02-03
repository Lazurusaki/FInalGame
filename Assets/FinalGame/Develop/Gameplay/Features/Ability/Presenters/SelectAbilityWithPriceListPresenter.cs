using System;
using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using FinalGame.Resources.Configs.Gameplay.Abilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Ability.Presenters
{
    public class SelectAbilityWithPriceListPresenter
    {
        public event Action ProvideComplete;
        
        private const int AbilitiesCount = 3;

        private readonly SelectAbilityWithPriceListView _view;
        
        private AbilityPresentersFactory _abilityPresentersFactory;

        private List<SelectAbilityWithPricePresenter> _presenters = new();
        private SelectAbilityWithPricePresenter _selectedPresenter;
        private AbilitiesConfigContainer _abilityConfigs;

        public SelectAbilityWithPriceListPresenter(SelectAbilityWithPriceListView view, Entity entity, AbilityPresentersFactory abilityPresentersFactory, AbilitiesConfigContainer abilityConfigs)
        {
            _view = view;
            _abilityPresentersFactory = abilityPresentersFactory;
            _abilityConfigs = abilityConfigs;
        }

        public void Enable()
        {
            _view.Selected += OnSelected;
            
            for (var i = 0; i < _abilityConfigs.AbilityConfigs.Count; i++)
            {
                SelectAbilityWithPriceView selectAbilityView = _view.SpawnItem();

                SelectAbilityWithPricePresenter presenter = _abilityPresentersFactory
                    .CreateSelectAbilityWithPricePresenter(_abilityConfigs.AbilityConfigs[i], selectAbilityView);
                
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

        private void OnPresenterSelected(SelectAbilityWithPricePresenter selected)
        {
            _view.Select(selected.View);
            _selectedPresenter = selected;
        }
        
        private void OnSelected()
        {
            _selectedPresenter.Provide();
            ProvideComplete?.Invoke();
            _view.ResetSelection();
        }
    }
}