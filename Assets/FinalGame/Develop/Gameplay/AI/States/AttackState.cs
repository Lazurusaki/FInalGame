using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class AttackState : State, IUpdatableState
    {
        private ReactiveEvent _attackEvent;

        public AttackState(ReactiveEvent attackEvent)
        {
            _attackEvent = attackEvent;
        }

        public override void Enter()
        {
            base.Enter();
            
            _attackEvent?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}