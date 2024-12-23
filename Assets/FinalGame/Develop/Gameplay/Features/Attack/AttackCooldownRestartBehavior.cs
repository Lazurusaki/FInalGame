using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class AttackCooldownRestartBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveVariable<float> _attackCooldown;
        private IReadOnlyVariable<float> _attackInterval;
        private IReadonlyEvent _instantAttackEvent;

        private IDisposable _disposableInstantAttackEvent;
        public void OnInit(Entity entity)
        {
            _attackCooldown = entity.GetAttackCooldown();
            _attackInterval = entity.GetAttackInterval();
            _instantAttackEvent = entity.GetInstantAttackEvent();

            _disposableInstantAttackEvent = _instantAttackEvent.Subscribe(OnShoot);
        }

        public void OnDispose()
        {
            _disposableInstantAttackEvent.Dispose();
        }

        private void OnShoot()
            => _attackCooldown.Value = _attackInterval.Value;
    }
}