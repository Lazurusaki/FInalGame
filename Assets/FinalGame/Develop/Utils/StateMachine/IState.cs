using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Utils.StateMachine
{
    public interface IState
    {
        IReadonlyEvent Entered { get; }
        IReadonlyEvent Exited { get; }

        void Enter();
        void Exit();
    }
}