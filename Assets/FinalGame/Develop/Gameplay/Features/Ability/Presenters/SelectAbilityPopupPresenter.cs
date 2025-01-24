using System;
using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Ability.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FinalGame.Develop.Gameplay.Features.Ability.Presenters
{
    public class SelectAbilityPopupPresenter
    {
        public event Action<SelectAbilityPopupPresenter> CloseRequest;
        
        private const string Title = "LEVEL {0} IN THIS ADVENTURE";
        private const string SelectAbilityText = "Select ability";

        private readonly SelectAbilityPopupView _view;
        private SelectAbilityListPresenter _selectAbilityListPresenter;

        private readonly Entity _entity;
        private readonly AbilityPresentersFactory _presentersFactory;

        private readonly ICoroutinePerformer _coroutinePerformer;

        public SelectAbilityPopupPresenter(
            SelectAbilityPopupView view, 
            Entity entity, 
            ICoroutinePerformer coroutinePerformer, 
            AbilityPresentersFactory presentersFactory)
        {
            _view = view;
            _entity = entity;
            _coroutinePerformer = coroutinePerformer;
            _presentersFactory = presentersFactory;
        }

        public void Enable()
        {
            _view.SetTitle(string.Format(Title,2));
            _view.SetAdditionalText(SelectAbilityText);

            _selectAbilityListPresenter =
                _presentersFactory.CreateSelectAbilityListPresenter(_view.AbilityListView, _entity);
            
            _selectAbilityListPresenter.Enable();

            _selectAbilityListPresenter.ProvideComplete += OnProvideComplete;

            _coroutinePerformer.StartPerform(Show());
        }

        public void Disable(Action callback)
        {
            _selectAbilityListPresenter.ProvideComplete -= OnProvideComplete;

            _coroutinePerformer.StartPerform(Hide(callback));
        }

        private IEnumerator Hide(Action callback)
        {
            _selectAbilityListPresenter.Disable();
            yield return _view.Hide();
            
            _selectAbilityListPresenter.Disable();
            
            Object.Destroy(_view.gameObject);
            
            callback?.Invoke();
        }

        private IEnumerator Show()
        {
            yield return _view.Show();
            
            _selectAbilityListPresenter.EnableSubscription();
        }

        private void OnProvideComplete() => CloseRequest?.Invoke(this);
        
        
    }
}