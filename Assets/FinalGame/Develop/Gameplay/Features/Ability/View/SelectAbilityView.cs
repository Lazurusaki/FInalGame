using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.Features.Ability.View
{
    public class SelectAbilityView : MonoBehaviour
    {
        public event Action Selected;

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Button _button;

        [SerializeField] private  AbilityIcon _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private TMP_Text _textOnTablet;
        
        [SerializeField] private Image _selectImage;

        private Sequence _currentAnimation;
        private float _startYOffset = 100;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }

        public AbilityIcon Icon => _icon;

        public void Subscribe() => _button.onClick.AddListener(OnClicked);

        public void Unsubscribe() => _button.onClick.RemoveListener(OnClicked);

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

        public void SetName(string name) => _name.text = name;

        public void SetDescription(string description) => _textOnTablet.text = description;

        public void SetTabletText(string tabletText) => _textOnTablet.text = tabletText;

        public void Select() => _selectImage.gameObject.SetActive(true);

        public void Unselect() => _selectImage.gameObject.SetActive(false);

        private void OnClicked()
        {
            Selected?.Invoke(); 
        }

        private void OnDestroy()
        {
            _currentAnimation.Kill();
        }
    }
}
