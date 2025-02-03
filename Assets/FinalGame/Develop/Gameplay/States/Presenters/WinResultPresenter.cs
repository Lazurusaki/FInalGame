using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.Configs.Gameplay.Levels;
using FinalGame.Develop.Gameplay.States.View;
using TMPro.EditorUtilities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States.Presenters
{
    public class WinResultPresenter : ResultPresenter
    {
        private readonly RewardView _rewardView;
        private LevelConfig _levelConfig;
        private ResultView _main;

        private Coroutine _coroutine;
        
        public WinResultPresenter(ResultView view, 
            RewardView rewardView, 
            LevelConfig levelConfig, 
            ICoroutinePerformer coroutinePerformer) : base(view , coroutinePerformer)
        {
            _levelConfig = levelConfig;
            _rewardView = rewardView;
            _main = view;
        }

        public override void Enable()
        {
            _rewardView.SetRewardValue(_levelConfig.Reward.Value);
            CoroutinePerformer.StartPerform(Show());
        }
        
        private IEnumerator Show()
        {
            yield return ShowMain();
            yield return _rewardView.Show();
            yield return ShowButton();
            _main.EnableSubscription();
        }
    }
}