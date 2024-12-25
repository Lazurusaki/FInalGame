using System;
using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Utils.Conditions;
using UnityEngine;

namespace FinalGame.Develop.Utils.StateMachine
{
    public abstract class StateMachine<TState>: State, IUpdatableState, IDisposable where TState : class, IState
    {
        private readonly List<StateNode<TState>> _states = new();
        private readonly List<IDisposable> _disposables;
        
        private StateNode<TState> _currentState;

        private bool _isRunning;
        
        protected StateMachine(List<IDisposable> disposables = null)
        {
            if (disposables == null)
            {
                _disposables = new List<IDisposable>();
                return;
            }

            _disposables = new List<IDisposable>(disposables);
        }

        public void AddState(TState state) => _states.Add(new StateNode<TState>(state));

        public void AddTransition(TState fromState, TState toState, ICondition condition)
        {
            var from = _states.First(stateNode => stateNode.State == fromState);
            var to = _states.First(stateNode => stateNode.State == toState);
            
            from.AddTransition(new StateTransition<TState>(to, condition));
        }

        protected StateNode<TState> CurrentState => _currentState;

        public override void Enter()
        {
            base.Enter();
            
            if (_isRunning) 
                return;
            
            if (_currentState is null)
                SwitchState(_states[0]);

            _isRunning = true;
        }
        
        public override void Exit()
        {
            base.Exit();
            
            _currentState?.State.Exit();
            _isRunning = false;
        }
 
        public virtual void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;
            
            StateTransition<TState> stateTransition = _currentState.Transitions
                .FirstOrDefault(transition => transition.Condition.Evaluate());

            if (stateTransition is not null)
                SwitchState(stateTransition.ToState);
        }
        
        private void SwitchState(StateNode<TState> stateNode)
        {
            _currentState?.State.Exit();
            _currentState = stateNode;
            _currentState.State.Enter();
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}