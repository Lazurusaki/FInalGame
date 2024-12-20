using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _ghost;
        private Entity _shifter;

        private bool _isPlayerInput = true;

        public void StartProcess(DIContainer container)
        {
            _container = container;
            _ghost = _container.Resolve<EntityFactory>().CreateGhost(Vector3.forward * 5);
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.forward * -5);
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.right * -5);
            _container.Resolve<EntityFactory>().CreateGhost(Vector3.zero + Vector3.right * 5);
            _shifter = _container.Resolve<EntityFactory>().CreateShifter(Vector3.zero + Vector3.zero);
        }

        private void Update()
        {
            if (_isPlayerInput)
            {
                ProcessPlayerInput();

                if (Input.GetKeyDown(KeyCode.G))
                {
                    _isPlayerInput = false;
                    _ghost.GetMoveDirection().Value = Vector3.zero;
                    _ghost.GetRotationDirection().Value = Vector3.zero;

                    AIStateMachine ghostBehavior = _container.Resolve<AIFactory>().CreateGhostBehavior(_ghost);
                    _ghost.AddBehavior(new StateMachineBrainBehavior(ghostBehavior));
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    _isPlayerInput = true;
                    _ghost.TryRemoveBehavior<StateMachineBrainBehavior>();
                }
            }

            ProcessShifter();
        }

        private void ProcessShifter()
        {
            if (Input.GetKeyDown(KeyCode.T))
                _shifter.GetRadialAttackTeleportTrigger().Invoke();
        }

        private void ProcessPlayerInput()
        {
            var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (_ghost is not null)
            {
                _ghost.GetMoveDirection().Value = input;
                _ghost.GetRotationDirection().Value = input;

                if (Input.GetKeyDown(KeyCode.E) && _ghost.TryGetTakeDamageRequest(out var takeDamageRequest))
                {
                    takeDamageRequest.Invoke(50);
                }
            }
        }
    }
}