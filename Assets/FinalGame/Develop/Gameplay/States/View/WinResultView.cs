using System;
using DG.Tweening;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States.View
{
    public class WinResultView: MonoBehaviour
    {
        [SerializeField] private ResultView _main;
        [SerializeField] private RewardView _rewardView;

        public ResultView Main => _main;
        public RewardView RewardView => _rewardView;
        
        private Sequence _animation;
        
        public YieldInstruction ShowMain()
        {
            return _main.ShowMain();
        }
        
        public YieldInstruction ShowButton()
        {
            return _main.ShowButton();
        }
        
        public YieldInstruction ShowReward()
        {
            return _rewardView.Show();
        }
        
        private void OnDestroy()
        {
            _animation.Kill();
        }
    }
}