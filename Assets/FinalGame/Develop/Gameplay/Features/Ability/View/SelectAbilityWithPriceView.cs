using System;
using DG.Tweening;
using FinalGame.Develop.CommonUI;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.Features.Ability.View
{
    
                            

public class SelectAbilityWithPriceView : MonoBehaviour
    {
        private enum TextColor
        {
            Red,
            Green
        }
        
        public event Action Selected;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _selectable;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _selectImage;
        [SerializeField] private IconWithText _currency;
        
        
        private Sequence _currentAnimation;
        private float _startYOffset = 100;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }
        
        public void Subscribe() => _selectable.onClick.AddListener(OnClicked);

        public void Unsubscribe() => _selectable.onClick.RemoveListener(OnClicked);
        
        public YieldInstruction Show()
        {
            _currentAnimation?.Kill();
                
            _currentAnimation = DOTween.Sequence();

            _currentAnimation
                .Append(_canvasGroup.DOFade(1, 0.4f))
                .Join(_canvasGroup.transform.DOLocalMoveY(0, 0.4f).From(_startYOffset))
                .SetUpdate(true); //ignore time scale

            return _currentAnimation.WaitForCompletion();
        }

        public YieldInstruction Hide()
        {
            _currentAnimation?.Kill();
                
            _currentAnimation = DOTween.Sequence();

            _currentAnimation
                .Append(_canvasGroup.DOFade(0, 0.4f))
                .Append(_canvasGroup.transform.DOLocalMoveY(_startYOffset, 0.4f))
                .SetUpdate(true); //ignore time scale

            return _currentAnimation.WaitForCompletion();
        }
        
        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetCurrencyIcon(Sprite icon) => _currency.SetIcon(icon);

        public void SetCurrencyValue(int value) => _currency.SetText(value.ToString());
            
        public void Select() => _selectImage.gameObject.SetActive(true);

        public void Unselect() => _selectImage.gameObject.SetActive(false);

        public void SetTextColor(Color color) => _currency.SetTextColor(color);
        
        private void OnClicked()
        {
            Selected?.Invoke(); 
        }
    }
}