using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Energy
{
    public class SpendEnergyFilterBehavior: IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _spendEnergyEvent;
        private ReactiveEvent<float> _spendEnergyRequest;

        private IReadOnlyVariable<float> _energy;
        
        private ICondition _spendEnergyCondition;

        private IDisposable _disposableSpendEnergyRequest;
        
        public void OnInit(Entity entity)
        {
            _spendEnergyEvent = entity.GetSpendEnergyEvent();
            _spendEnergyRequest = entity.GetSpendEnergyRequest();
            _spendEnergyCondition = entity.GetTakeDamageCondition();
            
            _energy = entity.GetEnergy();

            _disposableSpendEnergyRequest = _spendEnergyRequest.Subscribe(OnSpendEnergyRequest);
        }

        private void OnSpendEnergyRequest(float energy)
        {
            if (energy < 0)
                throw new ArgumentException("Negative Damage is not allowed");

            if (energy > _energy.Value)
            {
                Debug.Log("Not Enough energy");
                return;
            }

            if (_spendEnergyCondition.Evaluate())
                _spendEnergyEvent.Invoke(energy);
        }
        
        public void OnDispose()
        {
            _disposableSpendEnergyRequest.Dispose();
        }
    }
}