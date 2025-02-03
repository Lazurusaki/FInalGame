using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.States.View
{
    public class ButtonView : MonoBehaviour
    {
        public event Action OnClicked;
        
        [SerializeField] private CanvasGroup _main;
        [SerializeField] private Button _button;

        public CanvasGroup Main => _main;

        private Sequence _animation;

        private void Awake()
        {
            _main.alpha = 0f;
        }

        public YieldInstruction Show()
        {
            _animation?.Kill();

            _animation = DOTween.Sequence();

            _animation
                .Append(_main.DOFade(1, 0.05f))
                .Join(_main.transform.DOScale(0.5f, 0.5f).From())
                .SetUpdate(true); //ignore time scale

            return _animation.WaitForCompletion();
        }

        public void EnableSubscription() => _button.onClick.AddListener(OnClick);

        private void OnClick()
        {
            OnClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _animation.Kill();
        }
    }
}