using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class DamageModifierApplierBehavior : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _damage;
        private Dictionary<StatTypes, float> _modifiedStats;
        public void OnInit(Entity entity)
        {
            _damage = entity.GetDamage();
            _modifiedStats = entity.GetModifiedStats();
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.Damage];
            
            if (Mathf.Approximately(tempValue, _damage.Value))
                return;

            tempValue = Mathf.Max(0, tempValue);

            _damage.Value = tempValue;
        }
    }
}