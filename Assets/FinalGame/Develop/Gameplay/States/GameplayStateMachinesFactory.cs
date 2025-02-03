using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.SceneManagement;
using FinalGame.Develop.DI;
using FinalGame.Develop.Utils.Conditions;

namespace FinalGame.Develop.Gameplay.States
{
    public class GameplayStateMachinesFactory
    {
        private readonly DIContainer _container;
        
        public GameplayStateMachinesFactory(DIContainer container)
        {
            _container = container;
        }
        
        public GameplayStateMachine CreateGameplayStateMachine(GameplaySceneInputArgs gameplayInputArgs)
        {
            GameplayStatesFactory gameplayStatesFactory = _container.Resolve<GameplayStatesFactory>();
            
            GameplayFinishConditionService  gameplayFinishConditionService = _container.Resolve<GameplayFinishConditionService>();
            
            GameplayStateMachineDisposer disposer =
                _container.Resolve<GameplayStateMachineDisposer>();

            //CREATE STATES
            InitMainHeroState initMainHeroState = gameplayStatesFactory.CreateInitMainCharacterState(gameplayInputArgs);
            GameplayStateMachine gameplayLoopState = gameplayStatesFactory.CreateGameplayLoopState(gameplayInputArgs);
            WinState winState = gameplayStatesFactory.CreateWinState(gameplayInputArgs);
            LooseState looseState = gameplayStatesFactory.CreateLooseState(gameplayInputArgs);

            //CREATE TRANSITION CONDITIONS
            ActionCondition initMainHeroStateToGameplayLoopStateCondition =
                new ActionCondition(initMainHeroState.MainHeroSetupComplete);
            
            //MARK DISPOSABLE STATES
            List<IDisposable> disposables = new List<IDisposable>
            {
                initMainHeroStateToGameplayLoopStateCondition,
                gameplayLoopState
            };

            //CREATE STATE MACHINE
            GameplayStateMachine gameplayStateMachine = new GameplayStateMachine(disposables);

            //ADD STATES
            gameplayStateMachine.AddState(initMainHeroState);
            gameplayStateMachine.AddState(gameplayLoopState);
            gameplayStateMachine.AddState(winState);
            gameplayStateMachine.AddState(looseState);
            
            //ADD TRANSITIONS
            gameplayStateMachine.AddTransition(initMainHeroState, gameplayLoopState, initMainHeroStateToGameplayLoopStateCondition);
            gameplayStateMachine.AddTransition(gameplayLoopState, winState, gameplayFinishConditionService.WinCondition);
            gameplayStateMachine.AddTransition(gameplayLoopState, looseState, gameplayFinishConditionService.LooseCondition);
            
            disposer.Set(gameplayStateMachine);

            return gameplayStateMachine;
        }
    }
}