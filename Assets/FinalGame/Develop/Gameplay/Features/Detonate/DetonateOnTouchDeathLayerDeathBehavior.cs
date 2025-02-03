using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Extensions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Detonate
{
    public class DetonateOnTouchDeathLayerDeathBehavior : IEntityInitialize, IEntityDispose
    {
        private IReadOnlyVariable<float> _radius;
        private IReadOnlyVariable<float> _damage;
        private ReactiveEvent _detonateTrigger;
        private Transform _originTransform;
        private Entity _self;
        
        public void OnInit(Entity entity)
        {
            _radius = entity.GetRadiusAttackRadius();
            _damage = entity.GetRadiusAttackDamage();
            _detonateTrigger = entity.GetRadiusAttackTrigger();
            _originTransform = entity.GetTransform();
            _self = entity;

            _self.GetIsTouchDeathLayer().Changed += OnIsDeathLayerTouched;
        }

        private void OnIsDeathLayerTouched(bool arg1, bool newIsDead)
        {
            if (newIsDead)
            {
                _detonateTrigger.Invoke();
                Detonate();
            }
        }

        private void Detonate()
        {
            Collider[] colliders = Physics.OverlapSphere(_originTransform.position, _radius.Value);

            foreach (var collider in colliders)
            {
                var otherEntity = collider.GetComponentInParent<Entity>();
                
                if (otherEntity is null)
                    continue;
                
                if (otherEntity == _self || otherEntity.GetTeam().Value == _self.GetTeam().Value)
                    continue;
                
                otherEntity.TryTakeDamage(_damage.Value);
            }
        }

        public void OnDispose()
        {
            _self.GetIsTouchDeathLayer().Changed -= OnIsDeathLayerTouched;
        }
    }
}