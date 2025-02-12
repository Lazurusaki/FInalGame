using System;
using System.Collections.Generic;
using DG.Tweening;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FinalGame.Develop.Gameplay.Features.Loot
{
    public class LootPickupService : IDisposable
    {
        private ReactiveEvent _allCollected = new();
        private List<Entity> _loot = new();
        private EntitiesBuffer _entitiesBuffer;
        private bool _isActivated;

        public LootPickupService(EntitiesBuffer entitiesBuffer)
        {
            _entitiesBuffer = entitiesBuffer;

            _entitiesBuffer.Added += OnEntityAdded;
            _entitiesBuffer.Removed += OnEntityRemoved;
        }

        public IReadonlyEvent AllCollected => _allCollected;
        public bool IsActivated => _isActivated;
        
        private void OnEntityAdded(Entity entity)
        {
            if (entity.TryGetIsPickupable(out var isPickupable) && isPickupable.Value)
            {
                _loot.Add(entity);

                Transform lootTransform = entity.GetTransform();

                Vector2 randomOffset = UnityEngine.Random.insideUnitCircle;
                Vector3 offset = new Vector3(randomOffset.x, 0, randomOffset.y);
                Vector3 endJumpPosition = lootTransform.position + offset;

                lootTransform
                    .DOJump(endJumpPosition, 2, 1, Random.Range(1f,1.5f))
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() => entity.GetIsSpawningProcess().Value = false)
                    .Play();
            }
        }
        
        private void OnEntityRemoved(Entity entity)
        {
            _loot.Remove(entity);

            if (_loot.Count == 0)
            {
                _allCollected?.Invoke();
            }
        }

        public void PickupTo(Entity entity)
        {
            if (_isActivated)
                throw new InvalidOperationException();

            _isActivated = true;

            if (_loot.Count == 0)
            {
                _allCollected?.Invoke();
                return;
            }
            
            foreach (Entity loot in _loot)
            {
                loot.GetTarget().Value = entity;
                loot.GetIsPickupProcess().Value = true;
            }
        }

        public void Reset() => _isActivated = false;
        
        public void Dispose()
        {
            _entitiesBuffer.Added -= OnEntityAdded;
            _entitiesBuffer.Removed -= OnEntityRemoved;
        }
    }
}