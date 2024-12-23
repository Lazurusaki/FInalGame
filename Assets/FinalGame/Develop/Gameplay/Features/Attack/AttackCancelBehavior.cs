using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class AttackCancelBehavior: IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<bool> _isAttackProcess;
        
        private ICondition _condition;

        public void OnInit(Entity entity)
        {
            _condition = entity.GetAttackCancelCondition();
            _isAttackProcess = entity.GetIsAttackProcess();

        }

        public void OnUpdate(float deltaTime)
        {
            if (_isAttackProcess.Value == false)
                return;
            
            if (_condition.Evaluate())
                _isAttackProcess.Value = false;
        }
    }
}