using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class ShootPointRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _shootPoint;
        
        public override void Register(Entity entity)
        {
            entity.AddShootPoint(_shootPoint);
        }
    }
}