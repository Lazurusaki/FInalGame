using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Energy
{
    public class GradualRestoreEnergyBehavior : IEntityInitialize, IEntityUpdate
    {
        private const float EnergyRestoreAmountPercent = 0.1f;

        private ReactiveVariable<float> _energy;
        private IReadOnlyVariable<float> _maxEnergy;
        private IReadOnlyVariable<float> _restoreCooldown;
        private ICondition _condition;
        private float _energyRestoreAmount;
        
        private float _time;
        
        public void OnInit(Entity entity)
        {
            _restoreCooldown = entity.GetRestoreEnergyCooldown();
            _condition = entity.GetRestoreEnergyCondition();
            _energy = entity.GetEnergy();
            _maxEnergy = entity.GetMaxEnergy();
            
            _energyRestoreAmount = _maxEnergy.Value * EnergyRestoreAmountPercent;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate() == false)
            {
                _time = _time > 0 ? 0 : _time;
                return;
            }
            
            _time += deltaTime;

            if (_time <= _restoreCooldown.Value) 
                return;
            
            _energy.Value = Mathf.Min(_energy.Value + _energyRestoreAmount, _maxEnergy.Value);
            _time = 0;
            
            Debug.Log(_energy.Value);
        }
    }
}