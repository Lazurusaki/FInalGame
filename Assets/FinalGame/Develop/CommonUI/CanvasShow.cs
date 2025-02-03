using DG.Tweening;
using UnityEngine;

namespace FinalGame.Develop.CommonUI
{
    public class CanvasShow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Sequence _currentAnimation;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }
        
        public void Show()
        {
            _currentAnimation?.Kill();
                
            _currentAnimation = DOTween.Sequence();
            
            _currentAnimation
                .Append(_canvasGroup.DOFade(1, 0.05f))
                .Join(_canvasGroup.transform.DOScale(1.2f, 0.5f))
                .SetUpdate(true); //ignore time scale

            // return _currentAnimation.WaitForCompletion();
        }
        
        private void OnDestroy()
        {
            _currentAnimation.Kill();
        }
    }
}