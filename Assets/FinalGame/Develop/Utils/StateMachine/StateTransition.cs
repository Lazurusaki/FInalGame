using FinalGame.Develop.Utils.Conditions;

namespace FinalGame.Develop.Utils.StateMachine
{
    public class StateTransition<TState> where TState : IState
    {
        public StateTransition(StateNode<TState> toState, ICondition condition)
        {
            ToState = toState;
            Condition = condition;
        }

        public StateNode<TState> ToState { get; }
        public ICondition Condition { get; }
    }
}