using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.Features.Ability.View
{
    public class SelectAbilityWithPriceListView : MonoBehaviour
    {
        public event Action Selected;

        [SerializeField] private Transform _parent;
        [SerializeField] private SelectAbilityWithPriceView _selectAbilityWithPriceViewPrefab;
        [SerializeField] private Button _selectButton;
        
        
        private List<SelectAbilityWithPriceView> _abilityViewElements = new();

        private Coroutine _animation;
    
        
        private void Awake()
        {
            _selectButton.interactable = false;
        }
        
        public void Subscribe()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }
        
        public void Unsubscribe()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }
         
        public void Select(SelectAbilityWithPriceView selectAbilityView)
        {
            foreach (SelectAbilityWithPriceView view in _abilityViewElements)
                view.Unselect();
            
            selectAbilityView.Select();
            
            _selectButton.interactable = true;
        }

        public SelectAbilityWithPriceView SpawnItem()
        {
            SelectAbilityWithPriceView selectAbilityView = Instantiate(_selectAbilityWithPriceViewPrefab, _parent);
            _abilityViewElements.Add(selectAbilityView);
            return selectAbilityView;
        }

        public void Remove(SelectAbilityWithPriceView abilityView)
        {
            _abilityViewElements.Remove(abilityView);
            Destroy(abilityView.gameObject);
        }

        public YieldInstruction Show()
        {
            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(ShowElements());

            return _animation;
        }


        public YieldInstruction Hide()
        {
            if (_animation != null)
                StopCoroutine(_animation);
            
            _selectButton.interactable = false;
            _animation = StartCoroutine(HideElements());
            
            return _animation;
        }

        private IEnumerator ShowElements()
        {
            YieldInstruction[] showAnimations = new YieldInstruction[_abilityViewElements.Count];
            
            for (int i = 0; i < _abilityViewElements.Count; i++)
            {
                showAnimations[i] = _abilityViewElements[i].Show();
                yield return new WaitForSecondsRealtime(0.25f);
            }

            foreach (YieldInstruction animation in showAnimations)
                yield return animation;
        }
        
        private IEnumerator HideElements()
        {
            YieldInstruction[] showAnimations = new YieldInstruction[_abilityViewElements.Count];

            for (int i = 0; i < _abilityViewElements.Count; i++)
            {
                showAnimations[i] = _abilityViewElements[i].Hide();
                yield return new WaitForSecondsRealtime(0.25f);
            }

            foreach (YieldInstruction animation in showAnimations)
                yield return animation;
        }

        private void OnSelectButtonClicked() => Selected?.Invoke();

        
        public void ResetSelection()
        {
            foreach (SelectAbilityWithPriceView view in _abilityViewElements)
                view.Unselect();
            
            _selectButton.interactable = false;
        }
    }
}