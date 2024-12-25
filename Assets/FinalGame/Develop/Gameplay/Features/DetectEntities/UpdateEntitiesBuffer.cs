using System.Collections.Generic;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;

namespace FinalGame.Develop.Gameplay.Features.DetectEntities
{
    public class UpdateEntitiesBuffer : IEntityInitialize, IEntityDispose
    {
        private readonly EntitiesBuffer _entitiesBuffer;

        private List<Entity> _detectedEntities = new();

        public UpdateEntitiesBuffer(EntitiesBuffer entitiesBuffer)
        {
            _entitiesBuffer = entitiesBuffer;

            _entitiesBuffer.Added += OnEntityAdded;
            _entitiesBuffer.Removed += OnEntityRemoved;
            _entitiesBuffer.Cleared += OnCleared;
        }

        public void OnInit(Entity entity)
        {
            _detectedEntities = entity.GetDetectedEntitiesBuffer();
        }

        public void OnDispose()
        {
            _entitiesBuffer.Added -= OnEntityAdded;
            _entitiesBuffer.Removed -= OnEntityRemoved;
            _entitiesBuffer.Cleared -= OnCleared;
        }
        
        private void OnEntityAdded(Entity entity)
        {
            _detectedEntities.Add(entity);
        }
        
        private void OnEntityRemoved(Entity entity)
        {
            _detectedEntities.Remove(entity);
        }
        
        private void OnCleared()
        {
            _detectedEntities.Clear();
        }
    }
}