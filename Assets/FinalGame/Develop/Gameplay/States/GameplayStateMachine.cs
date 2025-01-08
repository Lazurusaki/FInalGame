using System;
using System.Collections.Generic;
using FinalGame.Develop.Utils.StateMachine;

namespace FinalGame.Develop.Gameplay.States
{
    public class GameplayStateMachine : StateMachine<IUpdatableState>
    {
        public GameplayStateMachine(List<IDisposable> disposables = null) : base(disposables)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            CurrentState.State.Update(deltaTime);
        }
    }
}