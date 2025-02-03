using System;
using System.Collections;
using FinalGame.Develop.CommonServices.CoroutinePerformer;
using FinalGame.Develop.Gameplay.States.View;
using UnityEngine;
using UnityEngine.UI;

namespace FinalGame.Develop.Gameplay.States.Presenters
{
    public class ResultPresenter
    {
        public event Action Continue;
        
        private readonly ResultView _main;
        private readonly ICoroutinePerformer _coroutinePerformer;

        public ICoroutinePerformer CoroutinePerformer => _coroutinePerformer;
        
        public ResultPresenter(ResultView view, ICoroutinePerformer coroutinePerformer)
        {
            _main = view;
            _coroutinePerformer = coroutinePerformer;

            _main.ContinueClicked += OnContinue;
        }

        private void OnContinue()
        {
            Continue?.Invoke();
        }

        public virtual void Enable()
        {
            CoroutinePerformer.StartPerform(Show());
        }
        
        private IEnumerator Show()
        {
            yield return ShowMain();
            yield return ShowButton();
            _main.EnableSubscription();
        }
        
        public YieldInstruction ShowMain()
        {
            return _main.ShowMain();
        }
        
        public YieldInstruction ShowButton()
        {
            return _main.ShowButton();
        }
    }
}