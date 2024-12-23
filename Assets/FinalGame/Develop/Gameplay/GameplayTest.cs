using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI;
using FinalGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace FinalGame.Develop.Gameplay
{
    public class GameplayTest : MonoBehaviour
    {
        private DIContainer _container;

        private Entity _mainHero;
        private Entity _shifter;

        private bool _isPlayerInput = true;

        public void StartProcess(DIContainer container)
        {
            _container = container;
            _mainHero = _container.Resolve<EntityFactory>().CreateMainHero(Vector3.forward * 5);
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
                    _mainHero.GetMoveDirection().Value = Vector3.zero;
                    _mainHero.GetRotationDirection().Value = Vector3.zero;

                    AIStateMachine ghostBehavior = _container.Resolve<AIFactory>().CreateGhostBehavior(_mainHero);
                    _mainHero.AddBehavior(new StateMachineBrainBehavior(ghostBehavior));
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    _isPlayerInput = true;
                    _mainHero.TryRemoveBehavior<StateMachineBrainBehavior>();
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

            if (_mainHero is not null)
            {
                _mainHero.GetMoveDirection().Value = input;
                _mainHero.GetRotationDirection().Value = input;

                if (Input.GetKeyDown(KeyCode.E) && _mainHero.TryGetTakeDamageRequest(out var takeDamageRequest))
                {
                    takeDamageRequest.Invoke(50);
                }
                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _mainHero.GetAttackTrigger().Invoke();
                }
            }
        }
    }
}