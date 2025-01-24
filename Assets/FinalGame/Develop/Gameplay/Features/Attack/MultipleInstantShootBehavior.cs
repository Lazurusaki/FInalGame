using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class MultipleInstantShootBehavior : IEntityInitialize, IEntityDispose
    {
        private IReadonlyEvent _instantShootEvent;
        private InstantShootingDirectionsArgs _directions;
        
        private IReadOnlyVariable<float> _damage;
        private Transform _shootPoint;
        
        private IDisposable _disposableShootEvent;
        
        private EntityFactory _factory;


        public MultipleInstantShootBehavior(EntityFactory factory)
        {
            _factory = factory;
        }

        public void OnInit(Entity entity)
        {
            _instantShootEvent = entity.GetInstantAttackEvent();
            _damage = entity.GetDamage();
            _shootPoint = entity.GetShootPoint();
            _directions = entity.GetInstantShootingDirections();

            _disposableShootEvent = _instantShootEvent.Subscribe(OnAttack);
        }
        
        public void OnDispose()
        {
            _disposableShootEvent.Dispose();
        }

        private void OnAttack()
        {
            foreach (var arg in _directions.Args)
                Shoot(arg.Angle, arg.ProjectilesCount);
        }

        private void Shoot(int angle, int projectilesCount)
        {
            Vector3 directionForShoot = Quaternion.Euler(new Vector3(0, angle, 0)) * _shootPoint.forward;
            Vector2 perpendicular = Vector2.Perpendicular(new Vector2(directionForShoot.x, directionForShoot.z)).normalized;

            float offsetBetweenProjectiles = 0.6f;

            for (int i = 0; i < projectilesCount; i++)
            {
                Vector2 offset = perpendicular * (-offsetBetweenProjectiles / 2f * (projectilesCount - 1) +
                                                  i * offsetBetweenProjectiles);

                Vector3 position = new Vector3(_shootPoint.position.x + offset.x, _shootPoint.position.y,
                    _shootPoint.position.z + offset.y);

                _factory.CreateArrow(position, directionForShoot, _damage.Value);
            }
        }
    }
}