using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.Features.Aim
{
    public class AimPresenter
    {
        private readonly AimView _view;
        private readonly Transform _aim;
        
        public AimPresenter(Entity entity, AimView view)
        {
            _view = view;
            _aim = entity.GetAimTransform();
        }

        public void Attack()
        {
            _view.Interact();
        }

        public void Enable()
        {
            _view.Show();
            Cursor.visible = false;
        }
        
        public void Disable()
        {
            _view.Hide();
            Cursor.visible = true;
        }

        public void Update()
        {
            _view.transform.position = _aim.position;
        }
    }
}