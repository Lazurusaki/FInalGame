using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Death
{
    public class SelfDestroyBehavior: IEntityInitialize, IEntityUpdate
    {
        private ICondition _selfDestroyCondition;
        private Transform _transform;
        
        public void OnInit(Entity entity)
        {
            _selfDestroyCondition = entity.GetSelfDestroyCondition();
            _transform = entity.GetTransform();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_selfDestroyCondition.Evaluate())
                Object.Destroy(_transform.gameObject);
        }
    }
}