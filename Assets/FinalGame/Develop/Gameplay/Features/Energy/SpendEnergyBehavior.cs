using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Energy
{
    public class SpendEnergyBehavior : IEntityInitialize, IEntityDispose
    {
        private ReactiveEvent<float> _spendEnergyEvent;
        private ReactiveVariable<float> _energy;
        
        private IDisposable _disposableSpendEnergyEvent;

        public void OnInit(Entity entity)
        {
            _spendEnergyEvent = entity.GetSpendEnergyEvent();
            _energy = entity.GetEnergy();

            _disposableSpendEnergyEvent = _spendEnergyEvent.Subscribe(OnSpendEnergy);
        }

        public bool HasEnoughEnergy(float energy) => _energy.Value >= energy;

        public void OnDispose()
        {
            _disposableSpendEnergyEvent.Dispose();
        }
        
        private void OnSpendEnergy(float energy)
        {
            if (energy< 0)
                throw new ArgumentException("Negative Energy is not allowed");
            
            if (HasEnoughEnergy(energy) == false)
            {
                Debug.Log("Not Enough energy");
                return;
            }

            _energy.Value = Math.Max(_energy.Value - energy, 0);
        }
    }
}