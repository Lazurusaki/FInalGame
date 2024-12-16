using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _ghost;

        public void StartProcess(DIContainer container)
        {
            _container = container;
            _ghost = _container.Resolve<EntityFactory>().CreateEntity(Vector3.zero);
        }

        private void Update()
        {
            var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (_ghost is not null)
            {
                _ghost.GetMoveDirection().Value = input;
                _ghost.GetRotationDirection().Value = input;
            }
        }
    }
}