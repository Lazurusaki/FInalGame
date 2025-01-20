using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class AttackIntervalModifierApplierBehavior : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _attackInterval;
        private Dictionary<StatTypes, float> _modifiedStats;
        public void OnInit(Entity entity)
        {
            _attackInterval = entity.GetAttackInterval();
            _modifiedStats = entity.GetModifiedStats();
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.AttackInterval];
            
            if (Mathf.Approximately(tempValue, _attackInterval.Value))
                return;

            tempValue = Mathf.Max(0.3f, tempValue);

            _attackInterval.Value = tempValue;
        }
    }
}