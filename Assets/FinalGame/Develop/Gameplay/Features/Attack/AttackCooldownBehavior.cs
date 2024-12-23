using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class AttackCooldownBehavior : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _attackCooldown;
        public void OnInit(Entity entity)
        {
            _attackCooldown = entity.GetAttackCooldown();
        }

        public void OnUpdate(float deltaTime)
        {
            if (InCooldown())
                _attackCooldown.Value -= deltaTime;
        }

        private bool InCooldown() => _attackCooldown.Value > 0;
    }
}