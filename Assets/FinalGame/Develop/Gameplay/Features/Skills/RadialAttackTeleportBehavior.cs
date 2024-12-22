using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Skills
{
    public class RadialAttackTeleportBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent _radialAttackTeleportTrigger;

        private ReactiveEvent _teleportTrigger;
        private ReactiveEvent _radialAttackTrigger;

        private ICondition _teleportCondition;
        private ICondition _radialAttackCondition;
        
        private IDisposable _radialAttackTeleportEvent;

        public void OnInit(Entity entity)
        {
            _radialAttackTeleportTrigger = entity.GetRadialAttackTeleportTrigger();
            _teleportTrigger = entity.GetTeleportTrigger();
            _radialAttackTrigger = entity.GetRadiusAttackTrigger();

            _teleportCondition = entity.GetTeleportCondition();
            _radialAttackCondition = entity.GetRadiusAttackCondition();

            _radialAttackTeleportEvent = _radialAttackTeleportTrigger.Subscribe(OnRadialAttackTeleport);
        }
        
        private void OnRadialAttackTeleport()
        {
            if (_teleportCondition.Evaluate() && _radialAttackCondition.Evaluate())
            {
                _teleportTrigger.Invoke();
                _radialAttackTrigger.Invoke();
            }
        }
        
        public void OnDispose()
        {
            _radialAttackTeleportEvent.Dispose();
        }
    }
}