using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Stats
{
    public class MoveSpeedModifierApplierBehavior : IEntityInitialize, IEntityUpdate
    {
        private ReactiveVariable<float> _moveSpeed;
        private Dictionary<StatTypes, float> _modifiedStats;
        public void OnInit(Entity entity)
        {
            _moveSpeed = entity.GetMoveSpeed();
            _modifiedStats = entity.GetModifiedStats();
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.MoveSpeed];
            
            if (Mathf.Approximately(tempValue, _moveSpeed.Value))
                return;

            tempValue = Mathf.Max(0, tempValue);

            _moveSpeed.Value = tempValue;
        }
    }
}