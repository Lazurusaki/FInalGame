using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class MaxHealthModifierApplierBehavior : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _maxHealth;
        private ReactiveVariable<float> _currentHealth;
        private Dictionary<StatTypes, float> _modifiedStats;
        public void OnInit(Entity entity)
        {
            _maxHealth = entity.GetMaxHealth();
            _currentHealth = entity.GetHealth();
            _modifiedStats = entity.GetModifiedStats();
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.MaxHealth];

            if (Mathf.Approximately(tempValue, _maxHealth.Value))
                return;

            float previousRatio = _currentHealth.Value / _maxHealth.Value;

            tempValue = Mathf.Max(0, tempValue);
            
            _maxHealth.Value = tempValue;
            _currentHealth.Value = _maxHealth.Value * previousRatio;
        }
    }
}