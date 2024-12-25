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
                    && team.Value != _team.Value);
            
            if (damageableTargets.Any() == false)
            {
                selectedTarget = null;
                return false;
            }
            
            selectedTarget = GetNearestTarget(damageableTargets);
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

            return nearestTarget;
        }

        private float GetDistanceTo(Entity target) => (_center.position - target.GetTransform().position).magnitude;
    }
}