using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Gameplay.Entities.Behaviors;
using FinalGame.Develop.Gameplay.Features.Input;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Aim
{
    public class MouseAimBehavior : IEntityInitialize, IEntityDispose, IEntityUpdate
    {
        private readonly LayerMask AimLayer = 1 << 7;
        
        private Transform _aimTransform;
        private IInputService _inputService;
        
        public MouseAimBehavior(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void OnInit(Entity entity)
        {
            _aimTransform = entity.GetAimTransform();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputService.IsEnabled)
            {
                Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 1000, AimLayer))
                    _aimTransform.position = hit.point;
            }
        }

        public void OnDispose()
        {
            _aimTransform.position = Vector3.zero;
        }
    }
}