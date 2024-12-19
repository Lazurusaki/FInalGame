using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Utils.Conditions;
using FinalGame.Develop.Utils.Reactive;

namespace FinalGame.Develop.Gameplay.Features.Death
{
    public class DeathBehavior: IEntityInitialize, IEntityUpdate
    {
        private ICondition _condition;
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _isDeathProcess;
        
        public void OnInit(Entity entity)
        {
            _condition = entity.GetDeathCondition();
            _isDead = entity.GetIsDead();
            _isDeathProcess = entity.GetIsDeathProcess();
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            if (_condition.Evaluate() == false)
                return;
            
            _isDead.Value = true; 
            _isDeathProcess.Value = true;
        }
    }
}