using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class CollectExperienceToTargetBehavior: IEntityInitialize, IEntityDispose
    {
        private IReadOnlyVariable<Entity> _target;
        private IReadOnlyVariable<bool> _isCollected;
        
        private ReactiveVariable<float> _experience;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();   
            _experience = entity.GetExperience();
            _isCollected = entity.GetIsCollected();

            _isCollected.Changed += OnIsCollectedChanged;
        }

        private void OnIsCollectedChanged(bool arg1, bool isCollected)
        {
            if (isCollected)
                _target.Value.GetExperience().Value += _experience.Value;
        }

        public void OnDispose()
        {
            _isCollected.Changed -= OnIsCollectedChanged;
        }
    }
}