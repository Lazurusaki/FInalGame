using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities
{
    public abstract class EntityView : MonoBehaviour
    {
        public void SubscribeTo(Entity entity)
        {
            entity.Initialized += OnEntityInitialized;
            entity.Disposed += OnEntityDisposed;
        }

        protected abstract void OnEntityInitialized(Entity entity);

        protected virtual void OnEntityDisposed(Entity entity)
        {
            entity.Initialized -= OnEntityInitialized;
            entity.Disposed -= OnEntityDisposed;
        }
    }
}