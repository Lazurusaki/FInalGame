using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Utils.Reactive;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Attack
{
    public class MouseAttackBehavior : IEntityInitialize, IEntityUpdate
    {
        private Transform _aimTransform;
        private IInputService _inputService;

        private ReactiveEvent _instantAttackTrigger;

        public MouseAttackBehavior(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnInit(Entity entity)
        {
            _instantAttackTrigger = entity.GetInstantAttackTrigger();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputService.IsEnabled)
                if (_inputService.Fire)
                {
                    _instantAttackTrigger?.Invoke();
                }
        }
    }
}