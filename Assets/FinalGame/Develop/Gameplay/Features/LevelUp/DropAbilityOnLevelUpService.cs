using System;
using System.Collections;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.Presenters;
using FinalGame.Develop.Gameplay.Features.MainHero;
using FinalGame.Develop.Gameplay.Features.Pause;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.LevelUp
{
    public class DropAbilityOnLevelUpService : IDisposable
    {
        private MainHeroHolderService _mainHeroHolderService;
        private AbilityPresentersFactory _abilityPresentersFactory;
        private IPauseService _pauseService;
        private ICoroutinePerformer _coroutinePerformer;

        private Queue<int> _levelUpRequests = new();

        private SelectAbilityPopupPresenter _popup;
        private Coroutine _selectAbilityProcess;

        private IDisposable _hereRegisteredDisposable;
        private IDisposable _hereUnregisteredDisposable;
        
        public DropAbilityOnLevelUpService(MainHeroHolderService mainHeroHolderService, AbilityPresentersFactory abilityPresentersFactory, IPauseService pauseService, ICoroutinePerformer coroutinePerformer)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _abilityPresentersFactory = abilityPresentersFactory;
            _pauseService = pauseService;
            _coroutinePerformer = coroutinePerformer;

            _mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered);
            _mainHeroHolderService.HeroUnregistered.Subscribe(OnMainHeroUnregistered);
        }

        private bool PopupIsOpened => _popup is not null;

        private void OnMainHeroRegistered(Entity hero) => hero.GetLevel().Changed += OnHeroLevelChanged;

        private void OnMainHeroUnregistered(Entity hero) => hero.GetLevel().Changed -= OnHeroLevelChanged;
        
        private void OnHeroLevelChanged(int arg1, int currentLevel)
        {
            _levelUpRequests.Enqueue(currentLevel);
            
            if (_selectAbilityProcess != null)
                return;

            _selectAbilityProcess = _coroutinePerformer.StartPerform(SelectAbilityProcess());
        }

        private IEnumerator SelectAbilityProcess()
        {
            while (_levelUpRequests.Count > 0)
            {
                int level = _levelUpRequests.Dequeue();
                
                _pauseService.Pause();
                _popup = _abilityPresentersFactory.CreateSelectAbilityPopupPresenter(_mainHeroHolderService.MainHero);
                _popup.Enable();

                _popup.CloseRequest += OnAbilitySelected;

                yield return new WaitUntil(() => PopupIsOpened == false);
            }
        }

        private void OnAbilitySelected(SelectAbilityPopupPresenter popup)
        {
            popup.CloseRequest -= OnAbilitySelected;
            
            _popup.Disable(() =>
            {
                _pauseService.Unpause();
                _popup = null;
            });
        }

        public void Dispose()
        {
            _hereRegisteredDisposable.Dispose();
            _hereUnregisteredDisposable.Dispose();
        }
    }
}