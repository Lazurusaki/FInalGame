using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class AttackCooldownRestartOnMoveBehavior: IEntityInitialize, IEntityDispose
    {
        private IReadOnlyVariable<bool> _isMoving;
        private ReactiveVariable<float> _attackCooldown;


        private IDisposable _disposableInstantAttackEvent;
        public void OnInit(Entity entity)
        {
            _isMoving = entity.GetIsMoving();
            _attackCooldown = entity.GetAttackCooldown();
            
            _isMoving.Changed += OnMovingChanged;
        }
        
        public void OnDispose()
        {
            _isMoving.Changed -= OnMovingChanged;
        }

        private void OnMovingChanged(bool arg1, bool isMoving)
        {
            if (isMoving)
                _attackCooldown.Value = 0;
        }
    }
}