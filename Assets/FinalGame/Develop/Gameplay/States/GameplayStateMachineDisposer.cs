using System;

namespace FinalGame.Develop.Gameplay.States
{
    public class GameplayStateMachineDisposer : IDisposable
    {
        private GameplayStateMachine _gameplayStateMachine;

        public void Set(GameplayStateMachine gameplayStateMachine)
            => _gameplayStateMachine = gameplayStateMachine;
        
        public void Dispose()
        {
            _gameplayStateMachine.Exit();
            _gameplayStateMachine.Dispose();
        }
    }
}