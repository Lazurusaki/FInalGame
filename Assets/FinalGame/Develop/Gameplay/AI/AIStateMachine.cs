using System;
using System.Collections.Generic;
using FinalGame.Develop.Utils.StateMachine;

namespace FinalGame.Develop.Gameplay.AI
{
    public class AIStateMachine : StateMachine<IUpdatableState>
    {
        public AIStateMachine(List<IDisposable> disposables = null) : base(disposables)
        {
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            CurrentState.State.Update(deltaTime);
        }
    }
}