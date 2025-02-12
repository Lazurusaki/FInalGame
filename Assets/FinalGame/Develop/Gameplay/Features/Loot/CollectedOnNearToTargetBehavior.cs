using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class CollectedOnNearToTargetBehavior : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<Entity> _target;
        private Transform _transform;
        private ReactiveVariable<bool> _isCollected;

        public void OnInit(Entity entity)
        {
            _target = entity.GetTarget();
            _transform = entity.GetTransform();
            _isCollected = entity.GetIsCollected();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isCollected.Value || _target.Value is null)
                return;
            
            
            if ((_target.Value.GetTransform().position - _transform.position).magnitude < 0.1f)
                _isCollected.Value = true;
        }
    }
}