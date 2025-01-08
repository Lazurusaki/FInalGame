using System;
using System.Collections.Generic;
using FinalGame.Develop.Configs.Gameplay.Levels.WaveStage;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Features.Enemy;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.GameModes.Wave
{
    public class WaveGameMode
    {
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

            SetupWave(waveConfig);

            _inProcess.Value = true;
        }

        private void SetupWave(WaveConfig waveConfig)
        {
            foreach (WaveItemConfig waveItemConfig in waveConfig.WaveItems)
                SpawnEnemy(waveItemConfig);
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

        private void SpawnEnemy(WaveItemConfig waveItemConfig)
        {
            Entity spawnedEnemy = _enemyFactory.Create(waveItemConfig.SpawnPosition, waveItemConfig.EnemyConfig);
            EntityToRemoveReason entityToRemoveReason = new EntityToRemoveReason(spawnedEnemy);
            entityToRemoveReason.OnRemoveReasonComplete += OnEnemyRemoved;
            _currentSpawnedEnemies.Add(entityToRemoveReason);
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
    }
}