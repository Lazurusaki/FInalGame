using System;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class TransformEntityRegistrator : MonoEntityRegistrator
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