using System;
using System.Collections.Generic;
using FinalGame.Develop.Configs.Gameplay.Levels.WaveStage;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Enemy;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FinalGame.Develop.Gameplay.Features.GameModes.Wave
{
    public class WaveGameMode
    {
        private const int MinEnemyPosition = 20;
        private const int MaxEnemyPosition = 30;
        
        private readonly EnemyFactory _enemyFactory;
        
        private ReactiveEvent _ended = new();

        private List<EntityToRemoveReason> _currentSpawnedEnemies = new();
        
        private ReactiveVariable<bool> _inProcess = new();

        public WaveGameMode(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public IReadOnlyVariable<bool> InProcess => _inProcess;

        public IReadonlyEvent Ended => _ended;

        public void Start(WaveConfig waveConfig)
        {
            if (InProcess.Value)
                throw new InvalidOperationException("Is already started");

            SpawnEnemies(waveConfig);

            _inProcess.Value = true;
        }
        
        public void Cleanup()
        {
            foreach (EntityToRemoveReason entityToRemoveReason in _currentSpawnedEnemies)
            {
                entityToRemoveReason.Dispose();
                UnityEngine.Object.Destroy(entityToRemoveReason.Entity.gameObject);
            }

            _currentSpawnedEnemies.Clear();

            _inProcess.Value = false;
        }

        private void SpawnEnemies(WaveConfig waveConfig)
        {
            for (int i = 0; i < waveConfig.EnemiesCount; i++)
            {
                Vector3 randomPosition = GenerateRandomPositionAround(Vector3.zero, MaxEnemyPosition, MinEnemyPosition);
                Entity spawnedEnemy = _enemyFactory.Create(randomPosition, waveConfig.EnemyConfig);
                EntityToRemoveReason entityToRemoveReason = new EntityToRemoveReason(spawnedEnemy);
                entityToRemoveReason.OnRemoveReasonComplete += OnEnemyRemoved;
                _currentSpawnedEnemies.Add(entityToRemoveReason);
            }
        }

        private void OnEnemyRemoved(EntityToRemoveReason entityToRemoveReason)
        {
            entityToRemoveReason.OnRemoveReasonComplete -= OnEnemyRemoved;

            if (_currentSpawnedEnemies.Contains(entityToRemoveReason))
            {
                _currentSpawnedEnemies.Remove(entityToRemoveReason);
                
                if (_currentSpawnedEnemies.Count == 0)
                    _ended?.Invoke();
            }
            else
            {
                throw new InvalidOperationException("EntityToRemoveReason is not exist");
            }
        }

        private class EntityToRemoveReason : IDisposable
        {
            public event Action<EntityToRemoveReason> OnRemoveReasonComplete;
            
            public EntityToRemoveReason(Entity entity)
            {
                Entity = entity;
                Entity.GetIsDead().Changed += OnEntityDied;
            }
            
            public Entity Entity { get; private set; }

            private void OnEntityDied(bool arg1, bool isDead)
            {
                if (!isDead) 
                    return;
                
                Entity.GetIsDead().Changed -= OnEntityDied;
                OnRemoveReasonComplete?.Invoke(this);
            }

            public void Dispose()
            {
                Entity.GetIsDead().Changed -= OnEntityDied;
            }
        }

        private Vector3 GenerateRandomPositionAround(Vector3 origin, float maxRange, float minRange = 0)
        {
            if (minRange > maxRange)
                throw new ArgumentException("MinRange can't be greater than MaxRange");
            
            float angle = Random.Range(0f, Mathf.PI * 2);
            
            float radius = Random.Range(minRange, maxRange);
            
            float offsetX = Mathf.Cos(angle) * radius;
            float offsetZ = Mathf.Sin(angle) * radius;
            
            return new Vector3(origin.x + offsetX, origin.y, origin.z + offsetZ);
        }
    }
}