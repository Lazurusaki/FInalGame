using System;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class InstantShootBehavior : IEntityInitialize,IEntityDispose
    {
        private IReadonlyEvent _instantShootEvent;
        
        private readonly EntityFactory _factory;

        private IReadOnlyVariable<float> _damage;
        private Transform _shootPoint;
        private Entity _owner;
        
        private IDisposable _disposableShootEvent;

        public InstantShootBehavior(EntityFactory factory)
        {
            _factory = factory;
        }

        public void OnInit(Entity entity)
        {
            _instantShootEvent = entity.GetInstantAttackEvent();
            _damage = entity.GetDamage();
            _shootPoint = entity.GetShootPoint();
            _owner = entity;
            
            _disposableShootEvent = _instantShootEvent.Subscribe(OnShoot);
        }

        public void OnDispose()
        {
            _disposableShootEvent.Dispose();
        }
        
        private void OnShoot()
        {
            _factory.CreateArrow(_shootPoint.position, _shootPoint.forward, _damage.Value, _owner);
        }
    }
}