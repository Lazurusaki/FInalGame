using UnityEngine;

namespace FinalGame.Develop.Gameplay.Entities.CommonRegistrators
{
    public class EntityViewRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _rootView;
        
        public override void Register(Entity entity)
        {
            foreach (var entityView in _rootView.GetComponentsInChildren<EntityView>())
                entityView.SubscribeTo(entity);
        }
    }
}