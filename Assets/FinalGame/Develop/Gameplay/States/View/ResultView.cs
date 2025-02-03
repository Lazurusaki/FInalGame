using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.States.View
{
    public class ResultView : MonoBehaviour
    {
        public event Action ContinueClicked;
        
        [SerializeField] private CanvasGroup _main;
        [SerializeField] private ButtonView _buttonView;
        
        private Sequence _animation;
        
        private void Awake()
        {
            _main.alpha = 0f;
            _buttonView.Main.alpha = 0f;
        }

        public void EnableSubscription()
        {
            _buttonView.EnableSubscription();
            _buttonView.OnClicked += OnContinue;
        }

        private void OnContinue()
        {
            ContinueClicked?.Invoke();
        }

        public YieldInstruction ShowMain()
        {
            _animation?.Kill();
                
            _animation = DOTween.Sequence();
            
            _animation
                .Append(_main.DOFade(1, 0.05f))
                .Join(_main.transform.DOScale(0.5f, 0.5f).From())
                .SetUpdate(true); //ignore time scale
            
             return _animation.WaitForCompletion();
        }
        
        public YieldInstruction ShowButton()
        {
            return _buttonView.Show();
        }
        
        private void OnDestroy()
        {
            _animation.Kill();
        }
    }
}