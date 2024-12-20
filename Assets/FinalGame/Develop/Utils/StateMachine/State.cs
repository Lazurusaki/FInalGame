using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Utils.StateMachine
{
    public abstract class State : IState
    {
        private ReactiveEvent _entered = new ReactiveEvent();
        private ReactiveEvent _exited = new ReactiveEvent();

        public IReadonlyEvent Entered => _entered;
        public IReadonlyEvent Exited => _exited;

        public virtual void Enter() => _entered?.Invoke();

        public virtual void Exit() => _exited?.Invoke();
    }
}