using System;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class AimTransformRegisterer : MonoEntityRegistrator
    {
        [SerializeField] private Transform _transform;
        
        public override void Register(Entity entity)
        {
            if (_transform is null)
                throw new NullReferenceException("Aim transform is not set");
            
            entity.AddAimTransform(_transform);
        }
    }
}