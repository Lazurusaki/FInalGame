using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class CollectHealthToTargetBehavior : IEntityInitialize, IEntityDispose
    {
        private IReadOnlyVariable<Entity> _target;
        private IReadOnlyVariable<bool> _isCollected;

        private ReactiveVariable<float> _health;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _health = entity.GetHealth();
            _isCollected = entity.GetIsCollected();

            _isCollected.Changed += OnIsCollectedChanged;
        }

        private void OnIsCollectedChanged(bool arg1, bool isCollected)
        {
            if (isCollected == false)
                return;

            var currentHealth = _target.Value.GetHealth();
            var maxHealth = _target.Value.GetMaxHealth().Value;

            if (currentHealth.Value + _health.Value > _target.Value.GetMaxHealth().Value)
                return;

            currentHealth.Value = currentHealth.Value + _health.Value > maxHealth
                ? maxHealth
                : currentHealth.Value + _health.Value;
        }

        public void OnDispose()
        {
            _isCollected.Changed -= OnIsCollectedChanged;
        }
    }
}