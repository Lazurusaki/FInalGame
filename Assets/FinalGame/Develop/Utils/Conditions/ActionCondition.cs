using System;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Utils.Conditions
{
    public class ActionCondition : ICondition, IDisposable
    {
        private IReadonlyEvent _action;

        private bool _isComplete;

        private readonly IDisposable _disposableAction;

        public ActionCondition(IReadonlyEvent action)
        {
            _action = action;

            _disposableAction = action.Subscribe(OnActionEvent);
        }

        private void OnActionEvent()
        {
            _isComplete = true;
        }

        public bool Evaluate()
        {
            bool temp = _isComplete;
            _isComplete = false;
            return temp;
        }

        public void Dispose()
        {
            _disposableAction.Dispose();
        }
    }
}