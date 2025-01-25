using System.Collections;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Death
{
    public class InstantDeathBehavior : IEntityInitialize,IEntityUpdate
    {
        private ICondition _condition;
        private ReactiveVariable<bool> _isDead;
        
        public void OnInit(Entity entity)
        {
            _condition = entity.GetDeathCondition();
            _isDead = entity.GetIsDead();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_condition.Evaluate())
                _isDead.Value = true;
        }
    }
}