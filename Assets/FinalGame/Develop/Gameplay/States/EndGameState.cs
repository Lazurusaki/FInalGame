using FinalGame.Develop.Gameplay.Features.Input;
using FinalGame.Develop.Gameplay.Features.Pause;
using FinalGame.Develop.Utils.StateMachine;

namespace FinalGame.Develop.Gameplay.States
{
    public abstract class EndGameState : State
    {
        private readonly IPauseService _pauseService;
        private readonly IInputService _inputService;


        protected EndGameState(IPauseService pauseService, IInputService inputService)
        {
            _pauseService = pauseService;
            _inputService = inputService;
        }

        public override void Enter()
        {
            base.Enter();
            
            _pauseService.Pause();
            _inputService.IsEnabled = false;
        }

        public override void Exit()
        {
            base.Exit();
            
            _pauseService.Unpause();
            _inputService.IsEnabled = true;
        }
    }
}