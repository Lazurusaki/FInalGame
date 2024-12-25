using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class AttackBehavior: IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _attackTrigger;
        private ReactiveVariable<bool> _isAttackProcess;

        private ICondition _condition;

        private IDisposable _disposableAttackTriggerEvent;
        
        public void OnInit(Entity entity)
        {
            _attackTrigger = entity.GetAttackTrigger();
            _condition = entity.GetAttackCondition();
            _isAttackProcess = entity.GetIsAttackProcess();

            _disposableAttackTriggerEvent = _attackTrigger.Subscribe(OnAttackTrigger);
        }

        public void OnDispose()
        {
            _disposableAttackTriggerEvent.Dispose();
        }
        
        private void OnAttackTrigger()
        {
            if (_condition.Evaluate())
                _isAttackProcess.Value = true;
        }
    }
}