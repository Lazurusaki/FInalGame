using System.Collections.Generic;
using System.Linq;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.AI.Sensors
{
    public class NearestDamageableTargetSelector : ITargetSelector
    {
        private readonly Transform _center;
        private readonly IReadOnlyVariable<int> _team;

        private Entity _target;

        public NearestDamageableTargetSelector(Transform center, IReadOnlyVariable<int> team)
        {
            _center = center;
            _team = team;
        }

        public bool TrySelectTarget(IEnumerable<Entity> targets, out Entity selectedTarget)
        {
            IEnumerable<Entity> damageableTargets = targets
                .Where(target =>
                    EntityExtensionsGenerated.TryGetTakeDamageRequest(target, out var request)
                    && EntityExtensionsGenerated.TryGetIsDead(target, out var isDead)
                    && isDead.Value == false
                    && EntityExtensionsGenerated.TryGetTeam(target, out var team)
                    && team.Value != _team.Value
                    && target.TryGetIsMainHero(out var value));
            
            if (damageableTargets.Any() == false)
            {
                selectedTarget = null;
                _target = null;
                return false;
            }
            
            selectedTarget = GetNearestTarget(damageableTargets);
            _target = selectedTarget;
            return true;
        }

        public bool TryGetDistanceToTarget(out Entity target, out float distance)
        {
            distance = 0;
            target = null;
            
            if (_target is null)
                return false;
            
            target = _target;
            distance = GetDistanceTo(target);
            
            return true;
        }
        
        private Entity GetNearestTarget(IEnumerable<Entity> targets)
        {
            var nearestTarget= targets.First();
            var minDistance = GetDistanceTo(nearestTarget);

            foreach (var target in targets)
            {
                var distance = GetDistanceTo(target);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }

            _target = nearestTarget;

            return nearestTarget;
        }

        private float GetDistanceTo(Entity target) => (_center.position - target.GetTransform().position).magnitude;
    }
}