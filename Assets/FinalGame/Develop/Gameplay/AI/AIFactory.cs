using System;
using System.Collections.Generic;
using FinalGame.Develop.CommonServices.Timer;
using FinalGame.Develop.DI;
using FinalGame.Develop.Gameplay.AI.States;
using FinalGame.Develop.Gameplay.Entities;
using FinalGame.Develop.Utils.Conditions;

namespace FinalGame.Develop.Gameplay.AI
{
    public class AIFactory
    {
        private readonly DIContainer _container;
        private TimerServiceFactory _timerServiceFactory;

        public AIFactory(DIContainer container)
        {
            _container = container;
            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
        }

        public AIStateMachine CreateGhostBehavior(Entity entity)
            => CreateWanderingStateMachine(entity);

        private AIStateMachine CreateWanderingStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            var moveDirection = entity.GetMoveDirection();
            var rotationDirection = entity.GetRotationDirection();

            Wandering wanderingState = new Wandering(moveDirection, rotationDirection, 0.5f);

            Idle idleState = new Idle();

            TimerService wanderingTimer = _timerServiceFactory.Create(2);
            disposables.Add(wanderingState.Entered.Subscribe(wanderingTimer.Restart));
            FuncCondition wanderingTimerEndedCondition = new FuncCondition(() => wanderingTimer.IsOver);

            TimerService idleTimer = _timerServiceFactory.Create(3);
            disposables.Add(idleState.Entered.Subscribe(idleTimer.Restart));
            FuncCondition idleTimerEndedCondition = new FuncCondition(() => idleTimer.IsOver);

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(wanderingState);
            stateMachine.AddState(idleState);

            stateMachine.AddTransition(wanderingState, idleState, wanderingTimerEndedCondition);
            stateMachine.AddTransition(idleState, wanderingState, idleTimerEndedCondition);
            
            return stateMachine;
        }
    }
}