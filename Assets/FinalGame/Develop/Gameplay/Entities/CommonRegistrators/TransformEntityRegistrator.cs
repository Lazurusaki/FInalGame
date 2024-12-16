using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class TransformEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _transform;
        public override void Register(Entity entity)
        {
            entity.AddTransform(_transform);
        }
    }
}