using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Utils.StateMachine;
using UnityEngine;

namespace FinalGame.Develop.Gameplay.States
{
    public class LooseState : EndGameState, IUpdatableState
    {
        private readonly SceneSwitcher _sceneSwitcher;
        
        public LooseState(SceneSwitcher sceneSwitcher,IPauseService pauseService, IInputService inputService) : base(pauseService, inputService)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("You Loose");
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _sceneSwitcher.ProcessSwitchSceneFor(new GameplaySceneOutputArgs(new MainMenuSceneInputArgs()));
        }
    }
}