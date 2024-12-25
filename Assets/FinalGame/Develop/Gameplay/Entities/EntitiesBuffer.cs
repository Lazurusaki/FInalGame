using System;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Entities
{
    public class EntitiesBuffer : ObservableList<Entity>, IDisposable
    {
        public override void Add(Entity entity)
        {
            base.Add(entity);

            entity.Disposed += OnEntityDisposed;
        }

        public void Dispose()
        {
            foreach (var entity in Elements)
                entity.Disposed -= OnEntityDisposed;

            Clear();;
        }
        
        private void OnEntityDisposed(Entity entity)
        {
            entity.Disposed -= OnEntityDisposed;
            Remove(entity);
        }
    }
}