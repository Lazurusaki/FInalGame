using System;
using FinalGame.Develop.Gameplay.Features.Stats;
using UnityEngine;

namespace FinalGame.Resources.Configs.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/StatChangeAbilityConfig", fileName = "StatChangeAbilityConfig")]
    public class StatChangeAbilityConfig : AbilityConfig
    {
        [field : SerializeField] public StatTypes StatType { get; private set; }

        [SerializeField] private StatChangeOperation _operation;
        [SerializeField] private float _value;

        public Func<float, float> GetApplyEffect()
        {
            switch (_operation)
            {
                case StatChangeOperation.Add:
                    return stat => stat += _value;
                
                case StatChangeOperation.Multiply:
                    return stat => stat *= _value;
                
                default:
                    throw new InvalidOperationException("Invalid operation");
            }
        }
        
        private enum StatChangeOperation
        {
            Multiply,
            Add
        }
    }
}