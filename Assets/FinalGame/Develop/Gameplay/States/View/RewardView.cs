using DG.Tweening;
using TMPro;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States.View
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _main;
        [SerializeField] private TMP_Text _rewardValue;
        
        private Sequence _animation;
        
        private void Awake()
        {
            _main.alpha = 0f;
        }
        
        public void SetRewardValue(int value)
        {
            _rewardValue.text = value.ToString();
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
        
        private void OnDestroy()
        {
            _animation.Kill();
        }
    }
}