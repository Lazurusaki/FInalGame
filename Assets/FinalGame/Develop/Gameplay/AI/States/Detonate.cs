using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.States
{
    public class Detonate: State, IUpdatableState
    {
        private readonly EntitiesBuffer _entitiesBuffer;
        
        private readonly Entity _self;
        
        public Detonate(Entity self)
        {
            _self = self;
        }
        
        public override void Enter()
        {
            base.Enter();

            if (_self.TryGetRadiusAttackTrigger(out var detonateTrigger))
                detonateTrigger.Invoke();
        }
        
        public void Update(float deltaTime)
        {
            
        }
    }
}