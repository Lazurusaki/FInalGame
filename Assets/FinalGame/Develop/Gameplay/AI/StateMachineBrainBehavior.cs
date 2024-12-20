using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI
{
    public class StateMachineBrainBehavior : IEntityInitialize, IEntityDispose, IEntityUpdate
    {
        private readonly AIStateMachine _stateMachine;

        public StateMachineBrainBehavior(AIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void OnInit(Entity entity) => _stateMachine.Enter();

        public void OnDispose()
        {
            _stateMachine.Exit();
            
            _stateMachine.Dispose();
        }

        public void OnUpdate(float deltaTime) => _stateMachine.Update(deltaTime);
    }
}