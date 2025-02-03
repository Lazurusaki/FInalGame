using DG.Tweening;
using UnityEngine;

namespace FinalGame.Develop.CommonUI
{
    public class CanvasBlink : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Sequence _currentAnimation;
        
        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }
        
        public void Activate()
        {
            _currentAnimation?.Kill();
                
            _currentAnimation = DOTween.Sequence();
            
             _currentAnimation
                 .Append(_canvasGroup.DOFade(1, 0.01f))
                 .Append(_canvasGroup.DOFade(0, 0.5f))
                 .SetUpdate(true); //ignore time scale

            // return _currentAnimation.WaitForCompletion();
        }
        
        private void OnDestroy()
        {
            _currentAnimation.Kill();
        }
    }
}