using System;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class TransformRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _transform;

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
        }

        public override void Register(Entity entity)
        {
            entity.AddTransform(_transform);
        }
    }
}